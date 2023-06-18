using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    private Transform tr;
    private Rigidbody2D rb;
    private float x;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        x = 0.2f;
    }

    void FixedUpdate(){
        tr.position = new Vector3(tr.position.x + x, tr.position.y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("YES\n");
        x = -x;
    }
}
