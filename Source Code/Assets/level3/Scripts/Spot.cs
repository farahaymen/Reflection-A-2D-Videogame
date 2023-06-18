using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    //private int damage = 1;
    private float activationDelay = 1;
    private float activateTime = 3;
    private Animator anim;
    //private SpriteRenderer sr;
    private bool triggered;
    //private bool active;
    public FireTrap[] ft;
    int index;
    // Start is called before the first frame update
    void Awake()
    {
        //anim = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        index = Random.Range(0, ft.Length);
        if(!triggered){
                StartCoroutine(ActivateFireTrap());
            }
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            //if(!triggered){
                //StartCoroutine(ActivateFireTrap());
            //}
            //if(ft.GetComponent<FireTrap>().active){
            //other.GetComponent<PlayerStats>().TakeDamage(damage);
            //}
        

        }
    }
    private IEnumerator ActivateFireTrap(){
        triggered = true;
        yield return new WaitForSeconds(activationDelay);
        ft[index].GetComponent<FireTrap>().active = true;
        ft[index].GetComponent<FireTrap>().anim.SetBool("activated", true);
        yield return new WaitForSeconds(activateTime);
        ft[index].GetComponent<FireTrap>().active = false;
        triggered = false;
        ft[index].GetComponent<FireTrap>().anim.SetBool("activated", false);

    }
}
