import { Packet, connect } from "mqtt";
import bent = require('bent');

const post = bent('POST', 'json', 200);

async function main() {
  try {
    const data: any = await post('https://pou41w0mic.execute-api.eu-west-1.amazonaws.com/dev/robot/install', { robotCode: 'testasd123' });
    console.log(data);
    const connection = connect(
      {
        host: data.endpoint,
        port: 8883,
        protocol: 'mqtt',
        clientId: data.certificate.certificateId,
        rejectUnauthorized: false,
        cert: data.certificate.certificatePem,
        key: data.certificate.keyPair.PrivateKey,
        reconnectPeriod: 1000 * 30,
      });

    connection.on('connect', () => {
      console.log('connected');
      connection.subscribe(`${data.robotTopic}/test`, (err, grant) => console.log(err, grant));
      setTimeout(() => {
        connection.publish(`${data.robotTopic}/test`, 'test');  
      }, 3000);
      
    });
    connection.on('error', (error) => {
      console.log(error);
    });
    connection.on('close', () => {
      console.log('closed');
    });
    connection.on("message", (topic: string, payload: Buffer, packet: Packet) => {
      console.log(topic, payload.toString())
    });

  } catch (e) {
    console.error(e);
  }
}

main();