using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chat : MonoBehaviour
{
    public TMP_InputField chatInput;
    public Transform chatContentBox;
    public Message MessagePrefab;
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
      GetChatInput();
    }

    void GetChatInput()
    {
        if (chatInput.text != string.Empty)
        {
            chatMessage = chatInput.text;
            //Debug.Log(chatMessage);
        }
        
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
        //OnMessageReceive, display message. Instantiate Prefab.
    }
}
