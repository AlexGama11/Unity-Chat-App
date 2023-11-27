using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    public TMP_InputField chatInput;
    public Transform chatContentBox;
    public MessageBubble chatBubble;
    public string chatMessage;

    public Button sendButton;

    // Start is called before the first frame update
    void Start()
    {
        sendButton.onClick.AddListener(SendMessage);
        chatInput.add

    }

    // Update is called once per frame
    void Update()
    {

      if (Globals.isConnected)
        {
            DisplayMessage(); // Change to OnMessageReceived event
        }
    }

    void GetChatInput()
    {
       chatMessage = chatInput.text;
       //Debug.Log(chatMessage);
    }

    void SendMessage()
    {
        if (Globals.isServer)
        {
            MyServer.Instance.SendData(chatMessage);
            Debug.Log(chatMessage);
        }

        else
        {
            MyClient.Instance.SendData(chatMessage);

        }
    }

    void DisplayMessage()
    {
        if (Globals.isServer)
        {
            if (MyServer.Instance.ReceiveMessage() != null)
            {
                MessageBubble newMessageBubble = Instantiate(chatBubble, chatContentBox);
                newMessageBubble.messageText = MyServer.Instance.ReceiveMessage();
                newMessageBubble.userText = Globals.Username + ":"; // to replace with receiving usernames.

            }
            Debug.Log(chatMessage);
        }

        else
        {
            if (MyClient.Instance.ReceiveMessage() != null)
            {
                MessageBubble newMessageBubble = Instantiate(chatBubble, chatContentBox);
                newMessageBubble.messageText = MyClient.Instance.ReceiveMessage();
                newMessageBubble.userText = Globals.Username + ":"; // to replace with receiving usernames.
            }
        }
    }
}
