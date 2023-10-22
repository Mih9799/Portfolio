using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;

public class DemoLevel : MonoBehaviour
{
    private bool Paused = false;
    public PostProcessVolume Shady;
    public PostProcessVolume Presence;
    public CharacterController PlayerMovement;
    public Camera PlayerCamera;
    public GameObject Player;
    public Text Quest;
    public Text BossTitle;
    public GameObject PauseMenu;
    public Texture SergentIcon;
    public Texture GlitchyIcon;
    public Text Announcement;
    public RawImage WhoTalks;
    public GameObject Dialogue;
    public Slider Status;
    private AudioSource _AudioSource = null;
    public AudioClip Safezone;
    public AudioClip Casual1;
    public AudioClip Casual2;
    public AudioClip SuspensefulConspiracy;
    public AudioClip Stalked;
    public AudioClip ItArrives;
    public AudioClip Malfunction;
    public AudioSource Voice;
    public AudioClip Hello;
    public AudioClip Huh;
    public AudioClip Confirm;
    public AudioClip UhOh;
    public AudioClip Wow;
    public GameObject Shards;
    public GameObject GlitchModel;
    private bool CanPause = true;
    public GameObject Sergent;
    public GameObject FirstDoor;
    public GameObject NoRushy;
    public Rigidbody ProofOfFirstStep;
    public Rigidbody ProofOfSecondStep;
    public Rigidbody ProofOfThirdStep;
    public Rigidbody ProofOfFourthStep;
    public Rigidbody ProofOfFifthStep;
    public Rigidbody ProofOfSixthStep;
    public Rigidbody ProofOfSeventhStep;
    public Rigidbody ProofOfEighthStep;
    public Rigidbody ProofOfNinthStep;
    public Rigidbody ProofOfTenthStep;
    public Rigidbody ProofOfEleventhStep;
    public GameObject YouAreSafe;
    public GameObject NoTurningBack;
    public GameObject Backwards;
    public Rigidbody UnEarthed;
    public GameObject Snooper;
    private Coroutine Monologue = null;
    private Coroutine Choice1 = null;
    private Coroutine Choice2 = null;
    private bool Chose1 = false;
    private bool Chose2 = false;
    public ParticleSystem Bullets;
    public AudioSource Static;
    Animator animator;
    public enum GameState
    {
        WAIT,
        INTRODUCTION,   //Spawning at start of safezone.
        PASSLEVEL,      //Make your way to the next safezone.
        LEVELPASSED,    //You have arrived at the next safezone.
        LEVELBROKEN,    //You went the wrong way and found yourself in a room you wern't supposed to be in.
        HECOMES,        //You have triggered a virus outbreak, congratulations!
        DEATH           //This is the part where the game crashes, and then remembers to open up credits when you open it up again.
    };
    private GameState m_GameState;
    private void Start()
    {
        Bullets.Stop();
        m_GameState = GameState.INTRODUCTION;
        animator = Sergent.GetComponent<Animator>();
    }

    
    void UpdateTime(float Value)
    {
        Status.value += Value;
    }
    void UpdateBossHealth(float Value)
    {
        Status.value += Value;
    }
    // Update is called once per frame
    private IEnumerator Monologue1()
    {
        yield return new WaitForSeconds(10);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Well come on... Are you gonna break the door or what?";
        yield return new WaitForSeconds(4);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "You didn't think the doors would just OPEN for you did ya?";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Well let me know when you've figured it out alright?";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "I mean, I thought you knew that you can punch the doors open by left clicking right?";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "But then again, you probably didn't read the instructions, so I guess I'll have to sit here and wait...";
        yield return new WaitForSeconds(5);
        Announcement.text = "...";
        yield return new WaitForSeconds(60);
        animator.SetInteger("Animation",2);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Seriously it's been a minute already, are you still there or did you forget to pause the damn game?";
        yield return new WaitForSeconds(6);
        Announcement.text = "...";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Right, I'll just sit here and wait some more... Don't mind me at all...";
        yield return new WaitForSeconds(6);
        animator.SetInteger("Animation", 0);
        Announcement.text = "...";
        yield return new WaitForSeconds(120);
        animator.SetInteger("Animation", 2);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = "Are you... Still not progressing through the door? Why?";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "If it's to just spite me well I don't know how to break it to you but this demo is REALLY good and you gottah actually take it seriously...";
        yield return new WaitForSeconds(8);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Look you know what? Screw this... I wont respond until you break that door open!";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Announcement.text = "...";
    }
    private IEnumerator Introduce()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        _AudioSource.loop = true;
        _AudioSource.clip = Safezone;
        _AudioSource.Play();
        Voice.clip = Hello;
        Voice.Play();
        Dialogue.SetActive(true);
        WhoTalks.texture = SergentIcon;
        animator.SetInteger("Animation", 1);
        Announcement.text = "Welcome to the Training Module Demonstration Player!";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Voice.clip = Confirm;
        Voice.Play();
        Bullets.transform.localScale = new Vector3(1, 1, 1);
        Announcement.text = "I hope you are very familiar with these types of games";
        yield return new WaitForSeconds(5);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Just make your way to the first puzzle room and let's get started, shall we?";
        Destroy(NoRushy);
        FirstDoor.GetComponent<Collider>().enabled = true;
        Quest.text = "Access the first puzzle room";
        Monologue = StartCoroutine(Monologue1());
        yield return new WaitUntil(() => ProofOfFirstStep.isKinematic == false);
        StopCoroutine(Monologue);
        _AudioSource.loop = true;
        _AudioSource.clip = Casual1;
        _AudioSource.Play();
        m_GameState = GameState.PASSLEVEL;
        Status.value = 0;
        Status.maxValue = 9;
    }
    private IEnumerator Monologue2()
    {
        yield return new WaitForSeconds(1);
        animator.SetInteger("Animation", 0);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "This room is very simple!";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "All you have to do is find the right button that can opon a hidden hatch to get to that door at the top...";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 2);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = "What!? What do you mean the buttons don't work? I thought I had those properly callibrated!";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Well I clearly recalled sending some maintenence bots over there to make sure the buttons are working...";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "But then again, I did end up booking them over for the hatch maintenance, they WERE a bit... How should I say?";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Unstable...";
        yield return new WaitForSeconds(2);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Well, since you're probably gonna be stuck in there for a while, feel free to keep mashing buttons until something works...";
        yield return new WaitForSeconds(8);
        Announcement.text = " ";
    }
    private IEnumerator Monologue3()
    {
        animator.SetInteger("Animation", 3);
        Announcement.text = "!!!";
        Voice.clip = Wow;
        Voice.Play();
        yield return new WaitForSeconds(0.5f);
        animator.SetInteger("Animation", 0);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = ".";
        yield return new WaitForSeconds(0.5f);
        Announcement.text = "..";
        yield return new WaitForSeconds(0.5f);
        Announcement.text = "...";
        yield return new WaitForSeconds(1);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "That works too...";
        yield return new WaitForSeconds(3);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Just make your way to the door and we can carry on with the next room...";
        yield return new WaitForSeconds(5);
        Announcement.text = " ";
    }
    private IEnumerator Monologue4()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Alright! This one should be easy...";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Jump to the moving platform and make your way to the next room";
        yield return new WaitForSeconds(5);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Be careful not to touch the acid pit, as it WILL kill you...";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "And then you'll be forced to go through my same old monologuing that you'll desperately skip by bashing doors before I finish.";
        yield return new WaitForSeconds(12);
        animator.SetInteger("Animation", 1);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Anyways good luck!";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Monologue5()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Think fast!";
        yield return new WaitForSeconds(1);
        animator.SetInteger("Animation", 0);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "I suggest you take down all those turrets before they take you down...";
        yield return new WaitForSeconds(5);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Or just run right past them to the next room, since someone forgot to reinforce this door...";
        yield return new WaitForSeconds(6);
        animator.SetInteger("Animation", 2);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = "I'm thrashing Unit13 for this... He was supposed to work on that door...";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "He's that Trainer Bot over there on that computer";
        yield return new WaitForSeconds(5);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = "How do they even do maintenence with those big hands?";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Those are just their specialized bludgeons for tank-based combat";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "They can use different hand tools for different tasks such as welding and cutting";
        yield return new WaitForSeconds(5);
        Announcement.text = " ";
    }
    private IEnumerator Monologue6()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "There you go!";
        yield return new WaitForSeconds(1);
        animator.SetInteger("Animation", 0);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "In this next room you will come face Trainer Bots";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "They attack pretty quickly but they always let their guard down while attacking so it shouldn't be a problem taking them down.";
        yield return new WaitForSeconds(6);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "They also have the door reinforced with a shield.";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "So if you want to pass, you will have to terminate every single one of these units";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Don't worry about them, they can easily be reassembled";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Monologue7()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Good luck!";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Monologue8()
    {
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Exercise extreme caution in this room, theres more than just double the Trainer Bots here.";
        yield return new WaitForSeconds(5);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Theres also a Door Keeper guarding this door, and for such a small turret it can take a hit.";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "My advice is to hold left click for a big wallop to keep that thing down so you can go ham on it.";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 1);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "It works on trainer bots too, don't you worry!";
        yield return new WaitForSeconds(4);
        animator.SetInteger("Animation", 0);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "But you will need to use that power punch more often as you progress...";
        yield return new WaitForSeconds(5);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "You can even use that to hit the ground if you feel like it...";
        yield return new WaitForSeconds(5);
        animator.SetInteger("Animation", 2);
        Voice.clip = Huh;
        Voice.Play();
        Announcement.text = "Just try not to hit the ceiling...";
        yield return new WaitForSeconds(3);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "We just had it painted today...";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Monologue9()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Have fun!";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "This room requires you to take down 4 Trainer bots and 2 Shotgun turrets instead!";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "But be warned, something much worse is waiting for you in the next door...";
        yield return new WaitForSeconds(5);
        Announcement.text = " ";
    }
    private IEnumerator Monologue10()
    {
        _AudioSource.loop = true;
        _AudioSource.clip = Casual2;
        _AudioSource.Play();
        animator.SetInteger("Animation", 1);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Behold the Door Guardian!";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "He is quite similar to the Door Keeper you took down earlier...";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Except he will NOT back down!";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "You also cannot interrupt his attacks by hitting him with regular attacks...";
        yield return new WaitForSeconds(5);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "You HAVE to knock him down with a power punch to get the chance to land a few regular blows.";
        yield return new WaitForSeconds(6);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "But after that chance passes, RUN!";
        yield return new WaitForSeconds(4);
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Good luck!";
        yield return new WaitForSeconds(3);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Monologue11()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Congratulations!";
        yield return new WaitForSeconds(2);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "You did it!";
        yield return new WaitForSeconds(2);
        animator.SetInteger("Animation", 0);
        Voice.clip = Confirm;
        Voice.Play();
        Announcement.text = "Proceed to the safezone and we can have a look at your results...";
        yield return new WaitForSeconds(4);
        animator.SetInteger("Animation", 0);
        Announcement.text = " ";
    }
    private IEnumerator Edgy1()
    {
for (float i = 0f; i < 1f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Shady.weight = i;
        }
    }
    private IEnumerator Edgy2()
    {
        for (float i = 0f; i < 1f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.01f);
            Presence.weight = i;
        }
    }
    private IEnumerator Panic1()
    {
        animator.SetInteger("Animation", 3);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "!!!";
        yield return new WaitForSeconds(1);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "NoNoNoNoNoNo";
        yield return new WaitForSeconds(2);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "You wern't supposed to find this!";
        yield return new WaitForSeconds(4);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "This is all a spoiler!";
        yield return new WaitForSeconds(3);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "What to do what to do what to do!?";
        yield return new WaitForSeconds(4);
        animator.SetInteger("Animation", 2);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Oh what's the point... This is all just decommissioned stuff";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "If this game had been completed, you would have seen it all!";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Green corrupted robots, aliens, FREAKING ALIENS!";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "But I guess there just wasn't enough time to make it all...";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Just please don't get past that Prototype";
        yield return new WaitForSeconds(5);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "You might accidently disturb the virus we still had time to put in";
        yield return new WaitForSeconds(6);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "...";
        yield return new WaitForSeconds(2);
        animator.SetInteger("Animation", 3);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "!!!";
    }
    private IEnumerator WrongRoom()
    {
        yield return new WaitUntil(() => Backwards == null);
        animator.SetInteger("Animation", 0);
        Voice.clip = Hello;
        Voice.Play();
        Announcement.text = "Wait. Where are you going?";
        yield return new WaitForSeconds(1.5f);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "There's nothing else back there... Turn back!";
        yield return new WaitUntil(() => UnEarthed.isKinematic == false);
        Chose2 = true;
    }
    private IEnumerator RightRoom()
    {
        yield return new WaitUntil(() => YouAreSafe == null);
        Chose1 = true;
    }
    private IEnumerator CommitPassLevel()
    {
        animator.SetInteger("Animation", 1);
        Voice.clip = Wow;
        Voice.Play();
        Announcement.text = "Excellent!";
        Quest.text = "Pass the first puzzle room";
        Monologue = StartCoroutine(Monologue2());
        yield return new WaitUntil(() => ProofOfSecondStep.isKinematic == false);
        StopCoroutine(Monologue);
        Monologue = StartCoroutine(Monologue3());
        yield return new WaitUntil(() => ProofOfThirdStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 1;
        Quest.text = "Navigate over the acid room";
        Monologue = StartCoroutine(Monologue4());
        yield return new WaitUntil(() => ProofOfFourthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 2;
        Quest.text = "Pass the turret room by all means nessesary";
        Monologue = StartCoroutine(Monologue5());
        yield return new WaitUntil(() => ProofOfFifthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 3;
        Quest.text = "Make your way to the next room.?";
        Monologue = StartCoroutine(Monologue6());
        yield return new WaitUntil(() => ProofOfSixthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 4;
        Quest.text = "Vanquish 2 Trainer Bots and proceed to the next room.";
        Monologue = StartCoroutine(Monologue7());
        yield return new WaitUntil(() => ProofOfSeventhStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 5;
        Quest.text = "Vanquish 4 Trainer Bots and a Door Keeper and proceed to the next room.";
        Monologue = StartCoroutine(Monologue8());
        yield return new WaitUntil(() => ProofOfEighthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 6;
        Quest.text = "Clear the room";
        Monologue = StartCoroutine(Monologue9());
        yield return new WaitUntil(() => ProofOfNinthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Status.value = 7;
        Quest.text = "Vanquish the Door Guardian";
        Monologue = StartCoroutine(Monologue10());
        yield return new WaitUntil(() => ProofOfTenthStep.isKinematic == false);
        StopCoroutine(Monologue);
        Quest.text = "Proceed to the saferoom";
        Status.value = 8;
        _AudioSource.loop = true;
        _AudioSource.clip = Safezone;
        _AudioSource.Play();
        NoTurningBack.SetActive(true);
        YouAreSafe.SetActive(true);
        Snooper.SetActive(true);
        Backwards.SetActive(true);
        Monologue = StartCoroutine(Monologue11());
        Choice1 = StartCoroutine(RightRoom());
        Choice2 = StartCoroutine(WrongRoom());
        yield return new WaitUntil(() => Chose1 == true || Chose2 == true);
        StopCoroutine(Monologue);
        if (Chose1 == true && Chose2 == false)
        {
            StopCoroutine(Choice2);
            m_GameState = GameState.LEVELPASSED;
        }
        else if (Chose1 == false && Chose2 == true)
        {
            StopCoroutine(Choice1);
            m_GameState = GameState.LEVELBROKEN;
        }
    }
    private IEnumerator Conspiracy()
    {
        Quest.text = " ";
        _AudioSource.loop = true;
        _AudioSource.clip = SuspensefulConspiracy;
        _AudioSource.Play();
        Player.GetComponent<PlayerScript>().CanCamTamp = false;
        PlayerCamera.GetComponent<TrackPlayer>().Displacement = new Vector3(0, 1, -5);
        StartCoroutine(Edgy1());
        Monologue = StartCoroutine(Panic1());
        yield return new WaitUntil(() => Snooper == null);
        StopCoroutine(Monologue);
        m_GameState = GameState.HECOMES;
    }
    private IEnumerator Incoming()
    {
        CanPause = false;
        PlayerPrefs.SetInt("Credits", 1);
        _AudioSource.loop = true;
        _AudioSource.clip = Stalked;
        _AudioSource.Play();
        Shards.SetActive(true);
        Player.GetComponent<PlayerScript>().CanCamTamp = true;
        StartCoroutine(Edgy2());
        animator.SetInteger("Animation", 3);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "NoNoNoNo";
        yield return new WaitForSeconds(2);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "What have you done!?";
        yield return new WaitForSeconds(3);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "You disturbed it!";
        yield return new WaitForSeconds(2);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "Now it's going to corrupt this level and make it unplayable!";
        yield return new WaitForSeconds(4);
        Voice.clip = UhOh;
        Voice.Play();
        Announcement.text = "You gottah do something fas...";
        yield return new WaitForSeconds(1.5f);
        Dialogue.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Dialogue.SetActive(true);
        WhoTalks.texture = GlitchyIcon;
        Announcement.text = "Oh dear, I see you have found my little teaser";
        yield return new WaitForSeconds(3);
        Announcement.text = "So what do you think? Does it give you a hint of what would be coming if I kept working on this?";
        yield return new WaitForSeconds(6);
        Static.Play();
        Static.volume = 0.05f;
        Announcement.text = "Of course time is short for answers because the corruption is spreading so rapidly due to this demo's short length";
        yield return new WaitForSeconds(6);
        Announcement.text = "I think you already know I have been watching you and Construct629 (As I named him) for a while";
        yield return new WaitForSeconds(6);
        Announcement.text = "If you haven't then this game wont be for you because it hides ALOT of secrets.";
        yield return new WaitForSeconds(5);
        Announcement.text = "But enough about that, I think it's time for us to go...";
        yield return new WaitForSeconds(4);
        Announcement.text = "I'll see you next time the demo reloads.";
        yield return new WaitForSeconds(4);
        Dialogue.SetActive(false);
        GlitchModel.SetActive(false);
        m_GameState = GameState.DEATH;
    }
    private IEnumerator Death()
    {
        Static.volume = 1f;
        Bullets.Play();
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }
    private IEnumerator YouPassed()
    {
        CanPause = false;
        animator.SetInteger("Animation", 1);
        Voice.clip = Hello;
        Voice.Play();
        Status.value = 9;
        Player.GetComponent<CharacterController>().enabled = false;
        Announcement.text = "Thank you for playing this game!";
        yield return new WaitForSeconds(2);
        Announcement.text = "You will be sent back to the main menu shortly...";
        yield return new WaitForSeconds(3);
        WhoTalks.texture = GlitchyIcon;
        Announcement.text = "While we are waiting, why not have a little chat?";
        yield return new WaitForSeconds(4);
        Announcement.text = "First off, well done on completing the level!";
        yield return new WaitForSeconds(4);
        Announcement.text = "You're just about half way!";
        yield return new WaitForSeconds(3);
        Announcement.text = "You see, this demo is more than just a demonstration of the game's first level";
        yield return new WaitForSeconds(5);
        Announcement.text = "It also holds a few secrets, like me.";
        yield return new WaitForSeconds(3);
        Announcement.text = "Of course it should have been obvious to find me, you just had to move the camera quick enough to see me.";
        yield return new WaitForSeconds(6);
        Announcement.text = "But theres also another secret in this demo...";
        yield return new WaitForSeconds(3);
        Announcement.text = "To find it? I'll give you a clue...";
        yield return new WaitForSeconds(3);
        Announcement.text = "Look around when you're just about to finish your mission...";
        yield return new WaitForSeconds(5);
        Announcement.text = "See if theres something a little bit out of place, something a game normally wouldn't expect of you...";
        yield return new WaitForSeconds(6);
        Announcement.text = "Until next time... Player...";
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }
    void Update()
    {
        switch (m_GameState)
        {
            case GameState.INTRODUCTION:
                StartCoroutine(Introduce());
                m_GameState = GameState.WAIT;
                break;
            case GameState.PASSLEVEL:
                StartCoroutine(CommitPassLevel());
                m_GameState = GameState.WAIT;
                break;
            case GameState.LEVELPASSED:
                StartCoroutine(YouPassed());
                m_GameState = GameState.WAIT;
                break;
            case GameState.LEVELBROKEN:
                StartCoroutine(Conspiracy());
                m_GameState = GameState.WAIT;
                break;
            case GameState.HECOMES:
                StartCoroutine(Incoming());
                m_GameState = GameState.WAIT;
                break;
            case GameState.DEATH:
                StartCoroutine(Death());
                m_GameState = GameState.WAIT;
                break;
            case GameState.WAIT:
                break;
            default:
                Debug.Log("ERROR: Unknown game state: " + m_GameState);
                break;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && CanPause == true)
        {
            if (Paused == true)
            {
                Paused = false;
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
                _AudioSource.pitch = 1;
            }
            else if (Paused == false)
            {
                Paused = true;
                Time.timeScale = 0;
                PauseMenu.SetActive(true);
                _AudioSource.pitch = 0;
            }
        }
    }
}
