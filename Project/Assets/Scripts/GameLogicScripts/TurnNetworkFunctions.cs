using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class TurnNetworkFunctions : NetworkBehaviour
{
    //This script should be on all player objects

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
    public void CmdTellServerToSwitchTurn() //Command that changes the currentPlayerNumber
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
    public void RpcSwitchTurn(NetworkInstanceId newPlayerId) 
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
    }

    public bool _canTaketurn //This is used on character scripts to indicate if the player can move/take actions this turn
    {
        get { return canTakeTurn; }
        set { canTakeTurn = value; }
    }
}
