using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bow : MonoBehaviour
{
    public GameObject projectilePrefab;

    private Transform firePoint;

    public float projectileForce = 40f;
    public float maxChargeTime = 5f;

    private float time = 0;


    private void Start()
    {
        firePoint = gameObject.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        //Debug.Log(time);
        if (time >= maxChargeTime)
        {
            Range_Weapon.Shoot(time, maxChargeTime, projectileForce, projectilePrefab, firePoint);
            time = 0;
        }
    }
}
