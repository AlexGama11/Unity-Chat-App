using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using System.Linq;
using UnityEngine.UI;
using System;

public class Chat : MonoBehaviour
{
    public List<string> bubbleText;
    public TMP_InputField chatText;

    public float chatBubbleX = -246.00f; //-246
    public float chatBubbleY = -112.00f; //-112

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chatText != null)
        {
            bubbleText.Add(chatText.text);

            if (bubbleText.Count == 1)
            {

                var textMessage = new GameObject();
                var message = textMessage.AddComponent<Text>();
                Debug.Log(bubbleText.Count);
                //message.text = bubbleText.ElementAt(1);
                textMessage.transform.position = new Vector2(chatBubbleX, chatBubbleY);
                textMessage.tag = "Message " + bubbleText.Count;
            }

            else
            {
                //var textComponent = chatCanvas.AddComponent<Text>();
                //textComponent.text = bubbleText.ElementAt(bubbleText.Count);
                //int previousMessageCount = bubbleText.Count - 1;
                //float oldTextYCoord = GameObject.FindGameObjectWithTag("Message " + previousMessageCount).transform.position.y;
                //textComponent.transform.position = new Vector2(chatBubbleX, oldTextYCoord + 20);
                //textComponent.tag = "Message " + bubbleText.Count;
            }
        }
    }
}
