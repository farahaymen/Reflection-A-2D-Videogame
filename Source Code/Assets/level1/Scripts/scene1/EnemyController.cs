using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioClip hitSound;
    public bool isFacingRight = false;
    public float speed = 2f;
    public int damage = 1;
    public int maxHealth = 1;
    public int health = 1;


    public void Flip() //should actually be protected, as only the inherited classes could access it, but will leave it as public just in case some other script wants to access it
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(-(transform.localScale.x), transform.localScale.y);
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "PlatformEdge")
            Flip();
        else if (other.tag == "Enemy")
        {
            if (other.GetComponent<EnemyController>().isFacingRight != isFacingRight) //condition to check if they are facing each other, and if they are, flip the other enemy as well (as it won't enter it's collider now, because this enemy has flipped)
                other.GetComponent<EnemyController>().Flip();
            Flip();
        }
        else if (other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
            Flip();
        }    
    }

    public void TakeDamage(int damage)
    {
        AudioManager.instance.setSfx(hitSound);
        health -= damage;
        if (health <= 0)
            Destroy(gameObject); //destroys the gameobject attached to the inherited class's script
    }
    
}
