using UnityEngine;
using System.Collections;

public class PlayAudioSource : MonoBehaviour {

    public AudioSource WorldSound;
    //We COULD specify the soundwe want to play here by creating apublic AudioClip var, 
    //we won;t here though, we want the AudioSource to be in control.

    //Do we want any actor to be able to trigger this event? or only the Player
    public bool onlyPlayerTriggers = true;

    bool hasFired = false;

    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag == "Player")
        {
            if (!hasFired)
            {
                WorldSound.Play();
                hasFired = true;
                gameObject.SetActive(false);
            }
            
        }
    }
}
