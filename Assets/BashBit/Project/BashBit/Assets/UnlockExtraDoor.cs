using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockExtraDoor : MonoBehaviour
{
    public InteractiveDoor doorToUnlock;

    private void OnCollisionEnter(Collision collision)
    {
        //when the player collides with this object
        if (collision.gameObject.tag == "Player")
        {
            //this condition checks whether or not the variable doorToUnlock is vald, or has been populated
            if (doorToUnlock)
            {
                doorToUnlock.UnlockDoor();
                doorToUnlock.OpenDoor();
            }

        }

    }
}
