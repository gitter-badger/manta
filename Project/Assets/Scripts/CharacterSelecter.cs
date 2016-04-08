using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelecter : MonoBehaviour {

	public Image characterPreviewImg;
	public Text characterName;
	public Sprite[] characterImgs;
	public string[] characterNames;
	public int gameScene;

	public GameObject networkManager;

	private NetworkMode networkMode;
	private int currentImgNumber = 0;

	// Use this for initialization
	void Start () {
	
		networkMode = networkManager.GetComponent<NetworkMode> ();

		PlayerPrefs.DeleteKey("PlayerNumber");

		characterPreviewImg.sprite = characterImgs [currentImgNumber];
		characterName.text = characterNames [currentImgNumber];
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CycleCharacterMenu(int direction)
	{
		currentImgNumber += direction;

		if (currentImgNumber < 0) 
		{
			currentImgNumber = characterImgs.Length - 1;
		}

		if (currentImgNumber >= characterImgs.Length)
		{
			currentImgNumber = 0;
		}

		characterPreviewImg.sprite = characterImgs [currentImgNumber];
		characterName.text = characterNames [currentImgNumber];
	}

	public void SetPlayerNumber()
	{
		PlayerPrefs.SetInt ("PlayerNumber", currentImgNumber);

		networkMode.StartGame ();
	}
		
}
