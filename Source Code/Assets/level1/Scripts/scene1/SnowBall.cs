using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite explodedSnowball; //has to be set up manually in the inspector
    public float delayDestroy = 0.5f; //if a dialogue trigger is also attached to the snowball, then this delay should be after the dialogue is invoked
    
    void Awake() 
    {
        sr = GetComponent<SpriteRenderer>();    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            sr.sprite = explodedSnowball;
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            gameObject.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroySnowBall());
        }
    }
    private IEnumerator DestroySnowBall()
    {
        yield return new WaitForSecondsRealtime(delayDestroy);
        if (gameObject.GetComponent<DialogueTrigger>() == null) //if there's a DialogueTrigger attached, then it will be the one responsible for the destruction of the gameObject
            Destroy(gameObject);
    }
}
