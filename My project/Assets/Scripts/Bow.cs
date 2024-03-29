using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject projectilePrefab;

    private Transform firePoint;

    public float projectileForce = 40f;
    public float maxChargeTime = 5f;

    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = gameObject.GetComponentInChildren<Transform>();
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
            Range_Weapon.Shoot(time, maxChargeTime, projectileForce, projectilePrefab, firePoint);
            time = 0;
        }

    }


}
