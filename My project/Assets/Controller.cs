using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody2D rb;

    private float x;
    private float y;
    public float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
         if (x!= 0)
        {
            rb.AddForce(new Vector2(speed * x,0f));
            // gameObject.transform.position += new Vector3(speed * moveh, 0f, 0f);

        }

        y = Input.GetAxisRaw("Vertical");
        if (y != 0)
        {
            rb.AddForce(new Vector2 (0f, speed * y));

           // Debug.Log("vertical");
            //gameObject.transform.position += new Vector3(0f, speed * movev, 0f);
        }

    }
}
