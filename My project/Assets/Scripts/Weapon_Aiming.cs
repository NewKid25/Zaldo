using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Aiming : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    public SpriteRenderer charSprite;

    Vector2 mousePos;
    private Vector2 lookDir;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        //charSprite = gameObject.GetComponentInParent<SpriteRenderer>();   just gets renderer in gameObject, should figure out why
    }

    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        if (lookDir.x < 0 && charSprite.flipX == false)
        {
            charSprite.flipX = !charSprite.flipX;
           // Debug.Log("Flip Left");
        }
        else if (lookDir.x > 0 && charSprite.flipX == true)
        {
            charSprite.flipX = !charSprite.flipX;
            //Debug.Log("Flip Left");
        }
    }
}
