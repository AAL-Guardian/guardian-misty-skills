import { Misty, Question } from "./types";
import { IClientOptions, MqttClient, Packet } from "mqtt";
import * as mqtt from "mqtt";
declare var misty: Misty;

async function main() {
  const data = {
    clientId: "robot8c70564a",
    endpoint: "a2tjpje3lbynp5-ats.iot.eu-west-1.amazonaws.com",
    robotCode: "12312312312",
    robotTopic: "misty-12312312312",
    token: "94a4b8f3-c3ce-4f21-8850-8e661697aad5"
  }

  try {
    const connection = mqtt.connect({
      host: data.endpoint,
      protocol: 'wss',
      path: '/mqtt?x-amz-customauthorizer-name=GuardianAuthorizer',
      hostname: data.endpoint,
      clientId: data.clientId,
      reconnectPeriod: 1000 * 5,
      transformWsUrl(url: string, options: IClientOptions, client: MqttClient) {
        options.username = data.token;
        return url;
      }
    });
    misty.DisplayText('Tried to connect');
    connection.on('Connect', () => {
      misty.DisplayText('Connected');
    })
    connection.on('error', (error) => {
      misty.DisplayText(JSON.stringify(error));
    })
    connection.subscribe(`${data.robotTopic}/question`);
    connection.on("message", (topic: string, payload: Buffer, packet: Packet) => {
      misty.DisplayText(topic);
      const question = JSON.parse(payload.toString()) as Question;
      misty.PlayAudio(question.audioUrl, 5);
    })

  } catch (e) {
    misty.DisplayText(JSON.stringify(e.message));
  }
}
misty.DisplayText('Ciao Ilaria');
main();