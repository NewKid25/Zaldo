using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used to handle basic ai logic including patroling, moving, and becoming alert
// ai aiming is handled in AI_Aiming

public class AI_Brain : MonoBehaviour
{
    private Vector3 vecUpAngle;
    private Vector3 vecDownAngle;
    private Vector3 origin;
    private Vector3 vecFront;
    private SpriteRenderer sprite;
    private GameObject bowChild;
    private Transform playerTrans;
    private Rigidbody2D rb;

    public float angle = 30f;
    public float lookDistance = 30f; //how far the enemy can see the player
    public float tooClose = 15;
    public float tooFar = 40;
    public float moveSpeed = 5f;
    public bool alert = false;
    public bool isFlipped = false;

    private void Start()
    {
        //should take angle and make a vector2 up and right with that angle from a line along the positive x axis,
        //vecDownAngle is vecUpAngle but flipped so that is points down and to the right
        vecUpAngle.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        vecUpAngle.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        vecDownAngle.x = vecUpAngle.x;
        vecDownAngle.y = -vecUpAngle.y;
        vecFront = Vector2.right;

        sprite = gameObject.GetComponent<SpriteRenderer>();
        bowChild = gameObject.transform.GetChild(0).gameObject;
        rb = gameObject.GetComponent<Rigidbody2D>();



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!alert)
            Look();
        else
            Fight();


    }


    private void Look()
    {
        //Look() creates three raycasts along the vectors made in Start(). These raycasts can return infromation on things they hit

        //determines orentaion of the enemy. If isFlipped is true, it's facing right
        //origin gives the starting location of the raycasts.
        //(can't cast them from transfrom.position, because it starts in the object and hits it on the way out)
        if (isFlipped)
        {
            origin = transform.position + new Vector3(1f, 0f, 0f);
        }
        else
        {
            origin = transform.position + new Vector3(1f, 0f, 0f) * (-1f);
        }

        //casts raycasts
        RaycastHit2D frontHit = Physics2D.Raycast(origin, vecFront, lookDistance);
        RaycastHit2D upHit = Physics2D.Raycast(origin, vecUpAngle, lookDistance);
        RaycastHit2D downHit = Physics2D.Raycast(origin, vecDownAngle, lookDistance);

        //draws lines so rays can be seen when debugging
        Debug.DrawLine(origin, origin + vecFront * lookDistance, Color.red, 1f);
        Debug.DrawLine(origin, origin + vecUpAngle * lookDistance, Color.red, 1f);
        Debug.DrawLine(origin, origin + vecDownAngle * lookDistance, Color.red, 1f);

        //makes array so easier to handle info from the raycasts
        ArrayList rays = new ArrayList();
        rays.Add(frontHit);
        rays.Add(upHit);
        rays.Add(downHit);

        //goes through whole array
        foreach (RaycastHit2D hit in rays)
        {
            if (hit.collider != null)

                //if enemy is not alert and they see the player they take out their bow
                if (alert == false && hit.collider.gameObject.CompareTag("Player"))
                {
                    bowChild.SetActive(true);
                    gameObject.GetComponent<HingeJoint2D>().connectedBody = bowChild.GetComponent<Rigidbody2D>();
                    //gameObject.GetComponentInChildren<AI_Aiming>().enabled = true;
                    //gameObject.GetComponentInChildren<AI_Bow>().enabled = true;


                    playerTrans = hit.collider.gameObject.transform;
                    gameObject.GetComponent<Patrolling>().enabled = false;

                    alert = true;
                }
        }

  //Debuging stuff. Allows you to see info on what (if anything) is being hit. Uncomment for to make work good :P

        /*foreach (RaycastHit2D hit in rays)
        {
            if (hit.point != null)
            {
                //  Debug.Log(frontHit.transform.ToString());
                Debug.Log(hit.transform);
                Debug.Log(hit.distance);

            }
            else
            {
                Debug.Log("hit=null");
            }
        }*/
    }

    //flips direction of the vectors
    public void FlipLook ()
    {

        vecUpAngle = new Vector3(vecUpAngle.x * (-1f), vecUpAngle.y, vecUpAngle.z);
        vecDownAngle = new Vector3(vecDownAngle.x * (-1f), vecDownAngle.y, vecDownAngle.z);
        vecFront = new Vector3(vecFront.x * (-1f), vecFront.y, vecFront.z);
    }


    private void Fight()
    {
        Vector2 playerPos = playerTrans.position;
        Vector2 thisPos = transform.position;

        float sqrPlayerDistance = (playerPos - thisPos).sqrMagnitude;
        //Debug.Log("PlayerDistance^2=" + sqrPlayerDistance);
        if (sqrPlayerDistance < tooClose*tooClose)
          {
              if (playerPos.x < thisPos.x)
              {
                  rb.MovePosition(thisPos + moveSpeed * Time.fixedDeltaTime * Vector2.right);
              }
              else
                  rb.MovePosition(thisPos + -1f * moveSpeed * Time.fixedDeltaTime * Vector2.right);
          }
        else if (sqrPlayerDistance > (tooFar * tooFar))
        {
            if (playerPos.x < thisPos.x)
            {
                rb.MovePosition(thisPos + -1f * moveSpeed * Time.fixedDeltaTime * Vector2.right);
            }
            else
                rb.MovePosition(thisPos + moveSpeed * Time.fixedDeltaTime * Vector2.right);
        }
    }
}
