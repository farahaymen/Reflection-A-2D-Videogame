using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEenemy : MonoBehaviour
{
    public int health;
    public float speed;
    public Transform[] moveSpots;
    public int spot;
    private float waitTime;
    public float startWaitTime;
    public bool isFacingLeft;
    public float stoppingDistance;
    public float retreatDistance; //when the enemy should back away from the target
    public float lookRadius;
    public Transform player;
    public Transform firepoint;
    public GameObject bullet;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private bool playerDetected;
    public AudioClip shootSound;
    public Animator anim;
    public bool isShooting;
    public float horizontalDirection;

    // Start is called before the first frame update
    void Start()
    {
        horizontalDirection = 0;
        anim = GetComponent<Animator>();
        waitTime = startWaitTime;
        isFacingLeft = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        playerDetected = false;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= lookRadius)
        {
            playerDetected = true;
            Follow();
        }
        if (playerDetected)
        {
            Follow();
        }
        else
        {
            Patrol();
        }
        changeAnimation();
    }
    public void Patrol()
    {
        //if x-axis is bigger then the spot is on the right, and vice versa
        if (moveSpots[spot].position.x > transform.position.x)
        {    //if spot on the enemy's right
            if (isFacingLeft)
            {
                Flip();
                isFacingLeft = false;
            }
            horizontalDirection = -1;
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[spot].position, speed * Time.deltaTime);

        }
        else if (moveSpots[spot].position.x < transform.position.x)
        { //if the spot on the enemy's left
            if (!isFacingLeft)
            {
                Flip();
                isFacingLeft = true;
            }
            horizontalDirection = 1;
            transform.position = Vector3.MoveTowards(transform.position, moveSpots[spot].position, speed * Time.deltaTime);

        }
        if (Vector3.Distance(transform.position, moveSpots[spot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {                              //if enemy done waiting 
                spot = Random.Range(0, moveSpots.Length);       //move to a new spot
                waitTime = startWaitTime;                    //reset waiting time
            }
            else
            {                                      //decrease waiting time until it's done
                waitTime -= Time.deltaTime;
                //transform.position = this.transform.position;
            }
        }
    }
    public void Follow()
    {
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {  //if distance between enemy and player is bigger than distance where he should stop
            if (player.position.x > transform.position.x)
            {    //if player on the enemy's right
                if (isFacingLeft)
                {
                    Flip();
                    isFacingLeft = false;
                }
            }
            else if (player.position.x < transform.position.x)
            { //if the player on the enemy's left
                if (!isFacingLeft)
                {
                    Flip();
                    isFacingLeft = true;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);     //move toward the player

        }
        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance)
        { //if the enemy is too close yet doesn't ened to retreat
            transform.position = this.transform.position;  //freeze the enemy's position
        }
        else if (Vector3.Distance(transform.position, player.position) < retreatDistance)
        { //if enemy is too close 
            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);     //move back from the player
        }
        Shoot();
    }
    public void Flip()
    {
        float newZScale = Mathf.Abs(transform.localScale.z - 1f);
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public void Shoot()
    {
        if (timeBtwShots <= 0)
        {
            Instantiate(bullet, firepoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        AudioManager.instance.setSfx(shootSound);

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Bullet"){
             health -= player.GetComponent<PlayerStats>().damage;
            if (health < 0)
                health = 0;
            if (health == 0)
            {
                Object.Destroy(this.gameObject);
            }
            Object.Destroy(other.gameObject);
            Debug.Log("Enemy Health: " + health.ToString());
        }
    }
    private void changeAnimation() //Amro's part
    {
        if(horizontalDirection == 0) //no input
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isIdle", true);
        }
        else
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isIdle", false);
        }
        //if(horizontalDirection < 0 && !isFacingLeft)
        //{
            //Flip();
        //}
        //else if(horizontalDirection > 0 && isFacingLeft)
        //{
            //Flip();
        //}
        if(isShooting){
            anim.SetBool("isIdle", false);
            anim.SetBool("isShooting", true);

         }
         if(!isShooting){
            anim.SetBool("isIdle", true);
            anim.SetBool("isShooting", false);

         }
    }
}
