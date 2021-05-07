# guardian-misty-skills

## Skills

A list of skill implemented in this repository

### Cloud connector - MQTT Bridge

Developed By: Smartrobot Solutions

Folder: cloud_connector

Language: C# UWP

Description:  The MQTT cloud connector skill makes a connection to an MQTT broker in the cloud based on a retrieved configuration. Here it listens for messages on the given topic. When a message is received it will be adapted to a Misty event message and published to Misty as an event. The cloud connector also listens for Misty events to be send to the MQTT broker.

#### Triggering Cloud -> Misty Event:
Any event send to MQTT topic: {robotTopic}/command.
Cloud connector expects json format of:

```json
{
    "guardian_command": "{command name}",
    "guardian_data": "{data in plain text or json object}"
}
```

The json event will be send as Misty event with event name as the value of guardian_command.

#### Triggering Misty Event -> Cloud:
Any event send to the Guardian misty event.
Cloud connector accepts any format as long as its a string.
The event data will be send to the cloud on MQTT topic: {robotTopic}/event.

#### Configuration:

* ConfigurationEndpoint: Endpoint where the configuration wil be retrieved, default: "https://pou41w0mic.execute-api.eu-west-1.amazonaws.com/dev/robot/install"
* ResetConfig: Boolean to reset the configuration, default: true
* RobotCode: The code of the Robot, default: 12312312312

#### Deploy:
Upload the cloud_connector.zip file in the publish folder to the misty skill runner
