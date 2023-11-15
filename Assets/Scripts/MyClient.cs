using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.PackageManager;
using UnityEngine;
using System;
using System.Threading;


public class MyClient : MonoBehaviour
{
    TcpClient client = null;
    public string host;
    public int port;

    NetworkStream stream;

    Thread receiverThread;

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ConnectedToServer();
        }
    }

    void ConnectedToServer()
    {
        host = Globals.IpAddress;
        port = Int32.Parse(Globals.Port);
        client = new TcpClient(host, port);
        stream = client.GetStream();
        SendData("This is Client Speaking");
        receiverThread = new Thread(ReceiveData);
        receiverThread.Start();
        SendData("Hi this is client");
    }

    private void SendData(string msg)
    {
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log("Client Sent :: " + msg);

    }

    private void ReceiveData()
    {
        while (true)
        {
            Byte[] bytes = new byte[256];
            stream.Read(bytes, 0, bytes.Length);
            string msgFromServer = System.Text.Encoding.ASCII.GetString(bytes);
            Debug.Log("Message from Server test:: " + msgFromServer);
        }
    }
}
