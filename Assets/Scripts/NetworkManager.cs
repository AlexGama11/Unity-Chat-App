using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiverThread;

    public void StartNetworking(bool isServer, string host, int port)
    {
        if (isServer)
        {
            CreateServer(host, port);
        }
        else
        {
            ConnectToServer(host, port);
        }
    }

    private void CreateServer(string host, int port)
    {
        server = new TcpListener(IPAddress.Parse(host), port);
        server.Start();
        Debug.Log("Server created successfully");

        while (true)
        {
            TcpClient connectedClient = server.AcceptTcpClient();
            if (connectedClient != null)
            {
                Debug.Log("Client connected :: " + connectedClient.Client.LocalEndPoint);
            }

            stream = connectedClient.GetStream();
            StartReceiverThread();
        }
    }

    private void ConnectToServer(string host, int port)
    {
        client = new TcpClient(host, port);
        stream = client.GetStream();
        StartReceiverThread();
    }

    private void StartReceiverThread()
    {
        receiverThread = new Thread(ReceiveData);
        receiverThread.Start();
    }

    public void SendData(string msg)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log("Sent :: " + msg);
    }

    private void ReceiveData()
    {
        while (true)
        {
            byte[] bytes = new byte[256];
            stream.Read(bytes, 0, bytes.Length);
            string receivedMsg = Encoding.ASCII.GetString(bytes);
            Debug.Log("Received :: " + receivedMsg);
        }
    }
}
