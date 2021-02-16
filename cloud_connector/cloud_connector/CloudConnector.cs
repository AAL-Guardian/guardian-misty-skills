using System.Collections.Generic;
using CloudConnector.Services;
using MistyRobotics.Common.Data;
using MistyRobotics.SDK;
using MistyRobotics.SDK.Messengers;

namespace CloudConnector
{
	internal class CloudConnector : IMistySkill
	{
		private static string ApiEndpointParamName = "ConfigurationEndpoint";
		
		private IRobotMessenger _misty;
		private IGuardianConfigurationService _mistyConfigurationService;
		private IMqttService _mqttService;
		private IMistyEventService _mistyEventService;

		/// <summary>
		/// Skill details for the robot
		/// 
		/// There are other parameters you can set if you want:
		///   Description - a description of your skill
		///   TimeoutInSeconds - timeout of skill in seconds
		///   StartupRules - a list of options to indicate if a skill should start immediately upon startup
		///   BroadcastMode - different modes can be set to share different levels of information from the robot using the 'SkillData' websocket
		///   AllowedCleanupTimeInMs - How long to wait after calling OnCancel before denying messages from the skill and performing final cleanup  
		/// </summary>
		public INativeRobotSkill Skill { get; private set; } = new NativeRobotSkill("cloud-connector", "cb2b1aa5-1226-4554-8a61-2a87ab957c8f")
		{
			AllowedCleanupTimeInMs = 1000,
			TimeoutInSeconds = int.MaxValue
		};

		/// <summary>
		///	This method is called by the wrapper to set your robot interface
		///	You need to save this off in the local variable commented on above as you are going use it to call the robot
		/// </summary>
		/// <param name="robotInterface"></param>
		public void LoadRobotConnection(IRobotMessenger robotInterface)
		{
			_misty = robotInterface;
		}

		/// <summary>
		/// This event handler is called when the robot/user sends a start message
		/// The parameters can be set in the Skill Runner (or as json) and used in the skill if desired
		/// </summary>
		/// <param name="parameters"></param>
		public async void OnStart(object sender, IDictionary<string, object> parameters)
        {
			/*
			 * 1 -> get config
			 * 2 -> start mqtt connection and client
			 * 3 -> listen for events from mqtt
			 * 4 -> listen for event from misty
			 */

			string apiUrl = "https://smartrobotsolutions-guardian.free.beeceptor.com/config";
			if (parameters.ContainsKey(ApiEndpointParamName))
				apiUrl = parameters[ApiEndpointParamName].ToString();
			
			_mistyConfigurationService = new GuardianConfigurationService(_misty, apiUrl);

			var config = await _mistyConfigurationService.GetConfigurationAsync();
			_mqttService = new MqttService(config);
			_mistyEventService = new MistyEventService(_misty);
			
			_mqttService.MqttMessageReceived += _mistyEventService.OnMqttMessage;
			_mistyEventService.MistyMessageReceived += _mqttService.OnMistyMessage;
        }

		/// <summary>
        /// This event handler is called when Pause is called on the skill
        /// User can save the skill status/data to be retrieved when Resume is called
        /// Infrastructure to help support this still under development, but users can implement this themselves as needed for now 
        /// </summary>
        /// <param name="parameters"></param>
        public void OnPause(object sender, IDictionary<string, object> parameters)
		{
			//In this template, Pause is not implemented by default
		}

		/// <summary>
		/// This event handler is called when Resume is called on the skill
		/// User can restore any skill status/data and continue from Paused location
		/// Infrastructure to help support this still under development, but users can implement this themselves as needed for now 
		/// </summary>
		/// <param name="parameters"></param>
		public void OnResume(object sender, IDictionary<string, object> parameters)
		{
			OnStart(sender, parameters);
		}
		
		/// <summary>
		/// This event handler is called when the cancel command is issued from the robot/user
		/// You currently have a few seconds to do cleanup and robot resets before the skill is shut down... 
		/// Events will be unregistered for you 
		/// </summary>
		public void OnCancel(object sender, IDictionary<string, object> parameters)
		{
			//TODO Put your code here and update the summary above
		}

		/// <summary>
		/// This event handler is called when the skill timeouts
		/// You currently have a few seconds to do cleanup and robot resets before the skill is shut down... 
		/// Events will be unregistered for you 
		/// </summary>
		public void OnTimeout(object sender, IDictionary<string, object> parameters)
		{
			//TODO Put your code here and update the summary above
		}

		#region IDisposable Support
		private bool _isDisposed = false;

		private void Dispose(bool disposing)
		{
			if (!_isDisposed)
			{
				if (disposing)
				{
					_mqttService.Dispose();
					_mistyEventService.Dispose();
					_mistyConfigurationService.Dispose();
				}

				_isDisposed = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
		}
		#endregion
	}
}
