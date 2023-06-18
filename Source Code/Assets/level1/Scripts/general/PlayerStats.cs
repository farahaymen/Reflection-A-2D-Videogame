using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 5;
    public int health = 5; //it is visible in the inspector for debugging purposes
    public int healthWArmor = 5;

    public bool armorEquipped;
    public GameObject heartSystem;
    private  List<Image> hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite goldenHeart;

    public int damage = 1;

    private float flickerTime = 0f; //"stopwatch" that increases until it reaches flicker duration
    public float flickerDuration = 0.1f; //duration in which spriteRenderer component is enabled/disabled (character is visible/non-visible)
    public bool isImmune = false;
    private float immunityTime = 0f; //"stopwatch" that increases until it reaches immunity duration
    public float immunityDuration = 1.5f; //duration in which player is immune, so if immunityDuration = 1f and flickerDuration = 0.1f, the player will flicker 10 times until becomming immune again.
    
    private SpriteRenderer spriteRenderer;
    private CheckpointManager cm;

    public AudioClip playerHit;

    void start(){
        armorEquipped = false;
    }
    void Awake()
    {
        //armorEquipped = false;
        if (heartSystem == null) //safety condition in case programmer forgot to drag and drop it in unity inspector
            heartSystem = GameObject.Find("playerHearts");
        hearts = new List<Image>(heartSystem.GetComponentsInChildren<Image>()); //GetComponentsInChildren returns an array of UI.Images, which is assigned to a List (hearts variable)

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

   void FixedUpdate() 
    {
        if(armorEquipped){
            for(int i = 0; i < hearts.Count; i++){
                if(i < healthWArmor){
                    hearts[i].sprite = goldenHeart;
                }
                else{
                    hearts[i].sprite = fullHeart;
                }
                if(i < health){ 
                   hearts[i].enabled = true;
               }
               else{
                   hearts[i].enabled = false;
               }
            }
        }
        else{
           if(health > maxHealth){
               health = maxHealth;
           }
           for(int i = 0; i < hearts.Count; i++)
           {
               if(i < health){
                   hearts[i].sprite = fullHeart;
               }
               else{
                   hearts[i].sprite = emptyHeart;
               }
               if(i < maxHealth){ //sets the number of hearts based on maxHealth
                   hearts[i].enabled = true;
               }
               else{
                   hearts[i].enabled = false;
               }
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
            if(armorEquipped)
            {
                healthWArmor -=damage;
                if(healthWArmor <= 0)
                {
                    healthWArmor = 0;
                    armorEquipped = false;
                }
            }
            else
            {
                AudioManager.instance.setSfx(playerHit);
                health -= damage;
                if (health < 0)
                    health = 0;
                if (health == 0)
                {
                    if(FindObjectOfType<PlayerController>() != null){
                        FindObjectOfType<LevelManager>().RespawnPlayer();
                    }
                    if(FindObjectOfType<PlayerMovement>() != null){
                        FindObjectOfType<L12Manager>().RespawnPlayer();

                    }

                    health = maxHealth;
                }
                Debug.Log("Player Health: " + health.ToString());
            }
        }
        activateImmunity();
    }
    void activateImmunity()
    {
        isImmune = true;
        immunityTime = 0f;
    }
}
