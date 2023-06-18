using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public float currHealth;
    [HideInInspector]public float maxHealth = 100f;
    Boss boss;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        boss = FindObjectOfType<Boss>();
    }

    // Update is called once per frame
    void Update()
    {
        setHealth(currHealth);
    }
    public void setHealth(float health){
        currHealth = health;
        healthBar.fillAmount = currHealth / maxHealth;  //to give percentage needed to fill the bar
    }
}
