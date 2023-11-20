using UnityEngine;
using System.Net.Sockets;
using System.Threading;

public class MyClient : MonoBehaviour
{
    public static MyClient Instance;
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiverThread;

    private void Awake()
    {
        Instance = this;
    }

    public void ConnectToServer()
    {
        client = new TcpClient(Globals.ipAddressString, Globals.port);
        stream = client.GetStream();
        SendMessage("This is Client Speaking");

        // Start a separate thread for receiving messages
        receiverThread = new Thread(ReceiveMessage);
        receiverThread.Start();
    }

    public void SendMessage(string msg)
    {
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log("Client Sent :: " + msg);
    }

    void ReceiveMessage()
    {
        while (true)
        {
            byte[] bytes = new byte[256];
            int bytesRead = stream.Read(bytes, 0, bytes.Length);

            if (bytesRead > 0)
            {
                string msgFromServer = System.Text.Encoding.ASCII.GetString(bytes, 0, bytesRead);
                Debug.Log("Message from Server :: " + msgFromServer);
                // Add any additional processing or pass the message to other scripts
            }
        }
    }
}
