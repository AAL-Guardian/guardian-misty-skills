using System;
using System.Collections.Generic;
using Windows.Foundation;
using MistyRobotics.Common.Data;
using MistyRobotics.Common.Types;
using MistyRobotics.SDK;
using MistyRobotics.SDK.Commands;
using MistyRobotics.SDK.Events;
using MistyRobotics.SDK.Logger;
using MistyRobotics.SDK.Messengers;
using MistyRobotics.SDK.Responses;

namespace CloudConnector
{
    public class GuardianCommand: IRobotCommand
    {
        public int CommandId { get; }
        public MessageType MessageType { get; }
        public DateTimeOffset Created { get; }
        public bool AckResponse { get; set; }
    }
    
    public class IGuardianRobot: IRobotMessenger
    {
        public bool ConnectToMisty(IMistySkill skill)
        {
            throw new NotImplementedException();
        }

        public ISDKLogger SkillLogger { get; }
        public bool SkillCancelling()
        {
            throw new NotImplementedException();
        }

        public bool Wait(int timeoutMs)
        {
            throw new NotImplementedException();
        }

        public void SkillCompleted()
        {
            throw new NotImplementedException();
        }

        public void SetCommandTimeout(MessageType messageType, int timeoutMs)
        {
            throw new NotImplementedException();
        }

        public bool SetAckResponse(MessageType messageType)
        {
            throw new NotImplementedException();
        }

        public bool SetDetailedResponse(MessageType messageType)
        {
            throw new NotImplementedException();
        }

        public IList<RegisteredEvent> GetRegisteredEvents(EventType eventType)
        {
            throw new NotImplementedException();
        }

        public IDictionary<EventType, IList<RegisteredEvent>> GetAllRegisteredEvents()
        {
            throw new NotImplementedException();
        }

