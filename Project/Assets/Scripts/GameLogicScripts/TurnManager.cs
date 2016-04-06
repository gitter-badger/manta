using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurnManager
{
    //public NetworkPlayer[] playerList; 

    public int turncounter; 

    public bool char1Turn;
    public bool char2Turn;
    public bool char3Turn;
    public bool char4Turn;
    public List<NetworkPlayer> players;

    public enum characterTurns
    {
        CHARACTER1,
        CHARACTER2,
        CHARACTER3,
        CHARACTER4
    }
    public characterTurns currentState;

    void Start()
    {
        //List<NetworkPlayer> players = new List<NetworkPlayer> ();
        //playerList = Network.connections;
        //print (playerList[0]);
        turncounter = 1;
        //ChangeTurnToC1();
        currentState = characterTurns.CHARACTER1;
    }

    void Update()
    {
        //2 players, each chooses one character to play each
        switch (currentState)
        {
            case characterTurns.CHARACTER1:
                {

                    if (Input.GetKeyDown(KeyCode.Space)) //condition for switching turns
                    {
                        turncounter++;
                        currentState = characterTurns.CHARACTER2;
                    }
                }
                break;
            case characterTurns.CHARACTER2:
                {
                    if (Input.GetKeyDown(KeyCode.Space)) //condition for switching turns
                    {
                        turncounter++;
                        currentState = characterTurns.CHARACTER3;
                    }
                }
                break;
            /*case characterTurns.CHARACTER3:
                {

                    if (Input.GetKeyDown(KeyCode.Space)) //condition for switching turns
                    {
                        turncounter++;
                        ChangeTurnToC4();
                        currentState = characterTurns.CHARACTER4;
                    }
                }
                break;
            case characterTurns.CHARACTER4:
                {

                    if (Input.GetKeyDown(KeyCode.Space)) //condition for switching turns
                    {
                        turncounter++;
                        ChangeTurnToC1();
                        currentState = characterTurns.CHARACTER1;
                    }
                }
                break;*/
            default:
                break;
        }
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
    }

    //test function for get/set
    public bool getBool
    {
        get
        {
            return char1Turn;
        }
        set
        {
            char1Turn = value;
        }
    }*/
}
