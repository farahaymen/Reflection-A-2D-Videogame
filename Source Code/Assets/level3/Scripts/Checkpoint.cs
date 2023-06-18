using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            FindObjectOfType<LevelManager>().currCheckpoint = this.gameObject;
        }
    }
    void OnTriggerEnterCollider2D(Collider other){
        if(other.tag == "Player"){
            Debug.Log("df");
            FindObjectOfType<LevelManager>().currCheckpoint = this.gameObject;
        }
    }
}
