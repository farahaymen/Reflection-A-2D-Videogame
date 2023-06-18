using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class L1S1Manager : MonoBehaviour
{
    [Header("Animation-Related")]
    public Animator screenAnim;
    [Header("Dialogue-Related")]
    public Dialogue[] dialogues;
    private DialogueManager dm;
    private GameObject fdt;
    //private int prevDialoguesDone = 1; //even though it is initially 0, 1 is placed for easy tracing
    private int convNum = 0;//number of conversation in the cutscene (0-based): increments when a conversation ends
    
    //Scene-Navigation Related:
    public bool loadNextBuildIndex = true; //if the programmer wants to set the build index of the next scene that will load after this one (current build index+1), he/she will adjust this bool to true in the inspector
    private SceneNavigationManager snInst; //can't initialize it here, even if the variable to be assigned is static

    //Dialogue Related:
    private bool finalDialogueStarted = false; // to switch to the next scene after the final dialogue ends


    //Button Related:
    private bool submitButton => (Input.GetButtonDown("Submit"));

    void Awake() 
    {
        dm = FindObjectOfType<DialogueManager>(); //FindObjectOfType<> could be used here, as there is only 1 DialogueManager script per scene
        fdt = GameObject.Find("finalDialogueTrigger");
        snInst = SceneNavigationManager.instance;
        if (screenAnim == null) //safety conditions in case the programmer using unity forgot to drag-drop the components in the inspector
            screenAnim = GameObject.Find("CanvasScreenEffects").GetComponent<Animator>();

        screenAnim.SetBool("startBlack", false);
        finalDialogueStarted = false;
    }
    void FixedUpdate() 
    {
        if (fdt != null && fdt.GetComponent<FinalDialogueTrigger>().finalDialogueStarted)
        {
            finalDialogueStarted = true;
            dm.dialogueDone = false; //#Tricky part regarding Invoke() and why this bool has to be manually set before each invoke
            StartConvo(); //Dialogue 1
            Destroy(fdt); //destroying fdt gameObject in order not to keep executing this if condition
        }
    }
    
    void Update() 
    {
        if (finalDialogueStarted && submitButton)
            dm.DisplayNextSentence(this.GetInstanceID());
        if (finalDialogueStarted && dm.dialogueDone)
        {
            screenAnim.SetBool("dialogue2Done", true); //dialogue2Done leads to screen transitioning to black
            AudioManager.instance.stopBG();
            Invoke("NextScene", 3.0f); //goes to level 1's second scene after 5 seconds
        }
    }
    
    //VV Modifies a global (to the script) variable
    private void StartConvo() 
    {
        if (convNum >= dialogues.Length) //condition isn't really necessary, done just in case index is greater than length for some reason
            convNum = 0;
        dm.StartDialogue(dialogues[convNum], this.GetInstanceID());
        convNum++;
    }
    private void NextScene()
    {
        if (loadNextBuildIndex)
            snInst.GoToScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            //snInst.[NEXT SCENE HERE]()
        }   
    }
}

/*
#Tricky part regarding Invoke():    even though this is set to false in StartDialogue (which is called inside StartConvo), 
                                    we have to set it manually before each Invoke() too, as control flow doesn't stop at Invoke(), 
                                    so if we didn't set it manually, the update function will check for the bool before 
                                    it has been set by StartDialogue()


*/