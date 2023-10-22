using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

[AddComponentMenu("AIE Scripts/Controls Disable/Controls Disable Advanced")]
public class ControlsDisableAdvanced : MonoBehaviour 
{

	public bool ActivateDelayAtStart = false;
	public float ActivateDelayAtStartTime = 1;

	
	public bool DisableControllerMovement = false;
	bool LastDisableMovement = false;
	
	public bool DisableControllerCamera = false;
	bool LastDisableCamera = false;


	
	
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
		LastDisableMovement = DisableControllerMovement;
		LastDisableCamera = DisableControllerCamera;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(ActivateDelayAtStart)
		{
			ActivateDelayAtStartTime -=  Time.deltaTime;
			if(ActivateDelayAtStartTime < 0)
			{
				ActivateDelayAtStart = false;
				EnableAll();
			}
		}
		if(LastDisableMovement != DisableControllerMovement || 
		   LastDisableCamera != DisableControllerCamera)
		{
			ApplyDisable ();
			LastDisableMovement = DisableControllerMovement;
			LastDisableCamera = DisableControllerCamera;
		}	
	}
	
	public void DisableMovement()
	{
		DisableControllerMovement = true;
	}
	
	public void EnableMovement()
	{
		DisableControllerMovement = false;
	}
	
	public void DisableCmaera()
	{
		DisableControllerCamera = true;
	}
	
	public void EnableCamera()
	{
		DisableControllerCamera = false;
	}
	
	public void DisableAll()
	{
		DisableControllerMovement = true;
		DisableControllerCamera = true;
	}
	
	public void EnableAll()
	{
		DisableControllerMovement = false;
		DisableControllerCamera = false;
	}
	
	
	
	void ApplyDisable()
	{
		if(TheFPScontrol != null)
		{
			TheFPScontrol.enabled = !DisableControllerCamera;
			TheFPScontrol.GetComponent<CharacterController>().enabled = !DisableControllerMovement;
		}
		if(TheFPScontrolR != null)
		{
			TheFPScontrolR.enabled = !DisableControllerCamera;
			TheFPScontrolR.GetComponent<Rigidbody>().isKinematic = DisableControllerMovement;
		}
	}
}


























