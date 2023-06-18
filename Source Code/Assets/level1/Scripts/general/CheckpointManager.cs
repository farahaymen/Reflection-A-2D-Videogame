using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour //equivalent to "levelManager" in the labs
{
    public GameObject firstCheckpoint; //first checkpoint in the scene
    public GameObject CurrentCheckpoint; //current checkpoint (that the player has passed, and is initially the same as firstCheckpoint)

    public void RespawnPlayer()
    {

        FindObjectOfType<PlayerMovement>().transform.position = CurrentCheckpoint.transform.position;
    }
    public void RespawnPlayerToSceneStart()
    {

        FindObjectOfType<PlayerMovement>().transform.position = firstCheckpoint.transform.position;
    }
}
