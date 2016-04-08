using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {

	public bool isWalkable = true;
	public MovementScript player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementScript>();
	}

	void OnMouseDown(){
		if(isWalkable){
			player.target = this.gameObject;
			player.CheckTile();
		}
	}
}
