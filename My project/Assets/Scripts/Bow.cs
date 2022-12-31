using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;

    public float projectileForce = 40f;
    public float maxChargeTime = 5f;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetButton("Fire1"))
        {
            time += Time.deltaTime;
            Debug.Log(time);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
          Shoot(time);
          time = 0;
        }
    }

    void Shoot(float time)
    {
  

        if (time > maxChargeTime)
            time = maxChargeTime;
        time /= maxChargeTime;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(projectileForce * time * firePoint.right, ForceMode2D.Impulse);

    }
}
