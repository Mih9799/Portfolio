using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Spawn - Destroy/Destroy Advanced")]
public class DestroyAdvanced : MonoBehaviour 
{
	public float DestroyDelay = 0;

	public List< GameObject> DestoyThisGameObject;

	public List< Object> DestroyThisOther;

	// Use this for initialization
	void Start () 
	{
		Destroy ();
	}
	
	public void Destroy()
	{
		if(DestoyThisGameObject != null && DestoyThisGameObject.Count > 0)
		{
			foreach(GameObject GG in DestoyThisGameObject)
			{
				Destroy (GG, DestroyDelay);
			}
		}
		if(DestroyThisOther != null && DestroyThisOther.Count > 0)
		{
			foreach(Object OO in DestroyThisOther)
			{
				Destroy (OO, DestroyDelay);
			}
		}
	}
}
