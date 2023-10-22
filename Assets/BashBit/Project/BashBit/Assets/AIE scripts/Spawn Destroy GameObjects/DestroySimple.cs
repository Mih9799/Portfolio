using UnityEngine;
using System.Collections;

[AddComponentMenu("AIE Scripts/Spawn - Destroy/Destroy Simple")]
public class DestroySimple : MonoBehaviour 
{
	public float DestroyDelay = 0;
	// Use this for initialization
	void Start () 
	{
		Destroy ();
	}

	public void Destroy()
	{
		Destroy (gameObject, DestroyDelay);
	}
}
