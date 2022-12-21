using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private float horizontal;
    private bool touch;
    private float sqrSpeedCap;
    private bool isFlipped;

    public float jumpForce = 20f;
    public float moveForce = 20f;
    public float speedCap = 30f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        sqrSpeedCap = speedCap * speedCap;
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
        rb.AddForce(Vector2.right * (horizontal * moveForce));

        if (horizontal < 0 && isFlipped == false)
        {
            Flip();
        }
        else if (horizontal > 0 && isFlipped == true)
        {
            Flip();
        }
        //gameObject.transform.position += new Vector3(horizontal * speed,0f, 0f);

        if (rb.velocity.sqrMagnitude > sqrSpeedCap)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedCap);

        Debug.Log(rb.velocity.sqrMagnitude);
    }


    private void OnCollisionStay2D(Collision2D collision)
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

        //gameObject.transform.position += new Vector3(0f, jumpForce, 0f);

        animator.Play("Player_Jump");
    }

    void Flip()
    {
        gameObject.transform.Rotate(0f,180f, 0f);
        isFlipped = !isFlipped;
    }

}