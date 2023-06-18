using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : EnemyController
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate() 
    {
        if (isFacingRight)
            rb.velocity = new Vector2(speed, rb.velocity.y);  
        else
            rb.velocity = new Vector2(-speed, rb.velocity.y);
    }
    
}
