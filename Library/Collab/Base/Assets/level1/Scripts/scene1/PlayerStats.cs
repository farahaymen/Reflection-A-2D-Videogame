using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 5;
    private int health = 5;
    public int damage = 1;
    private float flickerTime = 0f; //"stopwatch" that increases until it reaches flicker duration
    public float flickerDuration = 0.1f; //duration in which spriteRenderer component is enabled/disabled (character is visible/non-visible)
    private SpriteRenderer spriteRenderer;
    public bool isImmune = false;
    private float immunityTime = 0f; //"stopwatch" that increases until it reaches immunity duration
    public float immunityDuration = 1.5f; //duration in which player is immune, so if immunityDuration = 1f and flickerDuration = 0.1f, the player will flicker 10 times until becomming immune again.

    
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (isImmune == true)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                spriteRenderer.enabled = true; //note: ".enabled" is the tickbox next to the sprite renderer component of a gameObject, and setting it to true/false makes the object visible/disappear
            }
        }
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime += Time.deltaTime;
        }
        else 
        {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            flickerTime = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        if (isImmune == false)
        {
            health -= damage;
            if (health < 0)
                health = 0;
            if (health == 0)
            {
                FindObjectOfType<CheckpointManager>().RespawnPlayer();
                health = maxHealth;
            }
            Debug.Log("Player Health: " + health.ToString());
        }
        activateImmunity();
    }
    void activateImmunity()
    {
        isImmune = true;
        immunityTime = 0f;
    }
}
