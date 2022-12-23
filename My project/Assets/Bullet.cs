using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public GameObject hitEffect;
    public int weaponDamage = 1;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.04f);
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Killable killable = collision.GetComponent<Killable>();
        if (killable != null)
        {
            killable.TakeDamage(weaponDamage);
            Destroy(gameObject);
        }

        else
        {
            //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            //Destroy(effect, 5f);
            Destroy(gameObject);

            //   FindObjectOfType<AudioManager>().Play("Bullet Break");
        }

    }



}
