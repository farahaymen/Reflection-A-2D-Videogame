using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public bool enableObject = false; //if we want to enable an object instead of instantiating it
    public GameObject objectToSpawnOrEnable;
    public Transform spawnPoint; //won't be used if we're just enabling an object
    private GameObject spawnedObject;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (enableObject)
            {
                objectToSpawnOrEnable.SetActive(true);
                Destroy(this.gameObject);
                return;
            }
            spawnedObject = Instantiate(objectToSpawnOrEnable, spawnPoint.position, spawnPoint.rotation);
            if (spawnedObject.tag == "Enemy" && spawnedObject.GetComponent<EnemyController>().isFacingRight)
                spawnedObject.GetComponent<EnemyController>().Flip(); //starting with the object (e.g. enemy) facing the left direction
            Destroy(this.gameObject);
        }
    }
/*
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (enableObject)
            {
                objectToSpawnOrEnable.SetActive(true);
                Destroy(this.gameObject);
                return;
            }
            spawnedObject = Instantiate(objectToSpawnOrEnable, spawnPoint.position, spawnPoint.rotation);
            if (spawnedObject.tag == "Enemy" && spawnedObject.GetComponent<EnemyController>().isFacingRight)
                spawnedObject.GetComponent<EnemyController>().Flip(); //starting with the object (e.g. enemy) facing the left direction
            Destroy(this.gameObject);
        }
    }
    */
}
