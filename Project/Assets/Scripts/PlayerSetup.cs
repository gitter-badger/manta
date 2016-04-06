using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerSetup : NetworkBehaviour
{
    private Rigidbody rb;
    private GameObject create;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints =    RigidbodyConstraints.FreezeRotationX |
                            RigidbodyConstraints.FreezeRotationY |
                            RigidbodyConstraints.FreezeRotationZ;

        AudioListener audioList = GetComponentInChildren<AudioListener>();
        Camera cam = GetComponentInChildren<Camera>();

        if (isLocalPlayer)
        {
            audioList.enabled = true;
            cam.enabled = true;
        }

        else
        {
            audioList.enabled = false;
            cam.enabled = false;
        }
    }
}
