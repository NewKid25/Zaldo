using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Weapon : MonoBehaviour
{

    public static void Shoot(float time, float maxChargeTime, float projectileForce, GameObject projectilePrefab, Transform firePoint)
    {


        if (time > maxChargeTime)
            time = maxChargeTime;
        time /= maxChargeTime;

        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(projectileForce * time * firePoint.right, ForceMode2D.Impulse);

    }

}
