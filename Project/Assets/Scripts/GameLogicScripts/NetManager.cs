using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetManager : NetworkManager
{
    private TurnNetworkFunctions turnNetfunctions;

    public List<int> players = new List<int>();

	public override void OnServerConnect(NetworkConnection conn)
    {

        bool match = false;

        for (int i = 0; i < players.Count; i++)
        {
            if (conn.connectionId == i)
            {
                match = true;
            }
        }

        if (match == false)
        {
            players.Add(conn.connectionId);
        }

        //client.connection.playerControllers[0].gameObject.GetComponent<PlayerListManager>().RpcBeginClientConnect();
    }

    void Update()
    { 

    }

    /*void LoopFunction()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (i == currentPlayerNumber)
            {
                canMove = true;
            }
            else if (i != currentPlayerNumber)
            {
                canMove = false;
            }
        }
    }*/
}
