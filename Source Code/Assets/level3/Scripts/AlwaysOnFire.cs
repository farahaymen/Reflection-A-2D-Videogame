using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnFire : MonoBehaviour
{
    private int damage = 1;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("activated", true);

    }
     public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            }
     }
}
