using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class SlowDown : MonoBehaviour {
	// Cheap way to slowdown the player.
	// Slows down gametime to set amount.
	// Object needs a trigger and the tag must be "Player"

	[SerializeField] [Range(0f, 1f)] private float speed = 1.0F;

	void OnTriggerStay(Collider Collidee){
		if(Collidee.gameObject.tag == "Player"){
			if(Collidee.gameObject.GetComponent<FirstPersonController>()){
				Time.timeScale = speed;
			}
			if(Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>()){
				Time.timeScale = speed;
			}
		}
	}
	void OnTriggerExit(Collider Collidee){
		if(Collidee.gameObject.GetComponent<FirstPersonController>()){
			Time.timeScale = 1f;
		}
		if(Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>()){
			Time.timeScale = 1;
		}
	}
}