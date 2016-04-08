using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour

{
    #region Variables
    private NetManager netManager;
    private TurnNetworkFunctions turnNetfunctions;

    private int turncounter;
    private int currentPlayerNumber;
    private int playerConnID;

    //Timer variables
    private float timer = 10f; //Change these values to change the time
    private float resetValue = 10f;
    private bool endTurn;
    private Text timerText;

    private enum characterTurns
    {
        CHARACTER1,
        CHARACTER2,
    }
    private characterTurns currentState;
    #endregion

    void Start()
    {
        turncounter = 1;
        currentPlayerNumber = 0;
        currentState = characterTurns.CHARACTER1;

        timerText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<Text>();
    }

    void Update()
    {
        HandleTurns();
    }

    public void HandleTurns() //This runs on the server, and handles the character states
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

            startTime();
            //2 players, each chooses one character to play each
            switch (currentState)
            {
                case characterTurns.CHARACTER1:
                    {
                        currentPlayerNumber = 0;
                        if (timer == resetValue || endTurn == true)  //condition for switching turns
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
                        if (timer == resetValue || endTurn == true) //condition for switching turns
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

    public void endMyTurn() //Function to be called on an End Turn button
    {
        endTurn = true;
        timer = resetValue;
    }

    public void startTime()
    {
        timer -= Time.deltaTime;
        timerText.text = ((int)timer).ToString("f0");
        if (timer <= 0)
        {
            timer = resetValue;
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
}
