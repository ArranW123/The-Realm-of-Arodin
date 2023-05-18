using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class wormController : MonoBehaviour
{

bool mustFlip;
public Collider2D enemyCollider;
public LayerMask groundLayer;
public float walkSpeed;
Rigidbody2D myRB;
bool facingRight;
public bool isClear;
public Transform checkGroundPosition;






    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        isClear = true;

    }

    // Update is called once per frame
    void Update()
    {
            if(isClear)
            {
                Patrol();
            }   
    }

        void FixedUpdate()
    {
        if(isClear)
        {
            mustFlip =! Physics2D.OverlapCircle(checkGroundPosition.position, 0.1f, groundLayer);
        }

    }

    void Patrol()
    {        
        if (mustFlip || enemyCollider.IsTouchingLayers(groundLayer))
        {
            flip();
        }
        myRB.velocity = new Vector2 (walkSpeed * Time.fixedDeltaTime, myRB.velocity.y);
    }

        void flip()
    {
        isClear = false;
        facingRight=!facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        walkSpeed *= -1;
        isClear = true;
       
    }
    
}
