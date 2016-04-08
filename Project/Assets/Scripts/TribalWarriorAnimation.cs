using UnityEngine;
using System.Collections;

public class TribalWarriorAnimation : MonoBehaviour
{
    private Animator animCtrl;

    void Start()
    {
        animCtrl = GetComponent<Animator>();
    }
    void Update()
    {
        animCtrl.SetTrigger("Attack");
    }
}
