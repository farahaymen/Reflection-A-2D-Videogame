using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isFacingRight;

    public float jumpHeight = 5f;
    public KeyCode jumpButton;

    public LayerMask whatIsGround;
    public Rigidbody rb;
    private Vector2 movement;
    public Transform groundCheck;
    private bool grounded;
    public KeyCode shootButton;
    public Transform firepoint;
    public GameObject bullet;
    public Animator anim;
    private float horizontalDirection;
    private bool isShooting;
    public AudioClip jumpSound;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isFacingRight = true;
        isShooting = false;

    }
    // Update is called once per frame
    void Update()
    {
        horizontalDirection = 0;
       movement.x = Input.GetAxisRaw("Horizontal");
       movement.y = Input.GetAxisRaw("Vertical");
       //movement.Normalize();
       rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, movement.y * moveSpeed);
       RaycastHit hit;
       if(Physics.Raycast(groundCheck.position, Vector3.down, out hit, .3f, whatIsGround)){
           grounded = true;
       }
       else{
           grounded = false;
       }
       if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
           if(isFacingRight){
               Flip();
               isFacingRight = false;
               horizontalDirection = -1;
           }
            horizontalDirection = -1;

       }
       if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
           if(!isFacingRight){
               Flip();
           isFacingRight = true;
           }
            horizontalDirection = 1;

       }
       if(Input.GetKeyDown(jumpButton) && grounded){
           Jump();
        }

        if(Input.GetKeyDown(shootButton) && isShooting == false){
            anim.SetBool("isShooting", true);
            isShooting = true;
            shoot();
        }
        else{
            anim.SetBool("isShooting", false);
            isShooting = false;
        }

        changeAnimation();
    }
    public void Jump(){
        rb.velocity += new Vector3(0f, jumpHeight, 0f);
        AudioManager.instance.setSfx(jumpSound);
    }
    public void Flip(){
        transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public void shoot(){
        Instantiate(bullet ,firepoint.position, firepoint.rotation);
        AudioManager.instance.setSfx(shootSound);
    }    

    private void changeAnimation() //Amro's part
    {
        if(horizontalDirection == 0) //no input
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isIdle", true);

        }
        else
        {
            anim.SetBool("isMoving", true);
            anim.SetBool("isIdle", false);
        }
        if(horizontalDirection < 0 && isFacingRight)
        {
            Flip();
        }
        else if(horizontalDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        if(grounded)
        {
            anim.SetBool("isJumpingUp", false);
            anim.SetBool("isJumpingDown", false);
        }

        if(!grounded)
        {
            anim.SetBool("isJumpingUp", true);
            anim.SetBool("isIdle", false);
        }

        if(rb.velocity.y < -0.1)
        {
            anim.SetBool("isJumpingUp", false);
            anim.SetBool("isJumpingDown", true);
        }
        
        
        
        /*if(grounded && isShooting){
            anim.SetBool("isJumpingUp", false);
            anim.SetBool("isJumpingDown", false);
            anim.SetBool("isShooting", true);

         }
         if(!grounded && isShooting){
            anim.SetBool("isJumpingUp", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isShooting", true);

         }
         if(!grounded && !isShooting){
            anim.SetBool("isJumpingUp", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isShooting", false);
         }*/
    }
  
}
