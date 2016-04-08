using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TurnNetworkFunctions : NetworkBehaviour
{
    private int currentPlayerNumber;
    private bool canTakeTurn;

    private NetManager netManager;

    void Start()
    {
        netManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetManager>();
    }

    [Command]
    public void CmdTellServerToSwitchTurn() //Server command that changes the currentPlayerNumber
    {
        if (currentPlayerNumber == 0)
        {
            currentPlayerNumber++;
            RpcSwitchTurn();
        }
        else if (currentPlayerNumber == 1)
        {
            currentPlayerNumber--;
            RpcSwitchTurn();
        }
    }

    [ClientRpc]
    public void RpcSwitchTurn() //if the player's connID is the same as the currentPlayerNumber, that player can move/act, otherwise it can't until its connID matches
    {
        for (int i = 0; i < netManager.players.Count; i++)
        {
            if (i == currentPlayerNumber)
            {
                canTakeTurn = true; //this bool should be referenced on each player script
            }
            else if (i != currentPlayerNumber)
            {
                canTakeTurn = false;
            }
        }
    }

    //make a function here for get/set the canTakeTurn
}
