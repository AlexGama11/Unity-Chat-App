using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (IsOwner)
        {
            string isHost = IsHost ? "Hosting" : "Not Hosting";
            string isClient = IsClient ? "Joined Server" : "Client not Joined";
            Debug.Log(isHost + " / " + isClient);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
