using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Aiming : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;

    private Vector2 lookDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //same math as player aiming but using the player's position instead of the mouse's
        lookDir = playerRb.position - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}