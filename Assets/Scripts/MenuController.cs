using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public static class Globals
{
    public static string ipAddress;
    public static string port;
}

public class MenuController : MonoBehaviour
{

    public TMP_InputField ipField;
    public TMP_InputField portField;
    public GameObject serverObject;
    public GameObject clientObject;
    public Button hostButton;
    public Button joinButton;

    // Start is called before the first frame update
    void Start()
    {
        hostButton.onClick.AddListener(HostServer);
        joinButton.onClick.AddListener(OpenClient);
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        Globals.ipAddress = ipField.text;
        Globals.port = portField.text;
    }

    public void HostServer()
    {
        serverObject.SetActive(true);
    }

    public void OpenClient()
    {
        clientObject.SetActive(true);
    }
}
