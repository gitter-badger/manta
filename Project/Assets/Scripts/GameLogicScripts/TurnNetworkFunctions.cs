using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TurnNetworkFunctions : NetworkBehaviour
{
    private int currentPlayerNumber;
    private bool canTakeTurn;

    private NetManager netManager;

    public List<NetworkInstanceId> playerIdList = new List<NetworkInstanceId>();

    void Start()
    {
        netManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetManager>();

       if (isLocalPlayer)
       {
            CmdAddPlayerToList(gameObject.GetComponent<NetworkIdentity>().netId);
        }
    }

    [Command]
    public void CmdAddPlayerToList(NetworkInstanceId playerId)
    {
        if (isServer)
        {
            playerIdList.Add(playerId);
        }
        
    }

    [Command]
    public void CmdTellServerToSwitchTurn() //Server command that changes the currentPlayerNumber
    {
        if (currentPlayerNumber == 0)
        {
            currentPlayerNumber++;

            if (isServer)
            {
                SwitchTurn(currentPlayerNumber);
            }
            else
            {
                RpcSwitchTurn(currentPlayerNumber);
            }
        }
        else if (currentPlayerNumber == 1)
        {
            currentPlayerNumber--;

            if (isServer)
            {
                SwitchTurn(currentPlayerNumber);
            }
            else
            {
                RpcSwitchTurn(currentPlayerNumber);
            }
        }
    }

    [ClientRpc]
    public void RpcSwitchTurn(int newPlayerNumber)
    {
        SwitchTurn(newPlayerNumber);
    }


    public void SwitchTurn(int newPlayernumber) //if the player's connID is the same as the currentPlayerNumber, that player can move/act, otherwise it can't until its connID matches
    {
        if (isServer)
        {

            print("switchingTurns");

            if (playerIdList[newPlayernumber] == gameObject.GetComponent<NetworkIdentity>().netId)
            {
                canTakeTurn = true;
            }
            else
            {
                canTakeTurn = false;
            }
        }

        /*for (int i = 0; i < netManager.players.Count; i++)
        {
            if (i == currentPlayerNumber)
            {
                canTakeTurn = true; //this bool should be referenced on each player script
            }
            else if (i != currentPlayerNumber)
            {
                canTakeTurn = false;
            }
        }*/
    }

    //make a function here for get/set the canTakeTurn
}
