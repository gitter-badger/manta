using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class NetManager : NetworkManager
{
    private TurnNetworkFunctions turnNetfunctions;

    public List<NetworkInstanceId> playerIdList = new List<NetworkInstanceId>();
}
