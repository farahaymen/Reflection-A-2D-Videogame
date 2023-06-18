using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public Boss boss;
    int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        boss = FindObjectOfType<Boss>();
        PlayerController player;
        player = FindObjectOfType<PlayerController>();
        if(player.transform.localScale.x < 0){
            speed = -speed;
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(speed, GetComponent<Rigidbody>().velocity.y);
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Enemy"){
            //Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if(other.tag == "Boss"){
            boss.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
