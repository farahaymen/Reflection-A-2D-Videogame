using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusic : MonoBehaviour
{
    public AudioClip newTrack;
    private AudioManager am;
    // Start is called before the first frame update
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }
    
    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(newTrack != null){
             am.setBG(newTrack);
            }
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(newTrack != null){
             am.setBG(newTrack);
            }
        }
    }
}
