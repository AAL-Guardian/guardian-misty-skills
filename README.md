# guardian-misty-skills

## Skills

A list of skill implemented in this repository

### Cloud connector - MQTT Bridge

Developed By: Smartrobot Solutions

Folder: cloud_connector

Language: C# UWP

Description:  The MQTT cloud connector skill makes a connection to an MQTT broker in the cloud based on a retrieved configuration. Here it listens for messages on the given topic. When a message is received it will be adapted to a Misty event message and published to Misty as an event. The cloud connector also listens for Misty events to be send to the MQTT broker.

## Cloud connector startup procedure

### 1 Getting configuration
If misty already has a configuration file saved locally, then this will be used, else:

Misty will get a new configuration from: [https://api.guardian.jef.it/dev/robot/install](https://api.guardian.jef.it/dev/robot/install) and saves this to the local storage for later uses.

The configuration is saved in the default local folder, together with the pfx certificate used by the mqtt service. 
### 2 Setup mqtt connection
Misty will try to connect to mqtt based on the settings from de configuration file.
It will use the robotcode as a base topic (misty-{serial number}).
The service uses a couple of subtopics for it's functionalities:
  1. **"/Status"** subtopic: Used for alive messages. This will be in the form of:
     ```json
     {
       "alive": "true/false"
     }
     ```
  2. **"/Command"** subtopic: Used for receiving commands from the cloud. The mqtt service only subscribes to this subtopic. These command needs to be in this format:
     ```json
     {
       "guardian_command": "{command name}",
       "guardian_data": "{data in plain text or json object}"
     }
     ```
     If a command is received, the service will send a event to the misty event bus with the command name as event name. This way each skill can "subscribe" to it's own commands.

     The data in the event is in the exact format as above.
  3. **"/Event"** subtopic: Used for sending commands back to the cloud when a skill triggers an event meant for the cloud connector.
     These event need to have the following data:

     Event name: "guardian"

     The payload can be of any format, as long as it's serializable and there is a payload provided.
     
### 3 Setup misty event service
1. **"Sending"**: The event service listens for events with event name containing "guardian" and sends these to the mqtt service.
2. **"Receiving"**: If a message is received from the mqtt service it will send an event in the ffollowing format:
    ```json
    {
       "guardian_command": "{command name}",
       "guardian_data": "{data in plain text or json object}"
    }
    ```
    The service will send a event to the misty event bus with the command name as event name. This way each skill can "subscribe" to it's own commands.

    The source of the event will always be: "cloud_connector"

    The data in the event is in the exact format as above.
### 4 Setup skillmanager
The skill manager is a separate addon of the cloud connector and monitors the othe skills on the misty.

The skill manager will check the running state of every skill, every 5 minutes.

If a skill is stopped, it will (re)start it automatically.
## Development
Use the following guidelines to communicate and work with the cloud connector:

#### Send a message to the cloud from a skill:
Use the following code snippet to send a event to the cloud connector:

* Javascript:
  ```javascript
  misty.TriggerEvent("guardian", "{skill name}", "{The data as object or string}", "");
  ```

#### Sending a message to a skill:
Use the following code snippet to receive events from the cloud connector/ cloud:

* Javascript
  ```javascript
  misty.RegisterUserEvent("{skill_name}", true);
  
  function skill_name(data)
  {
    misty.Debug(data["guardian_command"]); // Equal to the skill_name
    misty.Debug(data["guardian_data"]); // Contains the data from the cloud
  }
  ```
  Replace skill_name with the name of your skill

#### Deploy:
Upload the cloud_connector.zip file in the publish folder to the misty skill runner
Use the zip that ends with Dev for the development environment and the version that ends with Prod for production
