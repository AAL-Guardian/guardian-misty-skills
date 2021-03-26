using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using CloudConnector.Data;
using CloudConnector.Services.Interfaces;
using MistyRobotics.Common.Data;
using MistyRobotics.SDK.Messengers;

namespace CloudConnector.Services
{
    public sealed class SkillManager: ISkillManager
    {
        private readonly IRobotMessenger _misty;
        private readonly MistyConfiguration _configuration;
        // private readonly IList<string> _skills;

        private Timer _skillHeartbeatTimer;

        public SkillManager(IRobotMessenger misty, MistyConfiguration configuration)
        {
            _misty = misty;
            _configuration = configuration;
            // _skills = new List<string>
            // {
            //     "17497331-1cc1-43e2-a9ff-886a845f96fb"
            // };
        }

        public IAsyncAction Start()
        {
            return _start().AsAsyncAction();
        }

        private async Task _start()
        {
            _skillHeartbeatTimer = new Timer(async (state) =>
            {
                await CheckSkills(state);
            }, null, 5000, 5000);
        }

        private async Task CheckSkills(object state)
        {
            var runningSkills = await _misty.GetRunningSkillsAsync();
            var allSkills = await _misty.GetSkillsAsync();

            foreach (var skill in allSkills.Data)
            {
                if (runningSkills.Data.All(s => s.UniqueId != skill.UniqueId))
                {
                    await _misty.SendDebugMessageAsync($"Skill {skill.Name} not running anymore, restarting...");
                    await _misty.RunSkillAsync(skill.UniqueId.ToString(), skill.Parameters);
                }
            }
        }
    }
}