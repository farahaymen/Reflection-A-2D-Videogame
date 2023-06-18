using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public float frequency;
    public float amplitude;
    private Vector3 currPosition;
    void Start(){
        currPosition = transform.position;

    }
    void Update(){
        transform.position = currPosition + transform.up * Mathf.Sin(Time.time * frequency) * amplitude;

    }
    private void OnCollisionEnter2D (Collision2D col)
	{
        Debug.Log("YESSSS");
			Destroy (gameObject);
	}
    private void OnCollisionEnter (Collision col)
	{
        Debug.Log("YESSSS");
			Destroy (gameObject);
	}

    //private void OnTriggerEnter2D(){
        //Destroy (gameObject);
    //}
}
