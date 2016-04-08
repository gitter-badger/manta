using UnityEngine;
using System.Collections;

public class MonuAnimation : MonoBehaviour
{
    private Animator animCtrl;
    private enum AnimationSelect { Idle = 1, Walk = 2, PushAttack = 3, MeleeAttack = 4, TakeHit = 5, FortifyAbility = 6, Death = 7 };
    [SerializeField]
    private AnimationSelect animSelect;

    void Awake()
    {
        animCtrl = GetComponent<Animator>();
        animSelect = AnimationSelect.Idle;
    }
    void Update()
    {
        KeyCode[] keys =
        { KeyCode.Alpha0,
          KeyCode.Alpha1,
          KeyCode.Alpha2,
          KeyCode.Alpha3,
          KeyCode.Alpha4,
          KeyCode.Alpha5,
          KeyCode.Alpha6,
          KeyCode.Alpha7
        };

        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                keyInput(key);
            }
        }
    }
    private void keyInput(KeyCode _key)
    {
        switch (_key)
        {
            case KeyCode.Alpha1:
                animCtrl.SetInteger("LifeCount", 50);
                animCtrl.Play("Idle", 1);
                animCtrl.SetBool("Walk", false);
                break;
            case KeyCode.Alpha2:
                walk();
                break;
            case KeyCode.Alpha3:
                pushAttack();
                break;
            case KeyCode.Alpha4:
                meleeAttack();
                break;
            case KeyCode.Alpha5:
                takeHit();
                break;
            case KeyCode.Alpha6:
                fortifyAbility();
                break;
            case KeyCode.Alpha7:
                death();
                break;
        }
    }

    private void death()
    {
        animCtrl.SetInteger("LifeCount", 0);
    }

    private void fortifyAbility()
    {
        animCtrl.SetTrigger("Fortify");
    }

    private void pushAttack()
    {
        animCtrl.SetTrigger("PushAttack");
    }

    private void meleeAttack()
    {
        animCtrl.SetTrigger("MeleeAttack");
    }

    private void walk()
    {
        animCtrl.SetBool("Walk", true);
    }

    private void takeHit()
    {
        animCtrl.SetTrigger("DamageTaken");
    }
}
