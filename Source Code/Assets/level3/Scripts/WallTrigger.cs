using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    public Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        //boss = FindObjectOfType<Boss>();

    }

    // Update is called once per frame
    void Update()
    {
        boss = FindObjectOfType<Boss>();

    }
    public void OnTriggerEnter(Collider other){
        if(boss.health <=0){
            Destroy(this.gameObject);
        }
    }
}
