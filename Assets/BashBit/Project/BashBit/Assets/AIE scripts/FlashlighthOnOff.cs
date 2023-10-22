using UnityEngine;
using System.Collections;

public class FlashlighthOnOff : MonoBehaviour {
	public bool LightOn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			if(LightOn){
				LightOn = false;
				gameObject.GetComponentInChildren<Light>().enabled = false;
			}
			else if (!LightOn){
				LightOn = true;
				gameObject.GetComponentInChildren<Light>().enabled = true;
			}

		}
	}
}
