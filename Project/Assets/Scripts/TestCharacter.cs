using UnityEngine;
using System.Collections;

public class TestCharacter : PlayerClass{

    public int maxHealth;
    public int curHealth;

    public int maxEnergy;
    public int curEnergy;

	private float baseDamage = 15;
    public int totalDamage;
    


	// Use this for initialization
	void Start (){
		Health = curHealth;

	}
	
	// Update is called once per frame
	void Update (){

	}
}
