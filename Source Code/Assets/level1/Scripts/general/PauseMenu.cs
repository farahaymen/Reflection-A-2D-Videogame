using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [Header("Pause Menu Elements")]
    public GameObject pauseMenu;
    public static bool paused;
    //pause buttons
    public Button[] pauseMenuButtons;
    //0: Resume, 1: Make a Call, 2: Return to Main Menu
    private int countB; //count number of elements in buttons array
    public int currS = 0; //Currently Selected element in buttons array
    
    [Header("Dialouge Related")]
    private DialogueManager dm;
    public List<Dialogue> eventDialogues; 
    public int currEvDial; //index of the next event dialogue
    public List<Dialogue> randomDialogues; //other conversations that aren't necessarily contributing to the story, or guidelines to the player
    
    
    //Buttons related
    private bool nextItemButton => (Input.GetButtonDown("NextItemHorizontal") || Input.GetButtonDown("NextItemVertical")); //right/d or down/s keys are pressed
    private bool previousItemButton => (Input.GetButtonDown("PreviousItemHorizontal") || Input.GetButtonDown("PreviousItemVertical")); //left/a or up/w keys are pressed
    private bool pauseResumeButton => (Input.GetButtonDown("Cancel"));
    private bool submitButton => (Input.GetButtonDown("Submit"));

    //Scene navigation related
    private static SceneNavigationManager snInst = SceneNavigationManager.instance;

    private void Awake() 
    {
        dm = FindObjectOfType<DialogueManager>(); //FindObjectOfType<> could be used here, as there is only 1 DialogueManager script per scene 
        if (eventDialogues == null)
            eventDialogues = new List<Dialogue>();
        if (randomDialogues == null)
            randomDialogues = new List<Dialogue>();
        paused = false;
        countB = pauseMenuButtons.Length;
        pauseMenu.SetActive(false);
    }
    private void Update() 
    {
        if (pauseResumeButton && !dm.dialogueBox.activeInHierarchy) //pause/resume only if there's no dialogue box currently displayed
            if (!paused)
                Pause();
            else
                Resume();
        if (paused)
            if (nextItemButton)
            {
                pauseMenuButtons[MoveCursor(1)].Select();
            }
            else if (previousItemButton)
            {
                pauseMenuButtons[MoveCursor(-1)].Select();
            }
            else if (submitButton)
                if (dm.dialogueDone) 
                    PressButton();
                else
                {    
                    dm.DisplayNextSentence(this.GetInstanceID());
                    if (!dm.dialogueBox.activeInHierarchy) //selects the call button again once dialogue box closes
                        pauseMenuButtons[currS].Select();
                }

    }

    //VV updates global variable "currS"
    private int MoveCursor(int incOrDec) //increments/decrements current highlighted button, possible argument values: 1, -1
    {
        currS += incOrDec;

        if (currS >= countB)
            currS = 0;
        else if (currS < 0)
            currS = countB-1;

        return currS;
    }

    private void PressButton()
    {
        if (currS <= 0 || currS >= countB)
            Resume();
        else if (currS == 1)
            MakeACall();
        else if (currS == 2)
            Home();
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        currS = 0;
        pauseMenuButtons[currS].Select();
        paused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }
    public void Home()
    {
        Time.timeScale = 1f;
        snInst.GoToHomeMenu();
    }

    //VV Modifies global (to the script) variable
    private void MakeACall() //prioritizes calls related to gameplay events, then random conversations
    {
        if (currEvDial < eventDialogues.Count)
        {
            dm.StartDialogue(eventDialogues[currEvDial++], this.GetInstanceID()); //starts current dialogue, then increments so that when call button is pressed again, a new dialogue starts
        }
        else
        {
            dm.StartDialogue(randomDialogues[Random.Range(0, randomDialogues.Count)], this.GetInstanceID()); //starts a dialogue at random (with equal probability)
        }
    }
}
