using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[AddComponentMenu("AIE Scripts/Controls Disable/Controls Disable Simple")]
public class ControlsDisableSimple : MonoBehaviour {

	public bool DisableController = false;
	bool LastDisable = false;

	
	FirstPersonController TheFPScontrol;
	RigidbodyFirstPersonController TheFPScontrolR;


	// Use this for initialization
	void Start () 
	{
			TheFPScontrol = GameObject.FindObjectOfType<FirstPersonController>();
			TheFPScontrolR = GameObject.FindObjectOfType<RigidbodyFirstPersonController>();

		if(!TheFPScontrol && !TheFPScontrol)
		{
			if(TheFPScontrol == null)
			{
				print("ERROR - " + gameObject.name + " - No FirstPersonController Found");
			}
			if(TheFPScontrolR == null)
			{
				print("ERROR - " + gameObject.name + " - No RigidbodyFirstPersonController Found");
			}
		}



		ApplyDisable ();
		LastDisable = DisableController;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(LastDisable != DisableController)
		{
			ApplyDisable ();
			LastDisable = DisableController;
		}	
	}

	public void DisableAllControls ()
	{
		DisableController = true;
	}

	public void EnableAllControls()
	{
		DisableController = false;
	}


	void ApplyDisable()
	{
		if(TheFPScontrol != null)
		{
			TheFPScontrol.enabled = !DisableController;
			TheFPScontrol.GetComponent<CharacterController>().enabled = !DisableController;
		}
		if(TheFPScontrolR != null)
		{
			TheFPScontrolR.enabled = !DisableController;
			TheFPScontrolR.GetComponent<Rigidbody>().isKinematic = DisableController;
		}
	}
}






















