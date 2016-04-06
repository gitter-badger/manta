using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSyncState : NetworkBehaviour
{
    [SerializeField]
    private const int maxHealth = 100;
    [SyncVar] public int currentHealth = maxHealth;

    [SerializeField]
    private const int maxStamina = 100;
    [SyncVar] public int currentStamina = maxStamina;

    void Update()
    {
        if(isLocalPlayer)
          takeDamage(1);
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
        }
    }

    [ClientCallback]
    void transmitPlayerDeadState()
    {
        CmdPlayerDead();
    }

    [Command]
    void CmdPlayerDead()
    {
        if (isLocalPlayer)
        {
            Network.Destroy(GetComponent<NetworkView>().viewID);
        }
    }
}
