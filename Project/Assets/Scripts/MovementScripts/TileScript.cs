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
<<<<<<< HEAD
			player.target = this.gameObject;
			player.CheckTile();
=======
            if (player.target == null)
            {
                player.target = this.gameObject;
                player.CheckTile();
            }
>>>>>>> leveldesign-gm-sg
		}
	}
}
