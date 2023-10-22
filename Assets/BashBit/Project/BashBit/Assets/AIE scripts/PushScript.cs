using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PushScript : MonoBehaviour {

	public List<string> Pushables = new List<string>();
	public KeyCode ActivationKey;
	public KeyCode ReverseActivationKey;
	public float Reach;
	public float PushForce;
	public LayerMask Mask;
	private Camera MainCam;

	// Use this for initialization
	void Awake () {
		MainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

	}
	
	// Update is called once per frame
	RaycastHit Hit;
	void Update () {
		if(Input.GetKeyDown(ActivationKey)){
			Vector3 fwd = MainCam.transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(MainCam.transform.position, fwd, out Hit, Reach, Mask)) {
				if(Pushables.Count > 0)
				{
                    print(Hit.rigidbody.gameObject.name);
					foreach(string TAG in Pushables)
					{
						if(Hit.rigidbody.gameObject.CompareTag(TAG))
						{
							Hit.rigidbody.gameObject.GetComponent<Rigidbody>().AddForce(fwd*PushForce);
						}
					}
				}
			}
		}
		else if(Input.GetKeyDown(ReverseActivationKey)){
			Vector3 fwd = MainCam.transform.TransformDirection(Vector3.forward);
			if (Physics.Raycast(MainCam.transform.position, fwd, out Hit, Reach, Mask)) {
				if(Pushables.Count > 0)
				{
					foreach(string TAG in Pushables)
					{
						if(Hit.rigidbody.gameObject.CompareTag(TAG))
						{
							Hit.rigidbody.gameObject.GetComponent<Rigidbody>().AddForce(-fwd*PushForce);
						}
					}
				}
			}
		}
	}
}
