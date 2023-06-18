using System; //for Tuple<>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiOpts
{
    InstantDisplay,
    AddToPauseMenuEvents,
    AddToPauseMenuRandom,
}
public class DialogueTrigger : MonoBehaviour
{
    public DiOpts dialogueOperation;
    public float triggerDelay = 0; //applied only if the dialogueOperation is instantDisplay
    public bool playRingSfx = true;
    public AudioClip ringSfx;
    public Dialogue dialogue;
    private DialogueManager dm;
    private PauseMenu pm;
    private bool submitButton => (Input.GetButtonDown("Submit"));
    private bool instantDialogueTriggered = false;

    void Awake() 
    {
         dm = FindObjectOfType<DialogueManager>(); //FindObjectOfType<> could be used here, as there is only 1 DialogueManager script per scene 
         pm = FindObjectOfType<PauseMenu>(); //FindObjectOfType<> could be used here, as there is only 1 PauseMenu script per scene
    }
    void Update() 
    {
        if (instantDialogueTriggered && submitButton)
        {
            dm.DisplayNextSentence(this.GetInstanceID());
        }    
        if (instantDialogueTriggered && dm.dialogueDone) //means that dialogue is triggered and finished
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !dm.dialogueBox.activeInHierarchy) //the other condition is done in case there was another dialogue running that wasn't finished
        {
            TriggerDialogue();
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player" && !dm.dialogueBox.activeInHierarchy) //the other condition is done in case there was another dialogue running that wasn't finished
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        switch (dialogueOperation)
        {
        case DiOpts.InstantDisplay: 
            dm.dialogueDone = false; //signaling that a dialogue will start after the trigger delay
            instantDialogueTriggered = true; //signaling that instant dialogue has started so that we can press submit button to continue it
            StartCoroutine(StartConvo());
            break;
        case DiOpts.AddToPauseMenuEvents: 
            pm.eventDialogues.Add(dialogue); 
            break; //adds a dialogue to the end of the list
        case DiOpts.AddToPauseMenuRandom: 
            pm.randomDialogues.Add(dialogue); 
            break;
        default:
            break;
        }
    }

    private IEnumerator StartConvo()
    {
        yield return new WaitForSecondsRealtime(triggerDelay);
        if (playRingSfx)
            AudioManager.instance.setSfx(ringSfx, 0.05f);
        Time.timeScale = 0;
        dm.StartDialogue(dialogue, this.GetInstanceID());
    }
}
