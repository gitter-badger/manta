using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct EnemyInfo{
	public GameObject enemy;
	public int turnCount;
}

public class TribalCharacter : PlayerClass{

    public int maxHealth;
    public int curHealth;

    public int maxEnergy;
    public int curEnergy;

	private float baseDamage = 15;
    public int totalDamage;
    
	public int turnInt = 3;

	public List <EnemyInfo> poisonoedPlayers;

	// Use this for initialization
	void Start (){
		Health = curHealth;

	}
	
	// Update is called once per frame
	void Update (){
		
        PassiveAbility();
		CheckHealth();

//		for (int i = 0; i < poisonoedPlayers; i++) {
//			
//		}
	}

    void PoisonAbility(){
		
		//poisonoedPlayers.Add(enemy, 3);

    }

    void PassiveAbility(){
        float curH = curHealth;
        float maxH = maxHealth;
		totalDamage = (int)(baseDamage + (1 - (curH / maxH)) * baseDamage);
    }

	void CheckStats(){

	}

	void CheckHealth(){
		if(curHealth > maxHealth){
			curHealth = maxHealth;
		}
		if(curHealth <= 0){
			//Death Function
		}
	}
}
