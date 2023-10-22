using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {
	public Color HoverTextColor = Color.red;
	public Color DefaultTextColor = Color.white;
	public bool isQuit;

	public string FirstLevelName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void OnMouseEnter(){
		//changes the text colour
		gameObject.GetComponent<Renderer>().material.color = HoverTextColor;
	}
	
	void OnMouseExit(){
		//changes the text colour
		gameObject.GetComponent<Renderer>().material.color = DefaultTextColor;
	}
	
	void OnMouseUp(){
		//when mouse is released
		if (isQuit==true) {
			//the function isQuit is applied as true so game will quit
			Application.Quit();
		}
		else {
			//else means if game doesnt quit it loads our next level or scene
			Application.LoadLevel(FirstLevelName);
		}
	}
}