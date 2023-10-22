using UnityEngine;
using System.Collections;
// Script by Michael Kalis
// Last Updated 15/10/2015

public class moviePlay : MonoBehaviour {

	public enum Trigger
	{
		Play,
		Trigger,
		StopTrigger,
	}; // Type of Trigger
	
	public Trigger TriggerType;
	public GameObject TV;
	public MovieTexture Movie;

	public bool Loop;
	public string TriggerTag;


	// Use this for initialization
	void Start () {
		TV.GetComponent<Renderer>().material.mainTexture = Movie; //Setting the texture for video
		if(TriggerType == Trigger.Play){
			if(Loop == true){
				Movie.loop = true;
				Movie.Play();
			}
			else{
				Movie.loop = false;
				Movie.Play();
			}
		} // Enum Trigger Play
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag == TriggerTag){
			if(TriggerType == Trigger.Trigger){
				if(Loop == true){
					Movie.loop = true;
					Movie.Play();
				}
				else{
					Movie.loop = false;
					Movie.Play();
				}
			} // Enum Trigger Trigger, On enter play video
			else if(TriggerType == Trigger.StopTrigger){
				Movie.Stop();
			} // Enum Trigger Trigger, On enter stop video
		}
	} 
}
