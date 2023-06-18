using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;  
    public Transform player;
    public Vector2 target;
    ShootingEenemy enemy;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<ShootingEenemy>();
        if(enemy.GetComponent<ShootingEenemy>().isFacingLeft){
                speed = -speed;
           }
           else if(!enemy.GetComponent<ShootingEenemy>().isFacingLeft){
               transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);

           }
        //if(enemy.transform.localScale.x < 0){
            //speed = -speed;
            //transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        //}
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;        
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //if(transform.position.x == target.x && transform.position.y == target.y){ 
        GetComponent<Rigidbody>().velocity = new Vector2(speed, GetComponent<Rigidbody>().velocity.y); 
}
void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            player.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
