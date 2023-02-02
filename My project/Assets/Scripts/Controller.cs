using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private GameObject bowChild;

    public Transform firePoint;
    public GameObject projectilePrefab;

    private float horizontal;
    private bool touch;
    private float sqrSpeedCap;
    private bool bowEnabled = true;

    public float jumpForce = 20f;
    public float moveForce = 20f;
    public float speedCap = 30f;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>(); 
        bowChild = gameObject.transform.GetChild(0).gameObject;

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

            if (Input.GetKeyDown(KeyCode.Q))
        {
            bowEnabled = !bowEnabled;
            bowChild.SetActive(bowEnabled);
            gameObject.GetComponent<HingeJoint2D>().connectedBody = bowChild.GetComponent<Rigidbody2D>();
        }
       
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * (horizontal * moveForce));

        if (!bowEnabled)
        {
            if (horizontal < 0 && sprite.flipX == false)
            {
               sprite.flipX = !sprite.flipX;
            }
            else if (horizontal > 0 && sprite.flipX == true)
            {
                sprite.flipX = !sprite.flipX;
            }
        }
        //gameObject.transform.position += new Vector3(horizontal * speed,0f, 0f);

        if (rb.velocity.sqrMagnitude > sqrSpeedCap)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, speedCap);

        //Debug.Log(rb.velocity.sqrMagnitude);
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
        animator.Play("Player_Jump");
    }
}