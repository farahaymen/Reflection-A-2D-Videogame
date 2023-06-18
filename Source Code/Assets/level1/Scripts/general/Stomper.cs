using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Helpful Resources: Expat Studios video:
https://www.youtube.com/watch?v=CIGJ1woYnjc&t=478s
*/

public class Stomper : MonoBehaviour
{
    private Rigidbody2D rb;
    private int playerDamage;
    public float bounceForce;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerDamage = GetComponentInParent<PlayerStats>().damage;
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "EnemyTop")
        {
            other.gameObject.GetComponentInParent<EnemyController>().TakeDamage(playerDamage);
            rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse); //ForceMode2D.Impulse, as we want the character to jump a little instantly after stomping eneymy
        }    
        else if (other.gameObject.tag == "LootBox")
        {
            other.gameObject.GetComponent<LootStomp>().playerStomped();
        }
    }
}
