using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
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
            player.GetComponent<PlayerStats>().armorEquipped = true;
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            player.GetComponent<PlayerStats>().armorEquipped = true;
            Destroy(this.gameObject);
        }
    }
}
