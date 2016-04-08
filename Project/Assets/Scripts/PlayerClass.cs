using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour {

	#region Variables

	//public string username;
	private int maxHealth;
	private int curHealth;
	private int maxEnergy;
	private int curEnergy;
	private int damage;

//	private int defence;
//	private int critChance;

	#endregion

	#region Unity Functions

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	#endregion

	#region Created Functions

	public int Health {
		get { return curHealth; }
		set { curHealth = value; }
	}

	public int Damage {
		get { return damage; }
		set { damage = value; }
	}

	public int Energy {
		get { return curEnergy; }
		set { curEnergy = value; }
	}

//	public void TakeDamage(int dam){
//		int chance = Random.Range(0, 100);
//		if(chance <= critChance){
//			curHealth -= dam;
//		}else{
//			curHealth -= (int)(((100 - defence) / 100) * dam);
//		}
//	}

	public void GetParalized(){
		Energy = Energy / 2;
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Para"){
			GetParalized();
			print("test : " + Energy);
		}
	}

	#endregion

}
