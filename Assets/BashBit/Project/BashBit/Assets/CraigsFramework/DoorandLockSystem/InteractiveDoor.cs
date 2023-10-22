using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveDoor : MonoBehaviour {

    public bool isOpen = false;
    public bool isInteractive = false;
    public bool isLocked = true;
    public bool closeBehindPlayer = false;

    Animator doorAnimationController;

    Collider doorCollider;

    private void Start()
    {
        doorAnimationController = gameObject.GetComponent<Animator>();
    }

    //Temp: specify the GameObject to use as the Door mesh
    public GameObject DoorMesh;

    //Used for automatic opening mode
    //This will ensure the door attempts to open as soon as the player enters the Trigger Collider
    private void OnTriggerEnter(Collider other)
    {
        if (!isInteractive)
        {
            if (other.gameObject.tag == "Player")
            {
                if (isOpen == false && isLocked == false)
                {
                    OpenDoor();
                }
                else if (isLocked == true)
                {
                    Debug.Log("Door is locked!");
                    //You could call a 'Handle Shake' animation here and a sound to indicate the door is locked.
                }
            }
        }
        if (other.gameObject.tag == "Player" && isInteractive && PlayerHudManager.instance)
        {
            PlayerHudManager.instance.DisplayPlayerPrompt("Press Left Mouse Button to Interact", true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Hit Trigger");

        if (other.gameObject.tag == "Player")
        {
            if (Input.GetButtonUp("Fire1"))
            {
                //we can use a specific check agains the 'isOpen' bool to determine which action to take.
                if (isOpen == false && isLocked == false)
                {
                    OpenDoor();
                }
                else if (isLocked == true)
                {
                    Debug.Log("Door is locked!");
                    if (PlayerHudManager.instance)
                    {
                        PlayerHudManager.instance.DisplayPlayerMessage("This Door is Locked, Find a Key!");
                    }
                    //You could call a 'Handle Shake' animation here and a sound to indicate the door is locked.
                }
                else if (isOpen)
                {
                    CloseDoor();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {          
            if (isOpen == true)
            {
                if (closeBehindPlayer)
                {
                    CloseDoor();
                }                
            }
            if (isInteractive)
            {
                PlayerHudManager.instance.StartCoroutine(PlayerHudManager.instance.HidePlayerPrompt());
            }
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Door is Opening!");

        //DoorMesh.SetActive(false);
        doorAnimationController.SetBool("isDoorOpen", true);

        isOpen = true;
    }

    public void CloseDoor()
    {
        Debug.Log("Door is Closing!");

        //DoorMesh.SetActive(true);
        doorAnimationController.SetBool("isDoorOpen", false);
        isOpen = false;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }

    public void LockDoor()
    {
        isLocked = true;
    }
}
