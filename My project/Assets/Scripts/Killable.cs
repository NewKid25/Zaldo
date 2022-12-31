using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public int health = 1;

    //public GameObject deathEffect1;

    //public float throwForce = .015f;
    //private GameObject gun;

    void Start()
    {
       // HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>();
       // Rigidbody2D gunRb = joint.connectedBody;
        //gun = gunRb.gameObject;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die ()
    {
        //Instantiate(deathEffect1, transform.position, Quaternion.identity);
        Destroy(gameObject);
      /*  gun.GetComponent<weapon_aiming>().enabled = false;
        gun.GetComponent<weapon_shooting>().enabled = false;
        gun.GetComponent<enemy_aiming>().enabled = false;
        gun.GetComponent<BoxCollider2D>().enabled = true;
        Rigidbody2D gunRb = gun.GetComponent<Rigidbody2D>();
        gunRb.AddForce(Vector2.up * throwForce, ForceMode2D.Impulse);
        gunRb.AddTorque(Random.Range(-.01f, .01f), ForceMode2D.Impulse);

        FindObjectOfType<AudioManager>().Play("Death");*/
    }
}