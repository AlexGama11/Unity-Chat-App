using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public class TcpServer : MonoBehaviour
{
    public string host;
    public int port;
    private TcpListener server;

    private Thread serverThread = null;
    NetworkStream stream;

    void Start()
    {
        CreateServer();
    }


    void CreateServer()
    {
        serverThread = new Thread(ReceiverThread);
        serverThread.Start();
    }

    void ReceiverThread()
    {
        IPAddress iPAddress = IPAddress.Parse(Globals.IpAddress);
        port = Int32.Parse(Globals.Port);
        Debug.Log(iPAddress.ToString());
        server = new TcpListener(iPAddress, port);
        server.Start();
        Debug.Log("Server created succesfully");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            if (client != null)
            {
                Debug.Log("Client is connected :: " + client.Client.LocalEndPoint);
            }

            stream = client.GetStream();
            Byte[] bytes = new Byte[256];
            int i;
            string msg;

            // Loops to receive all the data
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                //Translate data bytes to a  ASCII
                msg = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Debug.LogFormat("Server Received :: {0}", msg);

                //Process data sent by client
                string msgForClient = "Server Ack :: " + msg;
                byte[] bytesToClient = System.Text.Encoding.ASCII.GetBytes(msg);

                //Send back response
                stream.Write(bytesToClient, 0, bytesToClient.Length);
                Debug.LogFormat("Server Sent :: {0}", msgForClient);

            }

        }
    }
}
