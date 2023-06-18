using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Math behind parallax effect is based on the awesome AdamCYounis: https://www.youtube.com/watch?v=tMXgLBwtsvI
public class parallax : MonoBehaviour
{
    public Camera cam;
    public Transform subject; //subject == player, or GameObject that will always move around with camera; the point of focus
    public bool goesUp = false; //whether the parallex effect is applied vertically or not
    private Vector2 startPosition;
    private float startZ;
    private Vector2 travel => (Vector2)cam.transform.position - startPosition;
    private float distanceFromSubject => gameObject.transform.position.z - subject.position.z;
    private float clippingPlane => (cam.transform.position.z + (distanceFromSubject > 0 ? 
                                                        cam.farClipPlane : 
                                                        cam.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(distanceFromSubject) / clippingPlane;
    private Vector2 initialFactor; //to make gameObject remain at its position at the start of the scene
    private Vector2 factor; //the factor value that will keep changing the gameObject's position at each frame

    // Start is called before the first frame update
    void Start()
    {
        if (cam == null)
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        if (subject == null)
            subject = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = gameObject.transform.position;
        startZ = gameObject.transform.position.z;
        initialFactor = travel * parallaxFactor;
    }

    // Update is called once per frame
    void Update()
    {
        factor = travel * parallaxFactor;
        Vector2 newPos = startPosition + (factor - initialFactor);
        gameObject.transform.position = new Vector3(newPos.x, (goesUp ? newPos.y : startPosition.y) , startZ);
    }
}
