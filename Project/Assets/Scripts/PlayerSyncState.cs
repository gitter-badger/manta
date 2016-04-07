using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncState : NetworkBehaviour
{
    [SerializeField]
    private const int maxHealth = 50;
    [SyncVar] public int currentHealth;

    [SerializeField]
    private const int maxStamina = 15;
    [SyncVar] public int currentStamina;

    void Awake()
    {
        currentHealth = maxHealth;
        currentStamina = maxStamina;
    }

    public void takeDamage(int damageTaken) // reference this function from another script for the damage to be inflicted
    {
        if (!isServer)
            return;

        currentHealth -= damageTaken;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            transmitPlayerDeadState();
        }
    }

    public void staminaDecrease(int staminaCost) // reference this function from another script for the stamina to be decreased
    {
        if (!isServer)
            return;

        currentStamina -= staminaCost;
        if (currentStamina <= 0)
        {
            currentStamina = 0;
            transmitPlayerStaminaState();
        }
    }

    [ClientCallback]
    void transmitPlayerDeadState()
    {
        if (!isLocalPlayer)
            return;

            CmdPlayerDead();
    }

    [Command]
    void CmdPlayerDead()
    {
        Network.Destroy(GetComponent<NetworkView>().viewID);
    }


    [ClientCallback]
    void transmitPlayerStaminaState()
    {
        if (!isLocalPlayer)
            return;

            CmdPlayerStamina();
    }

    [Command]
    void CmdPlayerStamina()
    {
        // end turn
    }
}
