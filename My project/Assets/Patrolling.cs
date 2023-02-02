using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script handles patroling behavior for the attached enemy
//it must have an ai brian component, as well as two trigger colliders tagged "Patrol_Bound" to work

public class Patrolling : MonoBehaviour
{
    private Rigidbody2D rb;
    private AI_Brain brain;
    private SpriteRenderer sprite;

    private bool waiting = false;
    private float time = 0f;

    public float patrolSpeed = 5f;
    public float waitTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); 
        brain = gameObject.GetComponent<AI_Brain>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //just moves enemy along x axis
        if (!waiting)
            //  transform.position = Vector2.MoveTowards(transform.position, transform.position + Vector3.right, patrolSpeed);
            //rb.AddForce(Vector2.right * patrolSpeed);
            rb.MovePosition(rb.position + patrolSpeed * Time.fixedDeltaTime * Vector2.right);


        //if waiting is true then enemy stops moving and waits for time then turns around and starts moving again
        else
        {
            time += Time.deltaTime;
            //Debug.Log(time);
            if (time >= waitTime)
            {
                time = 0f;
                brain.FlipLook();
                brain.isFlipped = !brain.isFlipped;
                sprite.flipX = !sprite.flipX;
                waiting = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if enemy runs into a collider that is a trigger, it check is object is tagged "Patrol_Bound"
        //if so starts waiting, and flips direction of movement
        if (collision.CompareTag("Patrol_Bound") == true)
        {
            waiting = true;
            patrolSpeed *= -1f;
        }
    }
}

