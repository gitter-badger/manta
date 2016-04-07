using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

	public float timer;
	public float resetValue;
    public Text timerText;

	// Use this for initialization
	void Start () 
	{
		
        

	}
	
	// Update is called once per frame
	void Update () 
	{
        startTime();
    }

	public void startTime ()
	{
        timer -= Time.deltaTime;
        timerText.text = ((int)timer).ToString("f0");
       if (timer <= 0) 
	    {
        timer = resetValue;
        } 
	}
}
