using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public int Minx, Miny, Maxx, Maxy;
    public CameraController c;
    // Start is called before the first frame update
    void Start()
    {
        c = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player")
        c.GetComponent<CameraController>().miny = Miny;
        c.GetComponent<CameraController>().minx = Minx;
        c.GetComponent<CameraController>().maxx = Maxx;
        c.GetComponent<CameraController>().maxy = Maxy;

    }
}
