using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerEnemy : EnemyController
{
    public float speedWhenChasing = 1.2f;
    public float seeingDistance = 1f; //should always be positive
    public bool circleVision = false;
    public float timeToAgroAfterHit = 1.5f;
    public Transform castPoint; //enemy's eye that the 2DlineCast is casted from
    private float invisibilityTime = 0f;
    private bool playerInvisible = false;
    private Transform playerLoc; //player location so that the enemy keeps following him/her
    private Animator anim;
    private Rigidbody2D rb;
    private bool moonWalking => (transform.position.x > playerLoc.position.x && isFacingRight || transform.position.x < playerLoc.position.x && !isFacingRight); //if the enemy is following the player while looking in the other direction (i.e. moonwalking), then flip the enemy sprite
    
    void Awake() 
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //keeps changing whenever the player changes their location
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); 
        if (castPoint == null) //safety condition in case the programmer didn't drag and drop it into unity's inspector
            castPoint = transform.Find("castPoint"); //the hierarchy information is stored in the Transform component rather than the GameObject itself. You can find a child using the Find method on the Transform
        if (seeingDistance < 0f)
            seeingDistance = - seeingDistance;
    }
    void Update() 
    {
        CheckPlayerInvisibility(); //sets playerInvisible to false if player has been hit for a long time, increments invisibility time otherwise
        if (CanSeePlayer(seeingDistance) && !playerInvisible) //second condition ensures the enemy can see the player again after some time passes 
        {
            anim.SetBool("seesPlayer", true);
            ChasePlayer();
        }
        else
        {
            anim.SetBool("seesPlayer", false);
            StopChasingPlayer();
        }    
    }

    void OnTriggerEnter2D(Collider2D other) //overrides EnemyController's on trigger function, as extra logic was added here
    {
        if (other.tag == "PlatformEdge")
            Flip();
        else if (other.tag == "DisableVision")
        {
            Flip();
            playerInvisible = true;
        }
        else if (other.tag == "Enemy")
        {
            Flip(); //bug: Unlike in the WalkingEnemy (bear) case, in here, we don't need to flip the other enemy, I don't know why :(
        }
        else if (other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            playerInvisible = true;
            Flip();
        }    
    }
    
    bool CanSeePlayer(float agroDistance)
    {
        bool seesPlayer = false;
        Vector2 endPos = (Vector2)castPoint.position + Vector2.right * agroDistance * (isFacingRight ? 1 : -1); //the end of the 2DLinecast; if the enemy is facing left, it is multiplied by -1 to make the end in the left direction
        if (circleVision)
        {
            Collider2D colHitPlayer = Physics2D.OverlapCircle(castPoint.position, agroDistance, 1<<LayerMask.NameToLayer("Player"));
            if (colHitPlayer != null) //Bug note: apparently adding this "&& colHitPlayer.gameObject.tag == "Player"" makes the enemy not see the player sometimes
            {
                seesPlayer = true;
                Debug.DrawLine(castPoint.position, endPos, Color.red);
            }
            else
            {
                seesPlayer = false;
                Debug.DrawLine(castPoint.position, endPos, Color.blue);
            }

        }
        else
        {
            
            RaycastHit2D rayHitPlayer = Physics2D.Linecast(castPoint.position, endPos, 1<<LayerMask.NameToLayer("Player"));
            if (rayHitPlayer.collider != null)
            {
                seesPlayer = true;
                Debug.DrawLine(castPoint.position, rayHitPlayer.point, Color.red);
            }
            else
            {
                seesPlayer = false;
                Debug.DrawLine(castPoint.position, endPos, Color.blue);
            }
        }
        
        return seesPlayer;
    }

    void ChasePlayer()
    {
        if (Mathf.Abs(transform.position.x - playerLoc.position.x) < 0.1f) //if the enemy isn't moving (as the player is above it), make it's animation idle
            anim.SetBool("walking", false);
        else
        {
            anim.SetBool("walking", true);
            transform.position = Vector2.MoveTowards(transform.position, 
                                                    new Vector2(playerLoc.position.x, transform.position.y), //#MoveTowards() x-axis only
                                                    speedWhenChasing * Time.deltaTime); //multiplied by Time.deltaTime so that the speed is constant across different PCs
        if (moonWalking)
            Flip();
        }
    }

    void StopChasingPlayer()
    {
        if (isFacingRight)
            rb.velocity = new Vector2(speed, rb.velocity.y);  
        else
            rb.velocity = new Vector2(-speed, rb.velocity.y);
    }

    void CheckPlayerInvisibility()
    {
        if (playerInvisible)
            invisibilityTime += Time.deltaTime;
        if (invisibilityTime >= timeToAgroAfterHit)
        {
            playerInvisible = false;
            invisibilityTime = 0f;
        }
    }
}

/*
#MoveTowards() x-axis only:     MoveTowards() incremently moves vector a to b,
                                but when the player is above the enemy, the enemy jumps a little to try to get him (which we don't want).
                                so to avoid that, let b.y be the same as a.y, that's what's being done in the second argument:
                                new Vector2(playerLoc.position.x, transform.position.y
*/