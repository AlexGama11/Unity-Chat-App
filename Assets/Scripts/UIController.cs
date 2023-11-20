using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject LoginView;
    public GameObject ChatView;

    public static UIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //TCPServer.OnServerCreated += ShowChat;
        ShowLogin();
        
    }

    void Update()
    {
        
    }

    public void ShowLogin()
    {
        LoginView.SetActive(true);
        ChatView.SetActive(false);
    }

    public void ShowChat()
    {
        LoginView.SetActive(false);
        ChatView.SetActive(true);
    }

}
