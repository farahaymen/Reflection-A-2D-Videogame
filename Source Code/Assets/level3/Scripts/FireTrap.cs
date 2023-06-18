using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FireTrap : MonoBehaviour
{
    private int damage = 1;
    //private float activationDelay = 1;
    private float activateTime;
    public Animator anim;
    //private SpriteRenderer sr;
    //public GameObject player;
    //private bool triggered;
    public bool active;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //if(!triggered){
                //StartCoroutine(ActivateFireTrap());
            //}
            if(active){
            other.GetComponent<PlayerStats>().TakeDamage(damage);
            }

        }
    }
    //private IEnumerator ActivateFireTrap(){
        //triggered = true;
        //yield return new WaitForSeconds(activationDelay);
        //active = true;
        //yield return new WaitForSeconds(activateTime);
        //active = false;
        //triggered = false;
    //}
}
