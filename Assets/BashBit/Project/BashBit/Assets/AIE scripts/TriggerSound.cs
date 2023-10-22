using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerSound : MonoBehaviour {

	public List<string> TriggerTags = new List<string>();
	public AudioClip Sound2Play;
	public bool PlayOnce;
	public bool StopSound;
	public bool OnExitStopSound;

	private bool Played = false;
	
	void OnTriggerEnter(Collider collided)
	{
		if(!Played){
			if(TriggerTags.Count > 0)
			{
				foreach(string TAG in TriggerTags)
				{
					if(collided.gameObject.CompareTag(TAG))
					{
						if(StopSound){
							GetComponent<AudioSource>().Stop ();
						}
						GetComponent<AudioSource>().PlayOneShot(Sound2Play);
						if(PlayOnce){
							Played = true;
						}
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{
					if(OnExitStopSound == true){
						GetComponent<AudioSource>().Stop ();
					}
				}
			}
		}
	}
}
