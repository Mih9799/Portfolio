using UnityEngine;
using System.Collections;

public class FogTrigger : MonoBehaviour {

	public bool bFog;
	public Color cColor;
	public float ffogDensity;
	public float ffogStartDistance;
	public float ffogEndDistance;
	public FogMode FfogMode;

/*
	RenderSettings.fog = true;
	RenderSettings.fogColor = Color.blue;
	RenderSettings.fogDensity = 0.1F;
	RenderSettings.fogStartDistance = 0.1F;
	RenderSettings.fogEndDistance = 0.1F;
	RenderSettings.fogMode = FogMode.Linear;
	//
		Linear
		Exponential
		ExponentialSquared
	//
*/

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
		RenderSettings.fog = bFog;
		RenderSettings.fogColor = cColor;
		RenderSettings.fogDensity = ffogDensity;
		RenderSettings.fogStartDistance = ffogStartDistance;
		RenderSettings.fogEndDistance = ffogEndDistance;
		RenderSettings.fogMode = FfogMode;
	}
}
