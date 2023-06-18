using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public HealthBar healthbar;
    public float health;
    public bool isDead;
        //get{
            //return health == 0;
        //}
    //}
    public Transform player;
    public bool isFlipped;
    private int damage = 2;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim =GetComponent<Animator>();
        healthbar = FindObjectOfType<HealthBar>();
        health = healthbar.maxHealth;
        isDead = false;
        player = FindObjectOfType<PlayerStats>().transform;

    }

    // Update is called once per frame
    void Update()
    {
        //if(health < 0){
            //health = 0;
        //}
        healthbar.currHealth= health;
        //if(isDead){
        //anim.SetTrigger("Death");
        //}
    }
    public void Flip(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        if(transform.position.x > player.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if(transform.position.x < player.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f, 180f ,0f);
            isFlipped = true;
        }
    }
    public void Attack(){
        player.GetComponent<PlayerStats>().TakeDamage(damage);
    }
    public void TakeDamage(int damage){
        health -=damage;
        anim.SetTrigger("Hurt");
        if(health <= 0){
            isDead = true;
        }
        Die();

    }
    public void Die(){
        if(isDead){
            anim.SetTrigger("Death");
            isDead = false;
            Destroy(this.gameObject);
            //Invoke("removeObject", 1f);
        }
        void removeObject()
        {
        }
        //GetComponent<Collider>().enabled = false;
    }
}
