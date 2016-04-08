﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class TurnManager : MonoBehaviour

{
    //public NetworkPlayer[] playerList; 
    private PlayerListManager playerManager;
    public NetManager netManager;
    private TurnNetworkFunctions turnNetfunctions;

    public int turncounter;
    public int currentPlayerNumber;
    private int playerConnID;

    public Timer countdownTimer;
    public bool endTurn;

    public List<NetworkPlayer> players;

    public enum characterTurns
    {
        CHARACTER1,
        CHARACTER2,
        //CHARACTER3,
        //CHARACTER4
    }
    public characterTurns currentState;

    void Start()
    {
        //List<NetworkPlayer> players = new List<NetworkPlayer> ();
        //playerList = Network.connections;
        //print (playerList[0]);
        turncounter = 1;
        currentPlayerNumber = 0;
        //ChangeTurnToC1();
        currentState = characterTurns.CHARACTER1;
        countdownTimer = GameObject.FindWithTag("TimerObject").GetComponent<Timer>();

        playerManager = gameObject.GetComponent<PlayerListManager>();
        //netManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<NetManager>();
       

      
    }

    void Update()
    {
        if (gameObject.GetComponent<NetworkIdentity>().isServer)
        { 

            if (turnNetfunctions == null)
            {
                GameObject[] potentialPlayers = GameObject.FindGameObjectsWithTag("Player");

                for (int i = 0; i < potentialPlayers.Length; i++)
                {
                    if (potentialPlayers[i].GetComponent<TurnNetworkFunctions>().isLocalPlayer)
                    {
                        turnNetfunctions = potentialPlayers[i].GetComponent<TurnNetworkFunctions>();
                    }
                }
            }

            countdownTimer.startTime();
            //2 players, each chooses one character to play each
            switch (currentState)
            {
                case characterTurns.CHARACTER1:
                    {
                        currentPlayerNumber = 0;
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            print("p1 space");
                        }
                        if (countdownTimer.timer == countdownTimer.resetValue || endTurn == true)  //condition for switching turns
                        {
                            turncounter++;
                            Debug.Log("player1");
                            turnNetfunctions.CmdTellServerToSwitchTurn();
                            currentState = characterTurns.CHARACTER2;
                            endTurn = false;
                        }
                    }
                    break;
                case characterTurns.CHARACTER2:
                    {

                        currentPlayerNumber = 1;
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            print("p2 space");
                        }
                        if (countdownTimer.timer == countdownTimer.resetValue || endTurn == true) //condition for switching turns
                        {

                            turncounter--;
                            Debug.Log("player2");
                            turnNetfunctions.CmdTellServerToSwitchTurn();
                            currentState = characterTurns.CHARACTER1;
                            endTurn = false;
                        }
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public int _currentPlayerNumber
    {
        get { return currentPlayerNumber; }
        set { currentPlayerNumber = value; }
    }

    public int _turnCounter
    {
        get { return turncounter; }
        set { turncounter = value; }
    }

    public void endMyTurn()
    {
        endTurn = true;
        countdownTimer.timer = countdownTimer.resetValue;
    }

    //old functions
    /*
    private void ChangeTurnToC1() //function for switching to character 1's turn
    {
        char1Turn = true;
        char2Turn = false;
        char3Turn = false;
        char4Turn = false;
    }

    private void ChangeTurnToC2() //function for switching to character 2's turn
    {
        char1Turn = false;
        char2Turn = true;
        char3Turn = false;
        char4Turn = false;
    }

    private void ChangeTurnToC3() //function for switching to character 3's turn
    {
        char1Turn = false;
        char2Turn = false;
        char3Turn = true;
        char4Turn = false;
    }

    private void ChangeTurnToC4() //function for switching to character 4's turn
    {
        char1Turn = false;
        char2Turn = false;
        char3Turn = false;
        char4Turn = true;
    }*/

}
