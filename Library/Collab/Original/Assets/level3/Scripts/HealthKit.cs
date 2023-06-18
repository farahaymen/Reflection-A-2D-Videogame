using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public float frequency;
    public float amplitude;
    private Vector3 currPosition;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = currPosition + transform.up * Mathf.Sin(Time.time * frequency) * amplitude;
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(!(player.GetComponent<PlayerStats>().health == player.GetComponent<PlayerStats>().maxHealth)){
                player.GetComponent<PlayerStats>().health +=1;
            Destroy(this.gameObject);
            }
        }
    }
}
