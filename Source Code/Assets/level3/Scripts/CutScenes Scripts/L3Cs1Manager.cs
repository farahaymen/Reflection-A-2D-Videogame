using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class L3Cs1Manager : MonoBehaviour
{
    [Header("Animation-Related")]
    public Animator screenAnim;
    [Header("Dialogue-Related")]
    public Dialogue[] dialogues;
    private DialogueManager dm;
    private int prevDialoguesDone = 1; //even though it is initially 0, 1 is placed for easy tracing
    private int convNum = 0;//number of conversation in the cutscene (0-based): increments when a conversation ends
    
    //Scene-Navigation Related:
    public bool loadNextBuildIndex = true; //if the programmer wants to set the build index of the next scene that will load after this one (current build index+1), he/she will adjust this bool to true in the inspector
    private SceneNavigationManager snInst; //can't initialize it here, even if the variable to be assigned is static

    //Button Related:
    private bool submitButton => (Input.GetButtonDown("Submit"));
    public SpriteChange paper;
    public Sprite s;
    public AudioClip phoneRing;
    void Awake() 
    {
        dm = FindObjectOfType<DialogueManager>(); //FindObjectOfType<> could be used here, as there is only 1 DialogueManager script per scene
        snInst = SceneNavigationManager.instance;
        if (screenAnim == null) //safety conditions in case the programmer using unity forgot to drag-drop the components in the inspector
            screenAnim = GameObject.Find("CanvasScreenEffects").GetComponent<Animator>();
        screenAnim.SetBool("startBlack", false);
        dm.dialogueDone = false; //#Tricky part regarding Invoke() and why this bool has to be manually set before each invoke
        Invoke("StartConvo", 1.0f); //Dialogue 1
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
            paper.GetComponent<SpriteChange>().ChangeSprite(s);
            AudioManager.instance.setSfx(phoneRing);
            Invoke("StartConvo", 1.0f); //Dialogue 2
        }
        if (dm.dialogueDone && prevDialoguesDone == 2)
        {
            screenAnim.SetBool("dialogue2Done", true);
            dm.dialogueDone = false;
            prevDialoguesDone++;
        }
        if (prevDialoguesDone == 3)
        {
            Invoke("NextScene", 5.0f); //goes to second cutscene after 5 seconds
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