        public void UnregisterEvent(string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void UnregisterAllEvents(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ExecuteAsync(IRobotCommand command)
        {
            throw new NotImplementedException();
        }

        public void Execute(IRobotCommand entity, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void ExecuteGroup(IList<IRobotCommand> commands, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IList<IRobotCommandResponse>> ExecuteGroupAsync(IList<IRobotCommand> commands)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterActuatorEvent(int debounce, bool keepAlive, IList<ActuatorPositionValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterBatteryChargeEvent(int debounce, bool keepAlive, IList<BatteryChargeValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterBumpSensorEvent(int debounce, bool keepAlive, IList<BumpSensorValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterCapTouchEvent(int debounce, bool keepAlive, IList<CapTouchValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterDriveEncoderEvent(int debounce, bool keepAlive, IList<DriveEncoderValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterFaceRecognitionEvent(int debounce, bool keepAlive, IList<FaceRecognitionValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterFaceTrainingEvent(int debounce, bool keepAlive, IList<FaceTrainingValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterIMUEvent(int debounce, bool keepAlive, IList<IMUValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterLocomotionCommandEvent(int debounce, bool keepAlive, IList<LocomotionCommandValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterCurrentEvent(int debounce, bool keepAlive, IList<CurrentValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterTimeOfFlightEvent(int debounce, bool keepAlive, IList<TimeOfFlightValidation> validations, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterKeyPhraseRecognizedEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterHaltCommandEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterAudioPlayCompleteEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterHazardNotificationEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterWorldStateEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSelfStateEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSerialMessageEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSourceFocusConfigEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSourceTrackDataEvent(int debounce, bool keepAlive, string eventName,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterUserEvent(string eventName, int debounce, bool keepAlive, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterActuatorEvent(ProcessActuatorEventResponse eventCallback, int debounce, bool keepAlive,
            IList<ActuatorPositionValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterBatteryChargeEvent(ProcessBatteryChargeEventResponse eventCallback, int debounce, bool keepAlive,
            IList<BatteryChargeValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterBumpSensorEvent(ProcessBumpSensorEventResponse eventCallback, int debounce, bool keepAlive,
            IList<BumpSensorValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterCapTouchEvent(ProcessCapTouchEventResponse eventCallback, int debounce, bool keepAlive,
            IList<CapTouchValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterDriveEncoderEvent(ProcessDriveEncoderEventResponse eventCallback, int debounce, bool keepAlive,
            IList<DriveEncoderValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterFaceRecognitionEvent(ProcessFaceRecognitionEventResponse eventCallback, int debounce,
            bool keepAlive, IList<FaceRecognitionValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterFaceTrainingEvent(ProcessFaceTrainingEventResponse eventCallback, int debounce, bool keepAlive,
            IList<FaceTrainingValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterIMUEvent(ProcessIMUEventResponse eventCallback, int debounce, bool keepAlive, IList<IMUValidation> validations,
            string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterLocomotionCommandEvent(ProcessLocomotionCommandEventResponse eventCallback, int debounce,
            bool keepAlive, IList<LocomotionCommandValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterCurrentEvent(ProcessCurrentEventResponse eventCallback, int debounce, bool keepAlive,
            IList<CurrentValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterTimeOfFlightEvent(ProcessTimeOfFlightEventResponse eventCallback, int debounce, bool keepAlive,
            IList<TimeOfFlightValidation> validations, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterKeyPhraseRecognizedEvent(ProcessKeyPhraseRecognizedEventResponse eventCallback, int debounce,
            bool keepAlive, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterHaltCommandEvent(ProcessHaltCommandEventResponse eventCallback, int debounce, bool keepAlive,
            string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterAudioPlayCompleteEvent(ProcessAudioPlayCompleteEventResponse eventCallback, int debounce,
            bool keepAlive, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterUserEvent(string eventName, ProcessUserEventResponse eventCallback, int debounce, bool keepAlive,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterHazardNotificationEvent(ProcessHazardNotificationEventResponse eventCallback, int debounce,
            bool keepAlive, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterWorldStateEvent(ProcessWorldStateEventResponse eventCallback, int debounce, bool keepAlive,
            string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSelfStateEvent(ProcessSelfStateEventResponse eventCallback, int debounce, bool keepAlive,
            string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSerialMessageEvent(ProcessSerialMessageEventResponse eventCallback, int debounce, bool keepAlive,
            string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSourceFocusConfigEvent(ProcessSourceConfigMessageEventResponse eventCallback, int debounce,
            bool keepAlive, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IEventDetails RegisterSourceTrackDataEvent(ProcessSourceTrackDataMessageEventResponse eventCallback, int debounce,
            bool keepAlive, string eventName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void Drive(double linearVelocity, double angularVelocity, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DriveAsync(double linearVelocity, double angularVelocity)
        {
            throw new NotImplementedException();
        }

        public void LocomotionTrack(double leftTrackSpeed, double rightTrackSpeed, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> LocomotionTrackAsync(double leftTrackSpeed, double rightTrackSpeed)
        {
            throw new NotImplementedException();
        }

        public void DriveTime(double linearVelocity, double angularVelocity, int timeMs, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DriveTimeAsync(double linearVelocity, double angularVelocity, int timeMs)
        {
            throw new NotImplementedException();
        }

        public void DriveHeading(double heading, double distance, int timeMs, bool reverse, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DriveHeadingAsync(double heading, double distance, int timeMs, bool reverse)
        {
            throw new NotImplementedException();
        }

        public void DriveArc(double heading, double radius, int timeMs, bool reverse, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DriveArcAsync(double heading, double radius, int timeMs, bool reverse)
        {
            throw new NotImplementedException();
        }

        public void FollowPath(IList<MapCell> waypoints, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> FollowPathAsync(IList<MapCell> waypoints)
        {
            throw new NotImplementedException();
        }

        public void ResetSlam(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ResetSlamAsync()
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartTrackingAsync()
        {
            throw new NotImplementedException();
        }

        public void StartTracking(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopTrackingAsync()
        {
            throw new NotImplementedException();
        }

        public void StopTracking(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartMappingAsync()
        {
            throw new NotImplementedException();
        }

        public void StartMapping(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopMappingAsync()
        {
            throw new NotImplementedException();
        }

        public void StopMapping(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartSlamStreamingAsync()
        {
            throw new NotImplementedException();
        }

        public void StartSlamStreaming(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopSlamStreamingAsync()
        {
            throw new NotImplementedException();
        }

        public void StopSlamStreaming(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartRecordingAudioAsync(string filename)
        {
            throw new NotImplementedException();
        }

        public void StartRecordingAudio(string filename, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopRecordingAudioAsync()
        {
            throw new NotImplementedException();
        }

        public void StopRecordingAudio(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartRecordingVideoAsync()
        {
            throw new NotImplementedException();
        }

        public void StartRecordingVideo(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopRecordingVideoAsync()
        {
            throw new NotImplementedException();
        }

        public void StopRecordingVideo(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ClearDisplayTextAsync()
        {
            throw new NotImplementedException();
        }

        public void ClearDisplayText(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartFaceDetectionAsync()
        {
            throw new NotImplementedException();
        }

        public void StartFaceDetection(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopFaceDetectionAsync()
        {
            throw new NotImplementedException();
        }

        public void StopFaceDetection(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartFaceRecognitionAsync()
        {
            throw new NotImplementedException();
        }

        public void StartFaceRecognition(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopFaceRecognitionAsync()
        {
            throw new NotImplementedException();
        }

        public void StopFaceRecognition(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> CancelFaceTrainingAsync()
        {
            throw new NotImplementedException();
        }

        public void CancelFaceTraining(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IStartFaceTrainingResponse> StartFaceTrainingAsync(string faceId)
        {
            throw new NotImplementedException();
        }

        public void StartFaceTraining(string faceId, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ForgetAllFacesAsync()
        {
            throw new NotImplementedException();
        }

        public void ForgetAllFaces(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void DisplayImage(string fileName, double alpha, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DisplayImageAsync(string fileName, double alpha)
        {
            throw new NotImplementedException();
        }

        public void DeleteImage(string fileName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DeleteImageAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public void DeleteAudio(string fileName, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> DeleteAudioAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopAsync()
        {
            throw new NotImplementedException();
        }

        public void Stop(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void ChangeLED(uint red, uint green, uint blue, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ChangeLEDAsync(uint red, uint green, uint blue)
        {
            throw new NotImplementedException();
        }

        public void SetNetworkConnection(string networkName, string password, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetNetworkConnectionAsync(string networkName, string password)
        {
            throw new NotImplementedException();
        }

        public void MoveHead(double pitch, double roll, double yaw, double velocity, AngularUnit units,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> MoveHeadAsync(double pitch, double roll, double yaw, double velocity, AngularUnit units)
        {
            throw new NotImplementedException();
        }

        public void MoveArms(double leftArmPosition, double rightArmPosition, double? leftArmVelocity, double? rightArmVelocity,
            double? duration, AngularUnit units, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> MoveArmsAsync(double leftArmPosition, double rightArmPosition, double? leftArmVelocity,
            double? rightArmVelocity, double? duration, AngularUnit units)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SaveAudioAsync(string fileName, IEnumerable<byte> data, bool immediatelyApply, bool overwriteExisting)
        {
            throw new NotImplementedException();
        }

        public void SaveAudio(string fileName, IEnumerable<byte> data, bool immediatelyApply, bool overwriteExisting,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void PlayAudio(string fileName, int? volume, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> PlayAudioAsync(string fileName, int? volume)
        {
            throw new NotImplementedException();
        }

        public void SetDefaultVolume(int volume, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetDefaultVolumeAsync(int volume)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SaveImageAsync(string fileName, IEnumerable<byte> data, bool immediatelyApply, bool overwriteExisting,
            double? width, double? height)
        {
            throw new NotImplementedException();
        }

        public void SaveImage(string fileName, IEnumerable<byte> data, bool immediatelyApply, bool overwriteExisting, double? width,
            double? height, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> HaltAsync(IList<MotorMask> motorMasks)
        {
            throw new NotImplementedException();
        }

        public void Halt(IList<MotorMask> motorMasks, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StartKeyPhraseRecognitionAsync()
        {
            throw new NotImplementedException();
        }

        public void StartKeyPhraseRecognition(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> StopKeyPhraseRecognitionAsync()
        {
            throw new NotImplementedException();
        }

        public void StopKeyPhraseRecognition(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> WriteToSerialStreamAsync(string message)
        {
            throw new NotImplementedException();
        }

        public void WriteToSerialStream(string message, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> RebootSlamSensorAsync()
        {
            throw new NotImplementedException();
        }

        public void RebootSlamSensor(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetBlinkingAsync(bool blink)
        {
            throw new NotImplementedException();
        }

        public void SetBlinking(bool blink, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetBlinkSettingsAsync(bool revertToDefault, IDictionary<string, string> newMappings, int? openEyeMinMs,
            int? openEyeMaxMs, int? closedEyeMinMs, int? closedEyeMaxMs)
        {
            throw new NotImplementedException();
        }

        public void SetBlinkSettings(bool revertToDefault, IDictionary<string, string> newMappings, int? openEyeMinMs, int? openEyeMaxMs,
            int? closedEyeMinMs, int? closedEyeMaxMs, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> ResetBlinkSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public void ResetBlinkSettings(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> RemoveBlinkMappingsAsync(IList<string> openEyeImageNames)
        {
            throw new NotImplementedException();
        }

        public void RemoveBlinkMappings(IList<string> openEyeImageNames, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetNotificationSettingsAsync(bool revertToDefault, bool? ledEnabled, bool? keyPhraseEnabled,
            string keyPhraseFile)
        {
            throw new NotImplementedException();
        }

        public void SetNotificationSettings(bool revertToDefault, bool? ledEnabled, bool? keyPhraseEnabled, string keyPhraseFile,
            ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public void PublishMessage(string message, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> PublishMessageAsync(string message)
        {
            throw new NotImplementedException();
        }

        public void SendDebugMessage(string message, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SendDebugMessageAsync(string message)
        {
            throw new NotImplementedException();
        }

        public void SetRobotLogLevel(RobotLogLevel level, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> SetRobotLogLevelAsync(RobotLogLevel level)
        {
            throw new NotImplementedException();
        }

        public void RunSkill(string uniqueId, IDictionary<string, object> parameters, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> RunSkillAsync(string uniqueId, IDictionary<string, object> parameters)
        {
            throw new NotImplementedException();
        }

        public void CancelRunningSkill(string uniqueId, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IRobotCommandResponse> CancelRunningSkillAsync(string uniqueId)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<ITakePictureResponse> TakePictureAsync(string fileName, bool base64, bool displayOnScreen, bool overwriteExisting,
            double? width, double? height)
        {
            throw new NotImplementedException();
        }

        public void TakePicture(string fileName, bool base64, bool displayOnScreen, bool overwriteExisting, double? width,
            double? height, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<ISendExternalRequestResponse> SendExternalRequestAsync(string method, string resource, string authorizationType, string token,
            string arguments, bool save, bool apply, string fileName, string contentType)
        {
            throw new NotImplementedException();
        }

        public void SendExternalRequest(string method, string resource, string authorizationType, string token, string arguments,
            bool save, bool apply, string fileName, string contentType, ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetKnownFacesResponse> GetKnownFacesAsync()
        {
            throw new NotImplementedException();
        }

        public void GetKnownFaces(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetAudioListResponse> GetAudioListAsync()
        {
            throw new NotImplementedException();
        }

        public void GetAudioList(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetImageListResponse> GetImageListAsync()
        {
            throw new NotImplementedException();
        }

        public void GetImageList(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetImageResponse> GetImageAsync(string fileName, bool base64)
        {
            throw new NotImplementedException();
        }

        public void GetImage(ProcessCommandResponse commandCallback, string fileName, bool base64)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetAudioResponse> GetAudioAsync(string fileName, bool base64)
        {
            throw new NotImplementedException();
        }

        public void GetAudio(ProcessCommandResponse commandCallback, string fileName, bool base64)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetVideoResponse> GetVideoAsync()
        {
            throw new NotImplementedException();
        }

        public void GetVideo(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetDeviceInformationResponse> GetDeviceInformationAsync()
        {
            throw new NotImplementedException();
        }

        public void GetDeviceInformation(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<ITakeFisheyePictureResponse> TakeFisheyePictureAsync(bool base64)
        {
            throw new NotImplementedException();
        }

        public void TakeFisheyePicture(ProcessCommandResponse commandCallback, bool base64)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<ITakeDepthPictureResponse> TakeDepthPictureAsync()
        {
            throw new NotImplementedException();
        }

        public void TakeDepthPicture(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetSlamStatusResponse> GetSlamStatusAsync()
        {
            throw new NotImplementedException();
        }

        public void GetSlamStatus(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetLogFileResponse> GetLogFileAsync(DateTimeOffset? datetime)
        {
            throw new NotImplementedException();
        }

        public void GetLogFile(ProcessCommandResponse commandCallback, DateTimeOffset? datetime)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetMapResponse> GetMapAsync()
        {
            throw new NotImplementedException();
        }

        public void GetMap(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetSlamPathResponse> GetSlamPathAsync(int x, int y, double minGap, double wallCostDistance, bool unknownIsOpen)
        {
            throw new NotImplementedException();
        }

        public void GetSlamPath(ProcessCommandResponse commandCallback, int x, int y, double minGap, double wallCostDistance,
            bool unknownIsOpen)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetBatteryDetailsResponse> GetBatteryDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public void GetBatteryDetails(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetStoreUpdateAvailableResponse> GetStoreUpdateAvailableAsync()
        {
            throw new NotImplementedException();
        }

        public void GetStoreUpdateAvailable(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetSerialSensorDataResponse> GetSerialSensorValuesAsync()
        {
            throw new NotImplementedException();
        }

        public void GetSerialSensorValues(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetCameraDetailsResponse> GetCameraDataAsync()
        {
            throw new NotImplementedException();
        }

        public void GetCameraData(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetWebsocketDetailsResponse> GetWebsocketNamesAsync()
        {
            throw new NotImplementedException();
        }

        public void GetWebsocketNames(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetBlinkSettingsResponse> GetBlinkSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public void GetBlinkSettings(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetRobotLogLevelResponse> GetRobotLogLevelAsync()
        {
            throw new NotImplementedException();
        }

        public void GetRobotLogLevel(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetRunningSkillsResponse> GetRunningSkillsAsync()
        {
            throw new NotImplementedException();
        }

        public void GetRunningSkills(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public IAsyncOperation<IGetSkillsResponse> GetSkillsAsync()
        {
            throw new NotImplementedException();
        }

        public void GetSkills(ProcessCommandResponse commandCallback)
        {
            throw new NotImplementedException();
        }

        public int DefaultCommandTimeout { get; set; }
        public NativeSkillStatus SkillStatus { get; }
        public event EventHandler<IDictionary<string, object>> StartSkillCommandReceived;
        public event EventHandler<IDictionary<string, object>> CancelSkillCommandReceived;
        public event EventHandler<IDictionary<string, object>> TimeoutCommandReceived;
        public event EventHandler<IDictionary<string, object>> PauseCommandReceived;
        public event EventHandler<IDictionary<string, object>> ResumeCommandReceived;
        public event EventHandler<IActuatorEvent> ActuatorEventReceived;
        public event EventHandler<IFaceRecognitionEvent> FaceRecognitionEventReceived;
        public event EventHandler<IIMUEvent> IMUEventReceived;
        public event EventHandler<ITimeOfFlightEvent> TimeOfFlightEventReceived;
        public event EventHandler<IBumpSensorEvent> BumpSensorEventReceived;
        public event EventHandler<IAudioPlayCompleteEvent> AudioPlayCompleteEventReceived;
        public event EventHandler<IDriveEncoderEvent> DriveEncoderEventReceived;
        public event EventHandler<ICapTouchEvent> CapTouchEventReceived;
        public event EventHandler<ISerialMessageEvent> SerialMessageEventReceived;
        public event EventHandler<IKeyPhraseRecognizedEvent> KeyPhraseRecognizedEventReceived;
        public event EventHandler<ISourceTrackDataMessageEvent> SourceTrackDataMessageEventReceived;
        public event EventHandler<ISourceFocusConfigMessageEvent> SourceFocusConfigMessageEventReceived;
        public event EventHandler<IBatteryChargeEvent> BatteryChargeEventReceived;
        public event EventHandler<IFaceTrainingEvent> FaceTrainingEventReceived;
        public event EventHandler<ICurrentEvent> CurrentEventReceived;
        public event EventHandler<IHaltCommandEvent> HaltCommandEventReceived;
        public event EventHandler<ILocomotionCommandEvent> LocomotionCommandEventReceived;
        public event EventHandler<IUserEvent> UserEventReceived;
        public event EventHandler<ISelfStateEvent> SelfStateEventReceived;
        public event EventHandler<IWorldStateEvent> WorldStateEventReceived;
        public event EventHandler<IHazardNotificationEvent> HazardNotificationEventReceived;
    }
}