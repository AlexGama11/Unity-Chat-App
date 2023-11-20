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
    }

    // Update is called once per frame
    void Update()
    {
        if (chatInput.text != string.Empty)
        {
            GetChatInput();
        }

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
            Server.Instance.SendMessage(chatMessage);
            Debug.Log(chatMessage);
        }

        else
        {
            MyClient.Instance.SendMessage(chatMessage);

        }
    }

    void DisplayMessage()
    {
        if (Globals.isServer)
        {
            if (Server.Instance.ReceiveMessage() != null)
            {
                MessageBubble newMessageBubble = Instantiate(chatBubble, chatContentBox);
                newMessageBubble.messageText = Server.Instance.ReceiveMessage();
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
