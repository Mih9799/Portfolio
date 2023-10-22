using UnityEngine;
using System.Collections;

public class ChangeGravity : MonoBehaviour {

	public string TriggerTag;
	public float gravity = -9.81f;

	void OnTriggerEnter(Collider collision){
		if(collision.gameObject.tag == TriggerTag){
			Physics.gravity = new Vector3(0, gravity, 0);
		}
	}
}