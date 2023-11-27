using UnityEngine;
using TMPro;
using System.Net;
using System;
using Button = UnityEngine.UI.Button;

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
            Globals.ipAddress = IPAddress.Parse(ipField.text);
            Globals.port = Int32.Parse(portField.text);
            Globals.Username = userField.text;
        }

    }

    public void HostServer()
    {
        GetInput();
        serverObject.SetActive(true);
        MyServer.Instance.CreateServer();
        chatPanel.SetActive(true);
        menuPanel.SetActive(false);
        Globals.isServer = true;
    }

    public void OpenClient()
    {
        GetInput();
        clientObject.SetActive(true);
        MyClient.Instance.ConnectToServer();
        chatPanel.SetActive(true);
        menuPanel.SetActive(false);
        Globals.isServer = false;
    }
}
