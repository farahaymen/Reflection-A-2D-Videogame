using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//class responsible for any screen effects in a scene (cutscene mid-gameplay, etc)
public class L1Cs2Manager : MonoBehaviour
{
    [Header("Animation-Related")]
    public Animator screenAnim;
    public Animator bookAnim;
    public Animator boxAnim;
    [Header("Dialogue-Related")]
    public Dialogue[] dialogues;
    private DialogueManager dm;
    private int prevDialoguesDone = 1; //even though it is initially 0, 1 is placed for easy tracing
    private int convNum = 0;//number of conversation in the cutscene (0-based): increments when a conversation ends
    
    //Scene-Navigation Related:
    public bool loadNextBuildIndex = true; //if the programmer wants to set the build index of the next scene that will load after this one (current build index+1), he/she will adjust this bool to true in the inspector
    private SceneNavigationManager snInst; //can't initialize it here, even if the variable to be assigned is static

    //Audio-related variables
    public AudioClip alarmSfx;

    //Button Related:
    private bool submitButton => (Input.GetButtonDown("Submit"));

    void Awake() 
    {
        dm = FindObjectOfType<DialogueManager>(); //FindObjectOfType<> could be used here, as there is only 1 DialogueManager script per scene
        snInst = SceneNavigationManager.instance;
        if (screenAnim == null) //safety conditions in case the programmer using unity forgot to drag-drop the components in the inspector
            screenAnim = GameObject.Find("CanvasScreenEffects").GetComponent<Animator>();
        if (bookAnim == null)
            bookAnim = GameObject.Find("book").GetComponent<Animator>();
        if (boxAnim == null)
            boxAnim = GameObject.Find("box").GetComponent<Animator>();

        AudioManager.instance.setSfx(alarmSfx);
        screenAnim.SetBool("startBlack", true);
        dm.dialogueDone = false; //#Tricky part regarding Invoke() and why this bool has to be manually set before each invoke
        Invoke("StartConvo", 3.0f); //dialogue 1
    }
    void Update() 
    {
        if (submitButton)
            dm.DisplayNextSentence(this.GetInstanceID());
        if (dm.dialogueDone && prevDialoguesDone == 1)
        {
            screenAnim.SetBool("dialogue1Done", true);
            dm.dialogueDone = false;
            prevDialoguesDone++;
            Invoke("StartConvo", 3.0f); //dialogue 2
        }
        if (dm.dialogueDone && prevDialoguesDone == 2)
        {
            dm.dialogueDone = false;
            prevDialoguesDone++;
            Invoke("OpenBox", 1.5f);
            Invoke("StartConvo", 3.0f); //dialogue 3
        }
        if (dm.dialogueDone && prevDialoguesDone == 3)
        {
            dm.dialogueDone = false;
            prevDialoguesDone++;
            Invoke("OpenBook", 1.5f);
            Invoke("StartConvo", 3.0f); //dialogue 4
        } 
        if (dm.dialogueDone && prevDialoguesDone == 4)
        {
            screenAnim.SetBool("dialogue2Done", true);
            Invoke("NextScene", 5.0f); //goes to first level after 5 seconds
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
    private void OpenBox()
    {
        boxAnim.SetBool("openBox", true);
    }
    private void OpenBook()
    {
        bookAnim.SetBool("openBook", true);
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