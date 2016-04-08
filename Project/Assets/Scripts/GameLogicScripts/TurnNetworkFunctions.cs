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

       if (isLocalPlayer)
       {
            CmdAddPlayerToList(gameObject.GetComponent<NetworkIdentity>().netId);
        }
    }

    void Update()
    {
        if(canTakeTurn)
        {
            print("can take turn" + netId);
        }
        else if(canTakeTurn == false)
        {
            print("can't take turn" + netId);
        }
    }

    [Command]
    public void CmdAddPlayerToList(NetworkInstanceId playerId)
    {
        
        netManager.playerIdList.Add(playerId);
        
    }

    [Command]
    public void CmdTellServerToSwitchTurn() //Server command that changes the currentPlayerNumber
    {
        if (currentPlayerNumber == 0)
        {
            currentPlayerNumber++;

            
            SwitchTurn(currentPlayerNumber);
            
          
            
        }
        else if (currentPlayerNumber == 1)
        {
            currentPlayerNumber--;

            SwitchTurn(currentPlayerNumber);

           
        }
    }

    
    public void SwitchTurn(int newPlayerNumber)
    {
        RpcSwitchTurn(netManager.playerIdList[currentPlayerNumber]);
    }

    [ClientRpc]
    public void RpcSwitchTurn(NetworkInstanceId newPlayerId) //if the player's connID is the same as the currentPlayerNumber, that player can move/act, otherwise it can't until its connID matches
    {


            print("switchingTurns " + "new player id: " + newPlayerId.Value + " player id: " + gameObject.GetComponent<NetworkIdentity>().netId.Value);

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (newPlayerId == players[i].GetComponent<NetworkIdentity>().netId)
            {
                players[i].GetComponent<TurnNetworkFunctions>().canTakeTurn = true;
            }
            else
            {
                players[i].GetComponent<TurnNetworkFunctions>().canTakeTurn = false;
            }
        }

        if (newPlayerId == gameObject.GetComponent<NetworkIdentity>().netId)
        {
            canTakeTurn = true;
        }
        else
        {
            canTakeTurn = false;
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
