using UnityEngine;
using System.Collections;

[AddComponentMenu("AIE Scripts/Spawn - Destroy/Spawn Object Simple")]
public class SpawnObjectSimple : MonoBehaviour 
{
	public GameObject ObjectToSpawn;

	public bool SpawnAtStart = false;
	public float StartSpawnDelay = 0;

	public bool SpawnAtInterval = false;
	public float SpawnInterval = 1;
	float Timer = 0;
	public bool LimitSpawnCount = false;
	public float SpawnCount = 10;

	// Use this for initialization
	void Start ()
	{
		if(SpawnAtStart)
		{
			if(StartSpawnDelay > 0)
			{
				Invoke("SpawnObject",StartSpawnDelay);
			}
			else
			{
				SpawnObject();
			}
		}	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(SpawnAtInterval)
		{
			if(StartSpawnDelay > 0)
			{
				Timer += Time.deltaTime;
				if(Timer > SpawnInterval)
				{
					if(LimitSpawnCount)
					{
						SpawnCount--;
						if(SpawnCount < 0)
						{
							SpawnAtInterval = false;
						}
						Timer = 0;
						SpawnObject();
					}
					else
					{
						Timer = 0;
						SpawnObject();
					}
				}
			}
			else
			{
				StartSpawnDelay -= Time.deltaTime;
			}
		}	
	}


	public void SpawnObject()
	{
		if(ObjectToSpawn != null)
		{
			Instantiate (ObjectToSpawn, transform.position, transform.rotation);
		}
		else
		{
			print("ERROR - " + gameObject.name + " - SpawnObject No Object To Spawn!!");
		}
	}
}




































