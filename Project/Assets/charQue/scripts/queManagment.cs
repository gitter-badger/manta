using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class queManagment : PlayerClass {

    private int health;
    private int energy;
    private int damage;
    private GameObject enemy;
    private combatSystem CS;

	void Start () {
        PlayerClass PC = new PlayerClass();
        health = PC.Health;
        energy = PC.Energy;
        damage = PC.Damage;
    }

    void Update () {
        enemy = CS.enemy;
    }

    void activeAbilityOne() {
        //charm, prevent selected enemy from attacking que
    }
    void activeAbilityTwo() {
        //stronger attack to enemy with mid ranged attack (fire) up to 3 units in distance
    }
    void passiveAbility() {
        //normal attack is weaker with teh ability to increase que's HP by 30% of damage
    }
    void checkEnergy() {

    }
    void checkRange() {

    }
 }