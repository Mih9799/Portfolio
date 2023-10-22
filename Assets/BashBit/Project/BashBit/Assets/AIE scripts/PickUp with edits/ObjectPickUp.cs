using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

[System.Serializable]
public class SpringStuff
{
	public float SpringStrenght = 50;
	public float Damper = 5;
	public float MinDistance = 0;
	public float MaxDistance = 0.2f;
	public float RigidBodyDrag = 5;
	public float RigidBodyDragAngular = 5;
}

public class ObjectPickUp : MonoBehaviour 
{
	
	public bool LockMouse = true;
	//public bool InheretVelocity = true;
	
	
	public float Reach = 7;
	public float DistanceHeldFromCamera = 5;
	public List<string> BlockerTags; 
	RaycastHit RayOUT;
	
	bool HoldingObject = false;
	Rigidbody HeldObject ;
	Camera MainCamera;
	Vector3 DistanceFromCamera;
	
	public List<string> PickUpTags;

    public KeyCode PickupButton = KeyCode.Mouse0;
    public KeyCode DropButton = KeyCode.Mouse0;

    Rigidbody TheFPScontrolRigidbody;
	CharacterController TheFPScontrolCharControl;

	//public bool EnableSpringHold = true;
	public SpringStuff SpringGrab; 
	SpringJoint SP;

	float NomralDrag = 0;
	float NormalAdrag = 0.05f;
	// Use this for initialization
	void Start ()
	{		
		MainCamera = Camera.main;
		DistanceFromCamera = new Vector3(0.5f,0.5f,DistanceHeldFromCamera);

		
		FirstPersonController TheFPScontrol;
		RigidbodyFirstPersonController TheFPScontrolR;
		TheFPScontrol = GameObject.FindObjectOfType<FirstPersonController>();
		TheFPScontrolR = GameObject.FindObjectOfType<RigidbodyFirstPersonController>();

		if(TheFPScontrol != null)
		{
			TheFPScontrolCharControl = TheFPScontrol.GetComponent<CharacterController>();
		}
		if(TheFPScontrolR != null)
		{
			TheFPScontrolRigidbody = TheFPScontrolR.GetComponent<Rigidbody>();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(LockMouse)
		{
			if(!(Cursor.lockState == CursorLockMode.Locked) || Cursor.visible )
			{
			Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = true;
			}
		}
		
		if(Input.GetKeyDown(PickupButton))
		{
			
			if(!HoldingObject)
			{
				if(Physics.Raycast( MainCamera.ViewportPointToRay(new Vector3(0.5f,0.5f,0.5f)),out RayOUT,Reach))
				{
					if(RayOUT.rigidbody != null )
					{
						foreach(string TAG in PickUpTags)
						{
							if(RayOUT.rigidbody.gameObject.CompareTag(TAG))
							{
								HoldingObject = true;
								//RayOUT.rigidbody.isKinematic = false;
								RayOUT.collider.isTrigger = false;
								HeldObject = RayOUT.rigidbody;
								RayOUT.transform.gameObject.AddComponent<HeldObject>().PickUpScript = this;
								SP = RayOUT.transform.gameObject.AddComponent<SpringJoint>();
								SP.autoConfigureConnectedAnchor = false;
								SP.spring = SpringGrab.SpringStrenght;
								SP.damper = SpringGrab.Damper;
								SP.minDistance = SpringGrab.MinDistance;
								SP.maxDistance = SpringGrab.MaxDistance;
								SP.anchor = Vector3.zero;

								NomralDrag = RayOUT.rigidbody.drag;
								NormalAdrag = 	RayOUT.rigidbody.angularDrag;
								HeldObject.drag =  SpringGrab.RigidBodyDrag;
								HeldObject.angularDrag = SpringGrab.RigidBodyDragAngular;

								HeldObject.gameObject.transform.position = MainCamera.ViewportToWorldPoint(DistanceFromCamera);
								break;
							}						
						}
					}
				}
			}
		}

		if (HoldingObject)
		{
			if (Input.GetKeyUp(DropButton))
			{
				DropObject ();
			}
            if (HeldObject == null)
            {
                Destroy(SP);
                HoldingObject = false;
            }
        }

		if(HeldObject != null )
		{
			SP.connectedAnchor = MainCamera.ViewportToWorldPoint(DistanceFromCamera);
		}
    }


	public void DropObject()
	{
		if(HeldObject != null)
		{
			HoldingObject = false;
			HeldObject.isKinematic = false;
			HeldObject.GetComponent<Collider>().isTrigger = false;
			HeldObject.WakeUp();
			HeldObject.gameObject.GetComponent<HeldObject>().DestroyMe();
			//if(InheretVelocity && TheFPScontrolRigidbody != null)
			{
				if(TheFPScontrolRigidbody != null)
				{
					HeldObject.velocity += TheFPScontrolRigidbody.velocity;
				}
				else if(TheFPScontrolCharControl != null)
				{
					HeldObject.velocity += TheFPScontrolCharControl.velocity;
				}
			}

			HeldObject.drag =  NomralDrag;
			HeldObject.angularDrag = NormalAdrag;

			Destroy(SP);
			HeldObject = null;
		}
	}
}
