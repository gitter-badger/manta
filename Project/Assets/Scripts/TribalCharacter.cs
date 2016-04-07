using UnityEngine;
using System.Collections;

public class TribalCharacter : MonoBehaviour
{

    public int maxHealth;
    public int curHealth;
    public int maxEnergy;
    public int curEnergy;
    public int damage;
   // public int turnInt;


	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        PassiveAbility();
	}

    void poisonDamage()
    {

    }

    void PassiveAbility()
    {
        float curH = curHealth;
        float maxH = maxHealth;
        float factor = 15 + (1 - (curH / maxH)) * 15;
        damage = (int)factor;
    }

}
