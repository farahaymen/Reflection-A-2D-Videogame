using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Helpful resources: Tactical programmer's video:
https://www.youtube.com/watch?v=TTKPmPvekUY&t=56s
Blackthornprod's video:
https://www.youtube.com/watch?v=j111eKN8sJw
*/

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    [Header("Layer Masks")]
    public LayerMask groundLayer;
    [Header("Ground Collision Variables")]
    public Transform groundCheck;
    public float groundCheckRadius;
    private bool onGround;

    [Header("Movement Variables")]
    public float acceleration = 20f;
    public float maxMoveSpeed = 4f; 
    public float groundLinearDrag = 7f; //explained in ApplyGroundLinearDrag function
    private float horizontalDirection; //move right --> 1, move left --> -1, no movement key pressed --> 0
    private bool directionChanged => (rb.velocity.x > 0f && horizontalDirection < 0f
                                      || rb.velocity.x < 0f && horizontalDirection > 0f); //bool to decrease slipperiness upon changing directions
    [Header("Jump Variables")]
    public float jumpForce = 5f; //increase this --> increase the max height of your jump (while holding down jump button)
    public float airLinearDrag = 3f; //increase this --> decrease feeling of floating while jumping
    public float fallMultiplier = 2.5f; //increase this --> fall down quicker (if you hold down jump button)
    public float lowJumpFallMultiplier = 2f; //increase this --> decrease jump height when pressing jump button one time quickly.

    [Header("Animation Variables")]
    public Animator anim;
    public bool isFacingRight = true; 

    [Header("Sound Variables")]
    public AudioClip jumpSound;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        CheckCollisions();
        if (Input.GetButtonDown("Jump") && onGround){
            Jump();
        } 
        
        if (onGround){
            ApplyGroundLinearDrag();
        }
        
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            
        }
        changeAnimation(); //Amro's part
    }
    void FixedUpdate() 
    {
        horizontalDirection = GetInput().x;
        MoveCharacter();
    }
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //#GetAxisRaw()
    }
    private void CheckCollisions()
    {
        Vector2 endPos = (Vector2)groundCheck.position + Vector2.down * groundCheckRadius;
        RaycastHit2D hit = Physics2D.Linecast(groundCheck.position, endPos , groundLayer);
        if (hit.collider != null)
        {
            onGround = true;
            Debug.DrawLine(groundCheck.position, endPos, Color.red);   
        }
        else 
        {
            onGround = false;
            Debug.DrawLine(groundCheck.position, endPos, Color.blue);
        }
        //onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); //#Physics2D.OverlapCircle()
    }
    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(horizontalDirection, 0f) * acceleration); //adding acc. force until max speed is reached
        if (Mathf.Abs(rb.velocity.x) > maxMoveSpeed) //condition to stop at max speed
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x)*maxMoveSpeed, rb.velocity.y); //#Sign
        
    }
    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(horizontalDirection) < 0.4f || directionChanged) //adds friction/decrease slipperiness upon movement start/finish or changing directions 
            rb.drag = groundLinearDrag;
        else 
            rb.drag = 0; //don't add friction when player is in the middle of the movement
    }
    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);  
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //Shorthand for writing Vector2(0, 1) * jumpForce
        AudioManager.instance.setSfx(jumpSound);                                                      //#ForceMode2D.Impulse
    }
    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void Flip() //Amro's part
    {
        isFacingRight = !isFacingRight;
        //transform.Rotate(0f, 180f, 0f);
        transform.localScale = new Vector2(-(transform.localScale.x), transform.localScale.y);
    }

    private void changeAnimation() //Amro's part
    {
        if(horizontalDirection == 0 || rb.velocity.x == 0) //no input
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
        if(onGround)
        {
            anim.SetBool("isJumpingUp", false);
            anim.SetBool("isJumpingDown", false);
            anim.SetBool("isIdle", true);
        }

        if(!onGround)
        {
            anim.SetBool("isJumpingUp", true);
            anim.SetBool("isIdle", false);
        }

        if(rb.velocity.y < -0.3) //keep decreasing this value if the player will walk down on a steeper negative sloped platform
        {
            anim.SetBool("isJumpingUp", false);
            anim.SetBool("isJumpingDown", true);
        }
    }

}


/*
#LayerMasks struct:         Specifies Layers to use in a Physics.Raycast.
                            A GameObject can use up to 32 LayerMasks supported by the Editor. 
                            The first 8 of these Layers are specified by Unity; the following 24 are controllable by the user.
                            Bitmasks represent the 32 Layers and define them as true or false. 
                            Each bitmask describes whether the Layer is used. As an example, 
                            bit 5 can be set to 1 (true). This will allow the use of the built-in Water setting.
                            Edit->Settings->Tags and Layers option shows the use of the 32 bitmasks. 

#Input.GetAxisRaw():        Returns the value of the virtual axis identified by axisName with no smoothing filtering applied.
                            The value will be in the range -1...1 for keyboard and joystick input. Since input is not smoothed, 
                            keyboard input will always be either -1, 0 or 1. This is useful if you want to do all smoothing of keyboard input processing yourself.

#Mathf.Sign():              return value is 1 when value is positive or zero, -1 otherwise.      

#GetButtonDown():           you cannot use KeyCodes, rather you are required to set your own buttons in the input manager. This is the recommended by
                            Unity and can be quite powerful as it allows developers to map custom
                            joystick / D-pad buttons.
                            By default Unity has already created some custom button inputs that can be accessed out of the box.
                            To access the input manager go to Edit > Settings > Input.

#ForceMode2D.Impulse:       Apply the impulse force instantly. This mode depends on the mass of rigidbody so more force must be applied 
                            to move higher-mass objects the same amount as lower-mass objects.
                            This mode is useful for applying forces that happen instantly, such as forces from explosions or collisions.

VV not used
#Physics2D.Raycast():       Casts a ray against Colliders in the Scene.
                            Parameters (only mentioned used arguments in code):
                            origin: The point in 2D space where the ray originates.
                            direction: A vector representing the direction of the ray.
                            distance: The maximum distance over which to cast the ray.
                            layerMask: Filter to detect Colliders only on certain layers.

#Physics2D.OverlapCircle(): note that even though we're using a box collider, circle here
                            will work because we care about the groundCheck gameObject, 
                            which doesn't have a collider, so we're basically creating a circle
                            collider around the groundCheck gameObject.
*/