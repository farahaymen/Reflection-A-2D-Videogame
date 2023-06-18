using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float camSpeed, cameraOffset, minx, miny, maxx, maxy;
    public bool camFacingRight = true; //note that cameraOffset should be set (using the inspector) to a -ve value if this bool is set to false in the inspector
    public bool switchCamera => (Input.GetButtonDown("CameraDirection")); //public for debugging purposes
    public float verticalDiffToSlowSpeed = 100f; //set this to 0.3f if you want the camera to follow the player vertically quicker when he/she reaches a platform of a different y value, and leave it at a high number (e.g. 100) if you want the camera to Lerp() to the player at constant speed
    private float initialCamSpeed;

    void Awake() 
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;    
        initialCamSpeed = camSpeed;
    }
    void Update() 
    {
        if (switchCamera)
        {
            SwitchCamera();
        }    
    }
    void FixedUpdate() 
    {
        if (Mathf.Abs(player.position.y - transform.position.y) > verticalDiffToSlowSpeed) 
            camSpeed = initialCamSpeed / 4;
        else
            camSpeed = initialCamSpeed;
        Vector2 newCamPosition = Vector2.Lerp(transform.position, 
                                            (Vector2)player.position + Vector2.right * cameraOffset , 
                                            Time.deltaTime * camSpeed);
        float clampx = Mathf.Clamp(newCamPosition.x, minx, maxx);
        float clampy = Mathf.Clamp(newCamPosition.y, miny, maxy);
        transform.position = new Vector3(clampx, clampy, -5f);
    }
    public void SwitchCamera()
    {
        cameraOffset = - cameraOffset;
        camFacingRight = !camFacingRight;
    }

    //Failed attempt:
    //I wanted to make the camera follow the player on the y-axis after a small delay (will change when he/she walks on a new platform with different y-axis for a short amount of time)
    //The below function would've been called as a coroutine after the "newCampPosition =" line in the update function
    /* 
    private IEnumerator ChangeVertically()
    {
        yield return new WaitForSeconds(followVerticallyDelay);
        newCamPosition = Vector2.Lerp(transform.position, 
                                    new Vector2(newCamPosition.x, player.position.y), //changing y only
                                    Time.deltaTime * camSpeed);
    }
    */
}
