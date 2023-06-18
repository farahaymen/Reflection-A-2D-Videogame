using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSprite(Sprite newSprite){
        sr.sprite = newSprite;
    }
}
