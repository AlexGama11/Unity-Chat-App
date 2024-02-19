using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using System.Threading.Tasks;
using TMPro;

public class RockPaperScissors : MonoBehaviour
{
    private string choice;
    private string opponentChoice;
    private string result;
    public Button RockButton;
    public Button PaperButton;
    public Button ScissorsButton;
    public TMP_Text waitingTextBox;
    public TMP_Text resultTextBox;

    // Define an event for message received
    public delegate void MessageReceivedEventHandler(string username, string choice);
    public static event MessageReceivedEventHandler OnMessageReceived;

    private void Start()
    {
        RockButton.onClick.AddListener(ChooseRock);
        PaperButton.onClick.AddListener(ChoosePaper);
        ScissorsButton.onClick.AddListener(ChooseScissors);

        // Subscribe the CalculateResult method to the OnMessageReceived event
        OnMessageReceived += CalculateResult;
    }

    public void ChooseRock()
    {
        choice = "Rock";
    }

    public void ChoosePaper()
    {
        choice = "Paper";
    }

    public void ChooseScissors()
    {
        choice = "Scissors";
    }

    public async void MakeChoice()
    {
        waitingTextBox.gameObject.SetActive(true);
        resultTextBox.gameObject.SetActive(false);

        if (Globals.IsServer)
        {
            // Send player's choice
            await MyServer.Instance.SendDataAsync(choice);
        }

        else
        {
            // Send player's choice
            await MyClient.Instance.SendDataAsync(choice);
        }

    }

    private void CalculateResult(string username, string OpponentChoice)
    {
        if (OpponentChoice == "Rock")
        {
            if (choice == "Paper")
            {
                result = "Lose";
            }

            else if (choice == "Rock")
            {
                result = "Draw";
            }

            else if (choice == "Scissors")
            {
                result = "Win";
            }
        }

       else if (OpponentChoice == "Paper")
        {
            if (choice == "Paper")
            {
                result = "Draw";
            }

            else if (choice == "Rock")
            {
                result = "Win";
            }

            else if (choice == "Scissors")
            {
                result = "Lose";
            }
        }

        else if (OpponentChoice == "Scissors")
        {
            if (choice == "Paper")
            {
                result = "Win";
            }

            else if (choice == "Rock")
            {
                result = "Lose";
            }

            else if (choice == "Scissors")
            {
                result = "Draw";
            }
        }

        ShowResult(OpponentChoice, result);
    }

    public void ShowResult(string OpponentChoice, string Result)
    {
        waitingTextBox.gameObject.SetActive(false);
        resultTextBox.gameObject.SetActive(true);
        resultTextBox.text = "Opponent chose: " + opponentChoice + "\nResult: " + result;
        Debug.Log("Opponent chose: " + opponentChoice);
        Debug.Log("Result: " + result);
    }

    private async void Update()
    {
        // Check for incoming messages regularly
        if (Globals.IsServer)
        {
            (string receivedUsername, string OpponentCoice) = await MyServer.Instance.ReceiveMessageAsync();
            if (OpponentCoice != null)
            {
                // Trigger the event when a message is received
                OnMessageReceived?.Invoke(receivedUsername, OpponentCoice);
            }
        }
        else
        {
            (string receivedUsername, string OpponentCoice) = await MyClient.Instance.ReceiveMessageAsync();
            if (OpponentCoice != null)
            {
                // Trigger the event when a message is received
                OnMessageReceived?.Invoke(receivedUsername, OpponentCoice);
            }
        }
    }
}
