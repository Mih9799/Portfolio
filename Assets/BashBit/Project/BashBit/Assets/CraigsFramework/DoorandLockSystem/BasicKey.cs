using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicKey : MonoBehaviour {

    public InteractiveDoor doorToUnlock;

    private void OnCollisionEnter(Collision collision)
    {    
        //when the player collides with this object
        if (collision.gameObject.tag == "Player")
        {

            //Check if this Key has no door associated with it.
            if (doorToUnlock)
            {
                //unlock my associated door
                doorToUnlock.UnlockDoor();

                if (PlayerHudManager.instance)
                {
                    PlayerHudManager.instance.DisplayPlayerMessage("You Found a Key!");
                }
                //destroy myself
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("This key is orphaned, it has no Door specified in the Inspector");
            }            
        }
    }
}
