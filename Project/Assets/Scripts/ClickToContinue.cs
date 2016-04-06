using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickToContinue : MonoBehaviour {

	public int targetSceneIndex;
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKey(KeyCode.Mouse0))
		{
			SceneManager.LoadScene (targetSceneIndex);
		}
	}
}
