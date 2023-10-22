using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InformationPopUpScript : MonoBehaviour {

	public bool isActive;

	public string message;

	public float xPosition;
	public float yPosition;

	public GameObject player;
	public Text textBox;

	// Use this for initialization
	void Start () 
	{
		textBox.enabled = false;
	}
	

	void OnTriggerEnter(Collider c)
	{
		//If player touches Information area Then
		if(c.gameObject == player) 
		{
			textBox.enabled = true;
		}
	}

	void OnTriggerExit(Collider c)
	{
		//If player exits Information area Then
		if(c.gameObject == player)
		{
			textBox.enabled = false;
		}
	}


	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
