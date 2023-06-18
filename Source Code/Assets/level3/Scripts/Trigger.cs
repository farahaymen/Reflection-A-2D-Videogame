using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public ArrowTrap[] at;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            for(int i = 0; i < at.Length; i++){
                at[i].GetComponent<ArrowTrap>().active = true;
            }
        }
    }
}
