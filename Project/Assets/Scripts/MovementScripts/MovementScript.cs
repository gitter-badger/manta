using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MovementScript : MonoBehaviour {

	#region Variables
	public float speed = 5;
	public bool reached = false;
	private int moveIndex = 0;

	public List<GameObject> neighbors;
	public List<GameObject> openNodes;
	public List<GameObject> closedNodes;
	public GameObject curNode;
	public GameObject target;
	public List<GameObject> path;
	public List<GameObject> go;

	#endregion

	// Use this for initialization
	void Start () {
		CheckTile();
		openNodes.Add(curNode);
		path.Add(curNode);
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Node");
		foreach (GameObject node in tiles) {
			go.Add(node);
		}
		for (int i = 0; i < go.ToArray().Length; i++) {
			if(go.ToArray()[i].GetComponent<TileScript>().isWalkable == false){
				go.Remove(go.ToArray()[i]);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null){
            //print("Has Target");
            Vector3 dir = path.ToArray()[moveIndex].transform.position - transform.position;
            transform.LookAt(dir);
            if (target != curNode){
				CheckPath();
			}else{
				Move();
			}
		}

        
        //DebugColours();
    }

	public void CheckTile(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down, out hit, 3.0f)){
			if(hit.collider.tag == "Node"){
				curNode = hit.collider.gameObject;
			}
		}
	}

	void DebugColours(){

		for (int i = 0; i < closedNodes.ToArray().Length; i++) {
			closedNodes.ToArray()[i].GetComponent<Renderer>().material.color = Color.black;
		}

		for (int i = 0; i < path.ToArray().Length; i++) {
			path.ToArray()[i].GetComponent<Renderer>().material.color = Color.yellow;
		}	
	}

	void Move(){
		
		if(moveIndex != path.ToArray().Length){
			Vector3 pos = new Vector3(path.ToArray()[moveIndex].transform.position.x, transform.position.y, path.ToArray()[moveIndex].transform.position.z);
			transform.position = Vector3.MoveTowards(transform.position, pos, speed * Time.deltaTime);
			if(Vector3.Distance(transform.position, pos) < 0.05f){

               

                transform.position = pos;
				moveIndex++;
                //look at pos
            }
		}else{
			path.Clear();
			moveIndex = 0;
			path.Add(target);
			target = null;
			reached = false;

		}
	}

	void CheckPath(){
		//if(reached == false){
			//CheckNeighbors();
		//}else{
			neighbors.Clear();

			DirectPath();
		//}

		//make sure there is no duplicates
		neighbors = neighbors.Distinct().ToList();
		openNodes = openNodes.Distinct().ToList();
		closedNodes = closedNodes.Distinct().ToList();

		//add all the nodes used in the init. search to the possibilities to the direct search
		//ONLY WHEN USING CHECKPATH();
//		for (int i = 0; i < openNodes.ToArray().Length; i++) {
//			closedNodes.Add(openNodes.ToArray()[i]);
//		}

		//Check if the path has been reached
		for (int o = 0; o < closedNodes.ToArray().Length; o++) {
			if(closedNodes.ToArray()[o] == target){
				reached = true;
			}
		}
	}

	#region Check Path

	void CheckNeighbors(){
		neighbors.Clear();
		//Check which tiles are direct neighbors
		for (int i = 0; i < go.ToArray().Length; i++) {
			for (int x = 0; x < openNodes.ToArray().Length; x++) {
				if(Vector3.Distance(go.ToArray()[i].transform.position, openNodes.ToArray()[x].transform.position) <= 1.1f && go[i] != openNodes.ToArray()[x]){
					neighbors.Add(go.ToArray()[i]);
				}
			}
		}


		//Find the F values for all the neighbors
		int F = 0;
		int Findex = 100;
		openNodes.Clear();
		for (int i = 0; i < neighbors.ToArray().Length; i++) {
			float numX;
			float numZ;

			//x axis
			if(neighbors.ToArray()[i].transform.position.x > target.transform.position.x){
				numX = neighbors.ToArray()[i].transform.position.x - target.transform.position.x;
			}else{
				numX = target.transform.position.x - neighbors.ToArray()[i].transform.position.x;
			}
			if(numX < 0){
				numX *= -1;
			}

			//z axis
			if(neighbors.ToArray()[i].transform.position.z > target.transform.position.z){
				numZ = neighbors.ToArray()[i].transform.position.z - target.transform.position.z;
			}else{
				numZ = target.transform.position.z - neighbors.ToArray()[i].transform.position.z;
			}
			if(numX < 0){
				numZ *= -1;
			}

			//Get F
			F = (int)(numX + numZ);
			if(F == Findex){
				openNodes.Add(neighbors.ToArray()[i]);
			}else if(F < Findex){
				Findex = F;
				openNodes.Clear();
				openNodes.Add(neighbors.ToArray()[i]);
			}


		}

		//curNode = path.ToArray()[path.ToArray().Length - 1];
	}

	#endregion

	#region Direct Path

	void DirectPath(){
		//Check which tiles are direct neighbors
		//for (int x = 0; x < closedNodes.ToArray().Length; x++) {
			for (int i = 0; i < go.ToArray().Length; i++) {
				if(Vector3.Distance(go.ToArray()[i].transform.position, curNode.transform.position) <= 1f && go[i] != curNode ){
					if(path.ToArray().Length >= 2){
						if(go.ToArray()[i] != path.ToArray()[path.ToArray().Length - 2]){
							neighbors.Add(go.ToArray()[i]);
						}
					}else{
						neighbors.Add(go.ToArray()[i]);
					}
				}
			}
	//	}

		//Find the F values for all the neighbors
		int F = 0;
		int G = 1;
		int Findex = 100;
		openNodes.Clear();
		for (int i = 0; i < neighbors.ToArray().Length; i++) {
			float numX;
			float numZ;

			//x axis
			if(neighbors.ToArray()[i].transform.position.x > target.transform.position.x){
				numX = neighbors.ToArray()[i].transform.position.x - target.transform.position.x;
			}else{
				numX = target.transform.position.x - neighbors.ToArray()[i].transform.position.x;
			}
			if(numX < 0){
				numX *= -1;
			}

			//z axis
			if(neighbors.ToArray()[i].transform.position.z > target.transform.position.z){
				numZ = neighbors.ToArray()[i].transform.position.z - target.transform.position.z;
			}else{
				numZ = target.transform.position.z - neighbors.ToArray()[i].transform.position.z;
			}
			if(numX < 0){
				numZ *= -1;
			}
//
//			if(path.ToArray().Length > 2){
//				if(neighbors.ToArray()[i] == path.ToArray()[path.ToArray().Length - 2]){
//					neighbors.Remove(neighbors.ToArray()[i]);
//				}
//			}

			//Get F
			F = (int)(numX + numZ) + G;
			//print(F + " At: " + neighbors.ToArray()[i]);
			if(F == Findex){
				openNodes.Add(neighbors.ToArray()[i]);
			}else if(F < Findex){
				Findex = F;
				openNodes.Clear();
				openNodes.Add(neighbors.ToArray()[i]);
			}


		}

		//Chose which neighbor to be added to the path
		if(openNodes.ToArray().Length > 1){
			path.Add(openNodes.ToArray()[Random.Range(0, openNodes.ToArray().Length)]);
		}else{
			path.Add(openNodes.ToArray()[0]);
		}
			
		curNode = path.ToArray()[path.ToArray().Length - 1];
	}

	#endregion
}
