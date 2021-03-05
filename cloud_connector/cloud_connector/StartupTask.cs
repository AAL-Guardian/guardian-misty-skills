using Windows.ApplicationModel.Background;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK.Messengers;

namespace CloudConnector
{
    public sealed class StartupTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            RobotMessenger.LoadAndPrepareSkill(taskInstance, new CloudConnector(), SkillLogLevel.Verbose);
        }
    }
}