using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Globals
{
    public static string IpAddress;
    public static string Port;
    public static string Message;
}

public class MenuController : MonoBehaviour
{
    public NetworkManager NetworkObject;
    public Button HostButton;
    public TMP_InputField IpField;
    public Button JoinButton;
    public TMP_InputField PortField;

    // Start is called before the first frame update
    private void Start()
    {
        HostButton.onClick.AddListener(HostServer);
        JoinButton.onClick.AddListener(OpenClient);
    }

    // Update is called once per frame
    private void Update() => GetInput();

    private void GetInput()
    {
        Globals.IpAddress = IpField.text;
        Globals.Port = PortField.text;
    }

    public void HostServer()
    {
        if (NetworkObject != null && int.TryParse(Globals.Port, out var port))
        {
            Debug.Log("Creating Server!!!");
            NetworkObject.StartNetworking(true, Globals.IpAddress, port);
            Debug.Log("Created Server!!!");
        }

        else
        {
            Debug.LogError("Invalid port number: " + Globals.Port);
        }
    }

    public void OpenClient()
    {
        if (NetworkObject != null && int.TryParse(Globals.Port, out var port))
        {
            NetworkObject.StartNetworking(false, Globals.IpAddress, port);
        }

        else
        {
            Debug.LogError("Invalid port number: " + Globals.Port);
        }
    }
}
