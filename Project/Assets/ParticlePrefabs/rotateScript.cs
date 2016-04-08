using UnityEngine;
using System.Collections;

public class rotateScript : MonoBehaviour 
{
    private float roteSpeed = 2.0f;
	// Use this for initialization
	void Start () 
	{
	    
	}
	
	// Update is called once per frame
	void Update () 
	{
        transform.Rotate(new Vector3(0, roteSpeed, 0));
	}
}
