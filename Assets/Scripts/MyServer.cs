using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Text;

public class MyServer : MonoBehaviour
{
    public static MyServer Instance;
    private TcpListener server;
    private Thread serverThread;
    private NetworkStream stream = null;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateServer()
    {
        Debug.Log(Globals.ipAddressString);
        server = new TcpListener(Globals.ipAddress, Globals.port);
        server.Start();
        Debug.Log("Server created successfully");
        serverThread = new Thread(ReceiverThread);
        serverThread.Start();
    }

    private void ReceiverThread()
    {
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            
            if (client != null)
            {
                Debug.Log("Client is connected :: " + client.Client.LocalEndPoint);
                stream = client.GetStream();
                StartReceiving();
                //Globals.isConnected = true;
            }
        }
    }

    private void StartReceiving()
    {
        while (true)
        {
            if (stream != null)
            {
                string receivedMsg = ReceiveMessage();

                if (receivedMsg != null)
                {
                    // Process data sent by client
                    string msgForClient = "Server Ack :: " + receivedMsg;
                    SendData(msgForClient);
                }
            }
        }
    }


    public void SendData(string msg)
    {
        byte[] bytesToClient = Encoding.ASCII.GetBytes(msg);
        stream.Write(bytesToClient, 0, bytesToClient.Length);
        Debug.Log("Server Sent :: " + msg);
    }

    public string ReceiveMessage()
    {
        byte[] bytes = new byte[256];
        int bytesRead = stream.Read(bytes, 0, bytes.Length);

        if (bytesRead > 0)
        {
            string msg = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
            Debug.LogFormat("Server Received :: {0}", msg);
            return msg;
        }

        return null;
    }
}
