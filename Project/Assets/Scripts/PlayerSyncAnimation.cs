using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncAnimation : NetworkBehaviour
{
    private Animator anim;
    private bool isJumping = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    void Update()
    {
        if (!isLocalPlayer)
            return;

        jumping();
    }

    private void jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.enabled = true;
            isJumping = true;
            anim.SetBool("Jump", true);
            StartCoroutine(JumpTime());
        }

        if (isJumping == false)
        {
            anim.enabled = false;
        }
    }

    IEnumerator JumpTime()
    {
        yield return new WaitForSeconds(0.33f);
        isJumping = false;
        anim.SetBool("Jump", false);
    }
}
