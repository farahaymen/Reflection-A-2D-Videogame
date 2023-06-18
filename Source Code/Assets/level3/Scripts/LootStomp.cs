using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootStomp : Loot
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playerStomped()
    {
        sr.sprite = openedLootBox;
        Spawn();
        counter++;
    
    }
}
