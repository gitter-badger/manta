using UnityEngine;
using System.Collections;

public class suicideScript : MonoBehaviour
{
    public float time;
    public bool use;
	// Use this for initialization
	void Start ()
    {
        if (use)
        {
            StartCoroutine("suicide");
        }
	}
	
	IEnumerator suicide()
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}
