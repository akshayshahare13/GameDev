using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Rigidbody2D myRigidbody;
    public float enemySpeed;
    float enemyInput;
    public bool facingRight = true;
    public BoxCollider2D ground;
    float rightBound;
    float leftBound;
    float enemyPosition;
    int enemyDirection;

    // Start is called before the first frame update
    void Start()
    {
        rightBound = ground.bounds.max.x;
        leftBound = ground.bounds.min.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // enemy patrolling logic
        enemyPosition = myRigidbody.position.x;

        if (facingRight)
        {
            enemyDirection = 1;
        }
        else if (!facingRight)
        {
            enemyDirection = -1;
        }

        enemyInput = enemyDirection * enemySpeed;

        movePlayer(enemyInput);

        flipPlayer();
        
    }

    public void flipPlayer()
    {
        if (facingRight && enemyPosition > rightBound || !facingRight && enemyPosition < leftBound)
        {
            facingRight = !facingRight;
            Vector2 temp = transform.localScale;
            temp.x *= -1;
            transform.localScale = temp;
        }
    }

    public void movePlayer(float input)
    {
        myRigidbody.velocity = new Vector2(input*enemySpeed, myRigidbody.velocity.y);
    }
}
