using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class spawnItem{
    public GameObject item;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;

}
public class Loot : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer sr;
    [HideInInspector] public int counter = 0;
    public Sprite openedLootBox;
    public spawnItem[] spawnItems;
    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < spawnItems.Length; i++){
            if(i == 0){
                spawnItems[i].minSpawnProb = 0;
                spawnItems[i].maxSpawnProb = spawnItems[i].spawnRate - 1;       //60-1=59
            }
            else{
                spawnItems[i].minSpawnProb = spawnItems[i - 1].maxSpawnProb + 1;        //59+1=60
                spawnItems[i].maxSpawnProb = spawnItems[i].minSpawnProb + spawnItems[i].spawnRate -1;       //60+40-1=99
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Spawn(){
        float randomNum = Random.Range(0, 100);     //it will only choose a number between 0 and 99 
        for(int i = 0; i < spawnItems.Length; i++){
            if(randomNum >= spawnItems[i].minSpawnProb && randomNum <= spawnItems[i].maxSpawnProb){     //check if the number is in range of the min and max of the current item in the array
                Debug.Log(randomNum + " " + spawnItems[i].item.name);
                Instantiate(spawnItems[i].item, transform.position, Quaternion.identity);
                break;
            }
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Bullet" && counter == 0){
            sr.sprite = openedLootBox;
            Object.Destroy(other);
            //Object.Destroy(this);

            Spawn();
            counter++;
        }
    }
}
