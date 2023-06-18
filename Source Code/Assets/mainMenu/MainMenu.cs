using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Elements")]
    public GameObject mainMenu;
    //main menu buttons
    public Button[] mainMenuButtons;
    //0: Start a new game, 1: Continue Game, 2: Level 1, 3: Level 2, 4: Level 3, 5: Quit Game
    private int countB; //count number of elements in buttons array
    public int currS = 0; //Currently Selected element in buttons array (Public for debugging purposes)
    
    //Scene-Navigation Related:
    private SceneNavigationManager snInst; //can't initialize it here, even if the variable to be assigned is static


    private bool nextItemButton => (Input.GetButtonDown("NextItemHorizontal") || Input.GetButtonDown("NextItemVertical")); //right/d or down/s keys are pressed
    private bool previousItemButton => (Input.GetButtonDown("PreviousItemHorizontal") || Input.GetButtonDown("PreviousItemVertical")); //left/a or up/w keys are pressed
    private bool submitButton => (Input.GetButtonDown("Submit"));

    private void Start() 
    {
        snInst = SceneNavigationManager.instance;
        countB = mainMenuButtons.Length;
        mainMenu.SetActive(true);
        
    }
    private void Update() 
    {
        if (nextItemButton)
        {
            mainMenuButtons[MoveCursor(1)].Select();
        }
        else if (previousItemButton)
        {
            mainMenuButtons[MoveCursor(-1)].Select();
        }
        else if (submitButton)
            PressButton();

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
        switch (currS)
        {
        case 0: StartNewGame(); break;
        case 1: Level1(); break;
        case 2: Level2(); break;
        case 3: Level3(); break;
        case 4: QuitGame(); break;
        default: break;
        }
    }
    public void StartNewGame()
    {
        snInst.GoToScene(snInst.cs1BefL1S1);
    }
    //Didn't know how to write last checkpoint that player passed by unto hard disk to be retrieved later :[
    /*
    private void ContinueGame()
    {

    }
    */
    public void Level1()
    {
        snInst.GoToScene(snInst.level1Scene1); 
    }
    public void Level2()
    {
        snInst.GoToScene(snInst.level2Scene1);  
    }
    public void Level3()
    {
        snInst.GoToScene(snInst.level3Scene1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
