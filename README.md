# guardian-misty-skills

## Skills

A list of skill implemented in this repository

### Cloud Connector Demo (not working)

Developed By: Jef

Folder: cloud-connector-demo

Language: Typescript -> Javascript

Listening to: "robot bootstrap", continually running

Triggering Event: any

will try to connect to the cloud and perform installation
this is only an example of a skill description

### Cloud connector - MQTT Bridge

Developed By: Smartrobot Solutions

Folder: cloud-connector

Language: C# UAP

Triggering Event: any event with name Guardian

Description:  The MQTT cloud connector skill makes a connection to an MQTT broker in the cloud based on a retrieved configuration. Here it listens for messages on the given topic. When a message is received it will be adapted to a Misty event message and published to Misty as an event. The cloud connector also listens for Misty events to be send to the MQTT broker.
