using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDialogueTrigger : MonoBehaviour
{
    [HideInInspector]
    public bool finalDialogueStarted = false; //public to be seen by the level manager
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            finalDialogueStarted = true;
        }
    }
     private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {
            finalDialogueStarted = true;
        }
    }
}
