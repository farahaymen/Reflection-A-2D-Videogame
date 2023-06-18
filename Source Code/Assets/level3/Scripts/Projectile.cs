using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;  
    public Transform player;
    public Vector2 target;
    ArrowTrap arrowTrap;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        arrowTrap = FindObjectOfType<ArrowTrap>();
        //if(arrowTrap.GetComponent<ArrowTrap>().isFacingLeft){
            //speed = -speed;
            //transform.localScale = new Vector3((transform.localScale.x), transform.localScale.y, transform.localScale.z);
           //}
           //else if(!arrowTrap.GetComponent<ArrowTrap>().isFacingLeft){
               speed = -speed;
               transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);

           //}
           //else if(arrowTrap.GetComponent<ArrowTrap>().isFacingDown){
               //transform.localScale = new Vector3(transform.localScale.x, -(transform.localScale.y), (transform.localScale.z));
           //}
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        GetComponent<Rigidbody>().velocity = new Vector2(speed, GetComponent<Rigidbody>().velocity.y);

 
}
void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            player.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if(other.tag == "Wall"){
            Destroy(this.gameObject);
        }
    }
}
