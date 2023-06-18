using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompButton : MonoBehaviour
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
        if(other.tag == "Player" ||other.tag == "Bullet" ){
            for(int i = 0; i < at.Length; i++){
                at[i].GetComponent<ArrowTrap>().active = false;
            }
        }
    }
}
