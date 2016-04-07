using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerListManager : NetworkBehaviour
{
	public List<NetworkInstanceId> playerList = new List<NetworkInstanceId> ();

	public NetworkManager netManager;

	public bool turnIsActive = false;

	private int currentPlayerNumber = 0;

	void Start ()
    {
        netManager = GameObject.FindWithTag ("NetworkManager").GetComponent<NetworkManager> ();
        TurnManager turnManagerScript = this.gameObject.AddComponent<TurnManager>();

        //Properties can be used just like variables
        turnManagerScript._currentPlayerNumber = 0;
        currentPlayerNumber = turnManagerScript._currentPlayerNumber;
    }
	
    [ClientRpc] public void RpcBeginClientConnect()
    {
        ClientConnect();
    }

	public void ClientConnect()
    {
        print("uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");
		CmdAddPlayer (netManager.client.connection.playerControllers[0].gameObject.GetComponent<NetworkIdentity>().netId);

	}

	[Command] public void CmdAddPlayer(NetworkInstanceId netId) //command that adds a player's networkID to a list when the player connects
	{
		playerList.Add (netId);
	}

    //Re-make the function on the turn manager script, so that the currentPlayerNumber can be referenced on this script with get/set
	[Command] public void CmdTellServerToSwitchTurn() //command that tells all players the turn has been switched to the next player
	{
        if(currentPlayerNumber == 0)
        {
            currentPlayerNumber++;
            RpcSwitchTurn(playerList[currentPlayerNumber]);
        }
        else if(currentPlayerNumber == 1)
        {
            currentPlayerNumber--;
            RpcSwitchTurn(playerList[currentPlayerNumber]);
        }
	}

	[ClientRpc] public void RpcSwitchTurn(NetworkInstanceId netid) //if the player's netID is the same as the currentPlayerNumber, that player can move/act, otherwise it can't until its netID matches
	{
		if (gameObject.GetComponent<NetworkIdentity> ().netId == netid)
		{
			turnIsActive = true;
		}
		else
		{
			turnIsActive = false;
		}
	}
}
