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

	// Use this for initialization
	void Start ()
    {

		netManager = GameObject.FindWithTag ("NetworkManager").GetComponent<NetworkManager> ();
	}
	
	// Update is called once per frame
	void OnClientConnected ()
    {

		CmdAddPlayer (gameObject.GetComponent<NetworkIdentity> ().netId);

	}

	[Command] public void CmdAddPlayer(NetworkInstanceId netId)
	{
		playerList.Add (netId);
	}


	[Command] public void CmdTellServerToSwitchTurn() //command that tells all players the turn has been switched to the next player
	{
		currentPlayerNumber++;

		RpcSwitchTurn (playerList [currentPlayerNumber]);
	}

	[ClientRpc] public void RpcSwitchTurn(NetworkInstanceId netid) //
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
