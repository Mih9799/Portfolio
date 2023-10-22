using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHudManager : MonoBehaviour {

    //Variables
    Animator HudAnimator;
    public Text PlayerObjectiveText;
    public Text PlayerMessageText;
    public Text PlayerPromptText;
    public Image crosshair;
    public float messageTime = 2.0f;
    public bool showCrosshair = true;

    //Singleton Setup
    public static PlayerHudManager instance = null; //Static instance of PlayerHudManager which allows it to be accessed by any other script.
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        if (!showCrosshair)
        {
            crosshair.gameObject.SetActive(false);
        }
    }
        // Use this for initialization
    void Start ()
    {
        HudAnimator = gameObject.GetComponent<Animator>();
        //DisplayPlayerMessage("This is a Message to the player" );
        //DisplayPlayerObjective("This is an Objective");
        //DisplayPlayerPrompt("This is a Contextual Prompt");
    }

    public void DisplayPlayerObjective(string inMessage)
    {
        PlayerObjectiveText.text = inMessage;
        HudAnimator.Play("Anim_UI_ObjectiveAppear", 0);
        StartCoroutine(HidePlayerObjective());
    }

    public void DisplayPlayerMessage(string inMessage)
    {
        PlayerMessageText.text = inMessage;
        HudAnimator.Play("Anim_UI_MessageAppear", 0);
        StartCoroutine(HidePlayerMessage());
    }

    public void DisplayPlayerPrompt(string inMessage, bool holdPrompt)
    {
        PlayerPromptText.text = inMessage;
        HudAnimator.Play("Anim_UI_PromptAppear", 0);
        if (!holdPrompt)
        {
            StartCoroutine(HidePlayerPrompt());
        }        
    }

    public void DisplayObjectiveComplete()
    {        
        DisplayPlayerObjective("Objective Complete");
    }

    //Coroutines for hiding UI elements when duration complete
    public IEnumerator HidePlayerObjective()
    {
        yield return new WaitForSeconds(messageTime);
        HudAnimator.Play("Anim_UI_ObjectiveDisappear", 0);
    }

    public IEnumerator HidePlayerMessage()
    {
        yield return new WaitForSeconds(messageTime);
        HudAnimator.Play("Anim_UI_MessageDisAppear", 0);
    }

    public IEnumerator HidePlayerPrompt()
    {
        yield return new WaitForSeconds(messageTime);
        HudAnimator.Play("Anim_UI_PromptDisAppear", 0);
    }
}
