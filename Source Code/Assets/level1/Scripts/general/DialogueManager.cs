using System; //for Tuple<>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
Helpful resources: Tactical Brackeys' video:
https://www.youtube.com/watch?v=_nRzoTzeyxU
*/

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Image npcImage;
    public Sprite noFaceSprite;
    public Button continueButton;
    public GameObject dialogueBox;
    //public Animator dialogueBoxAnimator;

    //Dialogue-related variables
    [HideInInspector]
    public bool dialogueDone = true; //public because it decides certain after transitions done in different scripts (ex: Cs0Manager.cs) 
    [HideInInspector]
    public int dispNxtScenManager = -1; //-1 --> "Free" // public because it decides who manages displaying next scenetences upon pressing the submit button (ex: Cs0Manager.cs doesn't manage it, while PauseMenu does)
    private Queue<nameToSentenceTuple> sentencesQ;
    private bool submitButton => (Input.GetButtonDown("Submit"));

    //Temp Variables
    private nameToSentenceTuple sentenceInfo; //temp variable that stores the dequeuing of sentencesQ 
    private string currentName; //temp variable that stores the name of the character currently talking
    private Sprite currentFace; //temp variable that stores the face of the character currently talking
    
    void Awake()
    {
        if (nameText == null) //safety conditions in case the programmer using unity forgot to drag-drop the components in the inspector
            nameText = GameObject.Find("nameText").GetComponent<Text>();
        if (dialogueText == null)
            dialogueText = GameObject.Find("dialogueText").GetComponent<Text>();
        if (npcImage == null)
            npcImage = GameObject.Find("faceImage").GetComponent<Image>();
        if (noFaceSprite == null)
            noFaceSprite = Resources.Load<Sprite>("noFace"); //#Resources 
        if (nameText == null)
            noFaceSprite = GameObject.Find("faceImage").GetComponent<Sprite>();
        if (continueButton == null)
            continueButton = GameObject.Find("continueButton").GetComponent<Button>();
        if (dialogueBox == null)
            dialogueBox = GameObject.Find("dialogueBox");
        
        dialogueDone = true;
        dispNxtScenManager = -1; //note that even though this string is set above, it is done here also because sometimes the unity inspector sets an empty value by default, so it overrides the value set above
        dialogueBox.SetActive(false); //we don't want the dialogue box to appear unless there's text to be displayed
        sentencesQ = new Queue<nameToSentenceTuple>();
    }

    public void StartDialogue(Dialogue dialogue, int scriptResponsible)
    {
        if (dispNxtScenManager != -1) //doesn't start a new dialogue if the previous one isn't finished yet
            return;
        sentencesQ.Clear(); //clearing any left messages from a previous conversation
        nameText.text = ""; 
        dialogueText.text = "";
        npcImage.sprite = noFaceSprite; //#Image.sprite //a black image is used at the start, as no image should show initially when the dialogue box is empty      
        dialogueDone = false;
        dispNxtScenManager = scriptResponsible;

        dialogueBox.SetActive(true);
        continueButton.Select(); //makes the button focused on, such that when the submit button is pressed, this button is pressed, and the dialogue is continued
        
        foreach (nameToSentenceTuple sentence in dialogue.sentences) //note that we don't write Dialogue.nameToSentenceTuple (NTST), as NTST is defined outside Dialogue class
        {
            sentencesQ.Enqueue(sentence);
        }
        DisplayNextSentence(scriptResponsible); //as if submit button was pressed once; to make dialogue box start with initial sentence of the conversation
    }

    public void DisplayNextSentence(int scriptResponsible) //script responsible == instance ID of object that started the dialogue
    {
        if (dispNxtScenManager != scriptResponsible || !dialogueBox.activeInHierarchy) //condition to make sure the function is only excuted by a script that started the dialogue and that dialogue box is visible
            return;
        if (sentencesQ.Count == 0)
        {
            dialogueDone = true;
            dispNxtScenManager = -1; //States that no script is using the manager, so now any script can use it to start a new dialogue
            dialogueBox.SetActive(false);
            return;
        }
        continueButton.Select(); //make sure continue button is highlighted whenever a dialogue continues
        sentenceInfo = sentencesQ.Dequeue();

        nameText.text = EnumToString(sentenceInfo.name);

        currentFace = GameCharacters.instance.nameToFace[sentenceInfo.name];
        if (npcImage.sprite != currentFace) //Checking if the same character was talking in the previous sentence or not (to avoid the millisecond of replacing an image with the same one). This is done by comparing sprites
            npcImage.sprite = currentFace; //replacing UI image box with the image of the character currently talking

        dialogueText.text = ""; //making sure the text box is initially empty when starting a new sentence
        StopAllCoroutines(); //incase the player presses the submit button while a previous sentence was still being typed
        StartCoroutine(TypeDialogue());
        //if we wanted the sentence to appear at once (not letter by letter):
        //dialogueText.text = sentenceInfo.sentence;
    }
    
    private IEnumerator TypeDialogue() //replaces text box with current text letter by letter
    {
        foreach (char letter in sentenceInfo.sentence)
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(sentenceInfo.typeDelay); //#WaitForSecondsRealtime
        }
    }

    private string EnumToString(Characters ch)
    {
        switch (ch)
        {
            case Characters.NoFace: return "";
            case Characters.YoungSteve: return "Steve (Young)";
            case Characters.XHiker: return "Hiker";
            case Characters.XFormal: return "";
            case Characters.XRobber: return "Robber";
            case Characters.SergioNoFace: return "X";
            case Characters.ThePoliceOfficer: return "The Officer";
            case Characters.RandomPoliceOfficer: return "Officer";
            case Characters.OldSteve: return "Steve (Old)";
            case Characters.ConnorPoliceOfficer: return "Connor";
            default: return ch.ToString(); //returns same name for Steve, Gwen, Merideth, Connor, Father, and Sergio
        }
    }
}


/*
#Image.sprite:          The sprite that is used to render this image. 
                        This returns the source Sprite of an Image. 
                        This Sprite can also be viewed and changed in the Inspector 
                        as part of an Image component. 
                        This can also be used to change the Sprite using a script.

#Resources:             Loads the asset of the requested type stored at path in a Resources folder.
                        More than one Resources folder can be used. If you have multiple Resources folders 
                        you cannot duplicate the use of an asset name.

#WaitForSecondsRealtime:Just like WaitForSeconds(), but it isn't affected by Time.timeScale (so coroutine in pause menu, etc)
*/