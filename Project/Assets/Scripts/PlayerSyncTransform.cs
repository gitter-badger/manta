using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncTransform : NetworkBehaviour
{
    [SerializeField]
    private float moveSpeed = 15.0f;
    [SerializeField]
    private float lerpSpeed = 15.0f;
    [SerializeField]
    private float positionThreshold = 0.5f;
    [SerializeField]
    private float rotationThreshold = 5.0f;

    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private Rigidbody rb;

    [SyncVar]
    private Vector3 syncPosition;
    [SyncVar]
    private Quaternion syncRotation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            playerInput();
        }

        transmitPosition();
        lerpPosition();

        transmitRotation();
        lerpRotation();
    }
    private void playerMovement(KeyCode _key)
    {   Vector3 position = rb.position;
        Quaternion rotation = rb.rotation;

        switch (_key)
        {
            case KeyCode.W:
                position += transform.forward * moveSpeed * Time.deltaTime;
                break;
            case KeyCode.A:
                position += -transform.right * moveSpeed * Time.deltaTime;
                break;
            case KeyCode.S:
                position += -transform.forward * moveSpeed * Time.deltaTime;
                break;
            case KeyCode.D:
                position += transform.right * moveSpeed * Time.deltaTime;
                break;
        }
        rb.MovePosition(position);
        rb.MoveRotation(rotation);
    }
    private void playerInput()
    {
        KeyCode[] keys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

        foreach (var key in keys)
        {
            if (Input.GetKey(key))
            {
                playerMovement(key);
            }
        }
    }

    private void lerpPosition()
    {
        if (!isLocalPlayer)
        {
            rb.position = Vector3.Lerp(rb.position, syncPosition, lerpSpeed * Time.deltaTime);
        }
    }

    private void lerpRotation()
    {
        if (!isLocalPlayer)
        {
            rb.rotation = Quaternion.Lerp(rb.rotation, syncRotation, lerpSpeed * Time.deltaTime);
        }
    }

    [ClientCallback] void transmitPosition()
    {
        if (isLocalPlayer && Vector3.Distance(rb.position, lastPosition) > positionThreshold)
        {
            CmdSendPositionToServer(rb.position);
            lastPosition = rb.position;
        }
    }

    [ClientCallback] void transmitRotation()
    {
        if (isLocalPlayer && Quaternion.Angle(rb.rotation, lastRotation) > rotationThreshold)
        {
            CmdSendRotationToServer(rb.rotation);
            lastRotation = rb.rotation;
        }
    }

    [Command] void CmdSendPositionToServer(Vector3 _position)
    {
        syncPosition = _position;
    }

    [Command] void CmdSendRotationToServer(Quaternion _rotation)
    {
        syncRotation = _rotation;
    }
}
