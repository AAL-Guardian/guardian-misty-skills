# guardian-misty-skills

## Skills

A list of skill implemented in this repository

### Cloud Connector Demo (not working)

Developed By: Jef

Folder: cloud-connector-demo

Language: Typescript -> Javascript

Listening to: "robot bootstrap", continually running

Triggering Event: any

Description: Will try to connect to the cloud and perform installation
this is only an example of a skill description

### Cloud connector - MQTT Bridge

Developed By: Smartrobot Solutions

Folder: cloud_connector

Language: C# UWP

Triggering Event: any event with name Guardian

Description:  The MQTT cloud connector skill makes a connection to an MQTT broker in the cloud based on a retrieved configuration. Here it listens for messages on the given topic. When a message is received it will be adapted to a Misty event message and published to Misty as an event. The cloud connector also listens for Misty events to be send to the MQTT broker.

Configuration: 
  * ConfigurationEndpoint: Endpoint where the configuration wil be retrieved, default: "https://pou41w0mic.execute-api.eu-west-1.amazonaws.com/dev/robot/install"
  * ResetConfig: Boolean to reset the configuration, default: true
  * RobotCode: The code of the Robot, default: 12312312312

Deploy: Upload the cloud_connector.zip file in the publish folder to the misty skill runner
