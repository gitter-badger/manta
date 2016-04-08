using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), (typeof(Animator)))]
public class Char : MonoBehaviour
{
    [SerializeField]
    private Rigidbody smallJellyfish;
    [SerializeField]
    private float projectileSpeed = 10.0f;
    [SerializeField]
    private Transform spawner;

    private Animator jellyfishGirlAnim;
    private PlayerClass charB;

    void Start()
    {
        charB = new PlayerClass();
        charB.Health = 50;
        charB.Energy = 20;

        GameObject shield = transform.Find("JellyfishKing").gameObject;
        shield.AddComponent<BoxCollider>();
        BoxCollider shieldCol = shield.GetComponent<BoxCollider>();
        shieldCol.GetComponent<Renderer>().enabled = false;
        shieldCol.isTrigger = false;
        shield.transform.SetParent(this.transform);
    }
    public void takeDamage(int damage)
    {
        charB.Health -= damage;
        jellyfishKingShield(1, 25); // numbers between 1-25 will activate the shield; 25 percent chance of activating
    }

    private void jellyfishKingShield(int minimum, int maximum)
    {
        int getNumber = Random.Range(0, 100);
        if (getNumber > minimum && getNumber < maximum)
        {
            // play shield animation
            // jellyfishGirlAnim.SetTrigger("Shield");

            // scale jellyfish king to the character size
            GameObject shield = transform.Find("JellyfishKing").gameObject;
            shield.AddComponent<BoxCollider>();
            BoxCollider shieldCol = shield.GetComponent<BoxCollider>();
           // shieldCol.radius = 1f; // this probably doesnt work
        }
    }

    private void jellyfishWhip(int damage, int minimum, int maximum) // on button click
    {
        // play jellyfishwhip animation
        // set a vector distance to scale for melee(minimum) as well as longrange(maximum) damage
        // inflict damage
    }
    private void jellyfishShot(int damage) // on button click
    {
        // play jellyfishshot animation
        // instantiate small jellyfish
        Rigidbody clone;
        clone = Instantiate(smallJellyfish, spawner.position, spawner.rotation) as Rigidbody;
        clone.AddForce(transform.forward * projectileSpeed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.SphereCast(spawner.position, 1f, transform.forward, out hit, 0))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            { 
                // get reference to enemy health and deduce its health using the local damage variable
            }
        }
    }
}
