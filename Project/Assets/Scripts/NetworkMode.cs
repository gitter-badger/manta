using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkMode : MonoBehaviour {


	public InputField addressInputField;

	private NetworkManager networkManager;

	//0 = Host
	//1 = Client
	//2 = Server Only
	private int netModeIndex;
	private string networkAddress = "localhost";

	void Start()
	{
		networkManager = gameObject.GetComponent<NetworkManager> ();
	}

	public void SetNetModeIndex(int index)
	{
		netModeIndex = index;
	}

	public void SetNetworkAddress()
	{
		if (addressInputField.text.Length <= 0) 
		{
			networkAddress = "localhost";
		} 
		else 
		{
			networkAddress = addressInputField.text.ToString ();
		}
	}

	public void StartGame()
	{
		networkManager.networkAddress = networkAddress;

		if (netModeIndex == 0) {
			networkManager.StartHost ();
		}

		if (netModeIndex == 1) {
			networkManager.StartClient ();
		}

		if (netModeIndex == 1) {
			networkManager.StartServer ();
		}
	}
}
