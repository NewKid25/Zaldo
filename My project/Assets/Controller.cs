using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    private float horizontal;
    private bool touch;

    public float jumpForce = 20f;
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && touch)
        {
            Jump();
        }

    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * (horizontal * speed));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) 
        touch = true;
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        touch = false;
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

}