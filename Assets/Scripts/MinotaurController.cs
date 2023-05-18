using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurController : MonoBehaviour
{
    Animator myAnim;
    Rigidbody2D myRB;
    public bool isClear;
    public float walkSpeed;
    bool facingRight;
    bool mustFlip;
    public Transform checkGroundPosition;
    public LayerMask groundLayer;
    public Collider2D enemyCollider;
    public Transform player;
    public float agroRange;
    public bool agroed;
    public bool hasFliped;

    

    public int maxHealth = 100;
    int currentHealth;
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();   
        currentHealth = maxHealth;
        isClear = true;
        agroed = false;

    }

    void Update()
    {    
        if(currentHealth >= 0)
        {   
            if(isClear)
            {
                Patrol();
            }   
            
            float distToPlayer = Vector2.Distance(transform.position, player.position);
            if(distToPlayer < agroRange)
            {
                agroed = true;
                PlayerChase();
            } 
        }
       
    }

    void FixedUpdate()
    {
        if(isClear)
        {
            mustFlip =! Physics2D.OverlapCircle(checkGroundPosition.position, 0.1f, groundLayer);
        }

    }

    public void DamageTaken(int damage)
    {
        myAnim.SetTrigger("Hit");
        currentHealth -= damage;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        myAnim.SetBool("isDead",true);
        Debug.Log("Enemy died!");
       
        GetComponent<Collider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        this.enabled = false;
    }

    void Patrol()
    {        
        if (mustFlip || enemyCollider.IsTouchingLayers(groundLayer))
        {
            flip();
        }
        myRB.velocity = new Vector2 (walkSpeed * Time.fixedDeltaTime, myRB.velocity.y);
        myAnim.SetBool("isWalking",true);
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
        hasFliped = false;
    }

    void PlayerChase() 
    {
        if(transform.position.x < player.position.x)
        {
            //moves to the right (x)
            myRB.velocity = new Vector2(100 * Time.fixedDeltaTime ,myRB.velocity.y);
            if(facingRight && !hasFliped)
            {
                flip();
                hasFliped = true;
            }
            print("I am moving right!");
        } 
        else if(transform.position.x > player.position.x)
        {
            //moves to the left (-x)
            myRB.velocity = new Vector2(-100  * Time.fixedDeltaTime, myRB.velocity.y);
            if(!hasFliped && !facingRight)
            {
                flip();
                hasFliped = true;
            }
            print("I am moving left!");           
        }
    
    agroed = false;
    print("I am not agroed anymore!");

    }
}
