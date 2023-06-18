using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGMusic : MonoBehaviour
{
    public bool stopMusic;
    public AudioClip newMusic;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag != "Player")
            return;
        if (stopMusic)
            AudioManager.instance.stopBG();
        else
            AudioManager.instance.setBG(newMusic);
        Destroy(this.gameObject);
    }
}
