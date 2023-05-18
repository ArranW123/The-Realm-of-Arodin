using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
   // ########## Declaration ##########
   public float maxSpeed;
   
   //Jumping
   bool grounded = false;
   float groundCheckRadius = 0.2f;
   public Transform groundCheck;
   public float jumpHeight;
   public LayerMask groundLayer;
   
   // Animation & Rigidbody
   Rigidbody2D myRB;
   Animator myAnim;
   bool facingRight; 
   
   //Particle Systems
   public ParticleSystem playerDust;
   
   //Combat Declaration
   public Transform attackBox;
   public float attackDistance = 0.5f;
   public LayerMask enemyLayer;
   public LayerMask spikes;
   public Collider2D playerCollider;
   public int attackDamage = 40;
   public float attackTimer = 2f;
   private float nextAttackTimer = 0f;

// ########## END ##########

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();   
        playerCollider = GetComponent<Collider2D>();
        facingRight = true;

    }

    void Update()
    {
        // Checks if player is grounded and hasn't pressed the jump key
        if(grounded && Input.GetKey(KeyCode.W))
        {
            CreateDust();
            grounded = false;
            myAnim.SetBool("isGrounded",false);
            myRB.AddForce(new Vector2(0,jumpHeight));
        }

        if(Time.time >= nextAttackTimer)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                PlayerAttack();
                nextAttackTimer = Time.time + 1f/attackTimer; //Time to prevent user spamming
            }
        }
        
        if(playerCollider.IsTouchingLayers(spikes) || playerCollider.IsTouchingLayers(enemyLayer))
        {
            Die();
        }
        

        


    }
    void FixedUpdate()
    {
    // Check setting grounded and vertical speed (Movement y)
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded",grounded);
        myAnim.SetFloat("verticalSpeed",myRB.velocity.y);

    // Movement x
        float move = Input.GetAxis("Horizontal");
        myRB.velocity = new Vector2(move*maxSpeed,myRB.velocity.y);
        myAnim.SetFloat("speed",Mathf.Abs(move));
    
    // Flips Character sprite, when looking left or right
        if(move>0 && !facingRight)
        {
            flip();
            if(grounded)
            {
                CreateDust();
            }            
        } else if (move<0 && facingRight)
        {
            flip();
            if(grounded)
            {
                CreateDust();
            }   
        }
        

        
        
    }
    void flip()
    {
        facingRight=!facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void PlayerAttack()
    {
        myAnim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackBox.position, attackDistance, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<MinotaurController>().DamageTaken(attackDamage);
            Debug.Log("We Hit "+ enemy.name);
        }
    }

    void Die()
    {
        print("I have died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

// Draws a Gizmo for the attackBox rigidbody
    void OnDrawGizmosSelected() {
        if (attackBox == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackBox.position, attackDistance);
    }

    void CreateDust()
    {
       playerDust.Play(); 
    }


}
