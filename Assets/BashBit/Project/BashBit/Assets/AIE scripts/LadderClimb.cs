using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class LadderClimb : MonoBehaviour {
	public float speed = 6.0F;
	private Vector3 moveDirection = Vector3.zero;
	private Rigidbody m_RigidBody;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider Collidee){
		if(Collidee.gameObject.tag == "Player"){
			if(Collidee.gameObject.GetComponent<FirstPersonController>()){
				Collidee.gameObject.GetComponent<FirstPersonController>().enabled = false;
				CharacterController controller = Collidee.gameObject.GetComponent<CharacterController>();
				Collidee.gameObject.GetComponent<CharacterController>();
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				controller.Move(moveDirection * Time.deltaTime);
			}
			if(Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>()){
				m_RigidBody = Collidee.gameObject.GetComponent<Rigidbody>();
				Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = false;
				m_RigidBody.MovePosition(Collidee.gameObject.transform.position + new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0)*speed * Time.deltaTime);
			}
		}
	}
	void OnTriggerExit(Collider Collidee){
		if(Collidee.gameObject.GetComponent<FirstPersonController>()){
			Collidee.gameObject.GetComponent<FirstPersonController>().enabled = true;
		}
		if(Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>()){
			Collidee.gameObject.GetComponent<RigidbodyFirstPersonController>().enabled = true;
		}
	}
}