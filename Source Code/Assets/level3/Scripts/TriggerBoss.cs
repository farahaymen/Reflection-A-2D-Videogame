using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBoss : MonoBehaviour
{
    public GameObject loc;
    public GameObject boss;
    public GameObject healthBar;
    public bool isTriggered;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player" && isTriggered == false){
            isTriggered = true;
            Instantiate(boss, loc.transform.position, loc.transform.rotation);
            healthBar.GetComponent<Canvas>().enabled = true;           
        }
    }
        public void OnTriggerExit(Collider other){
            if(other.tag == "Player"){
                isTriggered = false;
                Destroy(this.gameObject);
            }
            
        }
}
