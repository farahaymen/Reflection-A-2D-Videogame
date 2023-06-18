using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float bounce = 9f;
    public Animator anim;
    void Awake() 
    {
        if (anim == null)
            anim = GetComponent<Animator>();    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            anim.SetBool("bounced", true);
        }    
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            anim.SetBool("bounced", false);
        }   
    }
}
