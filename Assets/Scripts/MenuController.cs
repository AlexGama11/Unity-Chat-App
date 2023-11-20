using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Net;
using System;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public static class Globals
{
    public static string Username;
    public static string ipAddressString;
    public static IPAddress ipAddress;
    public static int port;
    public static Boolean isServer;
    public static Boolean isConnected = false;
}

public class MenuController : MonoBehaviour
{

    public TMP_InputField ipField;
    public TMP_InputField portField;
    public TMP_InputField userField;
    public GameObject serverObject;
    public GameObject clientObject;
    public Button hostButton;
    public Button joinButton;

    public GameObject chatPanel;
    public GameObject menuPanel;

    // Start is called before the first frame update
    private void Start()
    {
        hostButton.onClick.AddListener(HostServer);
        joinButton.onClick.AddListener(OpenClient);
    }

    private void GetInput()
    {

        if (ipField.text != string.Empty && portField.text != string.Empty)
        {
            Globals.ipAddressString = ipField.text;
            Globals.ipAddress = IPAddress.Parse(Globals.ipAddressString);
            Globals.port = Int32.Parse(portField.text);
            Globals.Username = userField.text;
        }

    }

    public void HostServer()
    {
        GetInput();
        serverObject.SetActive(true);
        Server.Instance.CreateServer();
        if (Globals.isConnected)
        {
            chatPanel.SetActive(true);
            menuPanel.SetActive(false);
            Globals.isServer = true;
        }
    }

    public void OpenClient()
    {
        GetInput();
        clientObject.SetActive(true);
        MyClient.Instance.ConnectToServer();
        if (Globals.isConnected)
        {
            chatPanel.SetActive(true);
            menuPanel.SetActive(false);
            Globals.isServer = false;
        }
    }
}
