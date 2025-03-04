using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class MovementScript : MonoBehaviour
{
    //public HealthManager healthManager;
    public Rigidbody2D myRigidbody;
    public BoxCollider2D enemyCheck;
    public float speed;
    float playerInput;
    public bool isGrounded = true;
    public bool jump = false;
    public float jump_speed;
    public bool facingRight = true;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;
    public LayerMask enemyMask;
    public Animator animator;
    public UnityEvent OnLandEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Awake()
    {
        if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //capture player input
        playerInput = Input.GetAxis("Horizontal");

        animator.SetFloat("speed", Mathf.Abs(playerInput));

        //check if player pressed jump key
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindAnyObjectByType<GameManagerScript>().EndGame();
        }
    }

    void FixedUpdate()
    {
        movePlayer(playerInput);
        flipPlayer();
        grounded();

        StartCoroutine(waitHurt()); //wait few seconds for getting hurt again

        if (jump && isGrounded)
        {
            jumpPlayer();
            isGrounded = false;
            jump = false;
        }
    }

    //check if player is attacked by enemy
    public void enemyAttacked()
    {
        if (Physics2D.OverlapAreaAll(enemyCheck.bounds.min, enemyCheck.bounds.max, enemyMask).Length > 0)
        {
            HealthManager.health--;

            //reset health to max if it went below 0
            if (HealthManager.health < 0)
            {
                HealthManager.health = 3;
            }
        }
    }

    //wait few seconds before getting hurt again
    IEnumerator waitHurt()
        {
            yield return new WaitForSeconds(10);
            enemyAttacked();
        }

    //check if player is grounded
    private void grounded()
    {
        if (Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0)
        {
            isGrounded = true;
            OnLandEvent.Invoke();
        }
    }

    public void movePlayer(float input)
    {
        myRigidbody.velocity = new Vector2(input*speed, myRigidbody.velocity.y);
    }

    public void jumpPlayer()
    {
        animator.SetBool("jump", true);
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jump_speed);
    }

    public void onLanding()
    {
        animator.SetBool("jump", false);
    }

    public void flipPlayer()
    {
        if (playerInput < 0 && facingRight || playerInput > 0 && !facingRight)
        {
            facingRight = !facingRight;
            Vector2 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }
}
