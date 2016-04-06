using UnityEngine;
using System.Collections;

public class MenuCanvasSwitch : MonoBehaviour {

	public GameObject startMenuCanvas;
	public GameObject characterSelectCanvas;

	public void SwitchToCharacterSelect()
	{
		startMenuCanvas.SetActive (false);
		characterSelectCanvas.SetActive (true);
	}
}
