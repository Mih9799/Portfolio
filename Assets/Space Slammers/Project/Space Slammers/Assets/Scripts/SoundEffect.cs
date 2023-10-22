using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    public AudioClip sound;
    public float volume;
    private bool loaded = false;
    private bool playing = false;
    // Start is called before the first frame update

    void DoPlay()
    {
        gameObject.GetComponent<AudioSource>().clip = sound;
        gameObject.GetComponent<AudioSource>().volume = volume;
        gameObject.GetComponent<AudioSource>().Play();
        Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length);
    }

    void Update()
    {
        if (playing == false && loaded == true)
        {
            playing = true;
            DoPlay();
        }
        else
        {
            if (sound != null)
            {
                loaded = true;
            }
        }
    }
}
