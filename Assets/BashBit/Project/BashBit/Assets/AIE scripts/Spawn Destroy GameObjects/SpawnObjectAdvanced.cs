using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("AIE Scripts/Spawn - Destroy/Spawn Object Advanced")]
public class SpawnObjectAdvanced : MonoBehaviour
{

	public List<GameObject> ObjectToSpawn;
	
	public bool SpawnAtStart = false;
	public float StartSpawnDelay = 0;
	
	public bool SpawnAtInterval = false;
	public float SpawnInterval = 1;
	float Timer = 0;
	public bool LimitSpawnCount = false;
	public float SpawnCount = 10;
	
	public bool UsePosition = false;
	
	public bool UseRotation = false;
	
	public bool UseScale = false;
	public List<Transform> SpawnLocations;


	public bool RandomPosition = false;
	public Vector3 MaxPosOffset = new Vector3(2,2,2);

	public bool RandomRotation = false;



	public bool RandomScale = false;
	public Vector3 MinRandomScale = new Vector3(0.1f,0.1f,0.1f);
	public Vector3 MaxRandomScale = new Vector3(2,2,2);

	public bool UniformRandomScale = false;
	public Vector3 BaseScale = Vector3.one;
	public float MinScaleUniform = 0.5f;
	public float MaxScaleUniform = 2;
	
	// Use this for initialization
	void Start ()
	{
		ErrorCheck ();

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
			if(StartSpawnDelay < 0)
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
	
	GameObject Spawned;
	int RandomID = 0;
	Vector3 SpawnPos;
	Quaternion SpawnRotation;
	Vector3 SpawnScale;
	Vector3 TempV;

	public void SpawnObject()
	{
		if(ObjectToSpawn != null && ObjectToSpawn.Count > 0)
		{				
			SpawnPos = transform.position;
			SpawnRotation = transform.rotation;
			SpawnScale = transform.lossyScale;
			
			RandomID = Random.Range(0,SpawnLocations.Count);
			if(UsePosition)
			{
				SpawnPos = SpawnLocations[RandomID].position;
			}
			
			if(UseRotation)
			{
				SpawnRotation = SpawnLocations[RandomID].rotation;
			}
			
			if(UseScale)
			{
				SpawnScale = SpawnLocations[RandomID].lossyScale;
			}

			if(RandomPosition)
			{
				TempV = Random.insideUnitSphere;
				TempV.x *= MaxPosOffset.x;
				TempV.y *= MaxPosOffset.y;
				TempV.z *= MaxPosOffset.z;
				SpawnPos += TempV;
			}

			
			if(RandomRotation)
			{
				SpawnRotation = Random.rotationUniform;
			}
			
			if(RandomScale)
			{
				SpawnScale = new Vector3(Random.Range(MinRandomScale.x,MaxRandomScale.x),
				                         Random.Range(MinRandomScale.y,MaxRandomScale.y),
				                         Random.Range(MinRandomScale.z,MaxRandomScale.z));
			}
			if(UniformRandomScale)
			{
				SpawnScale =  BaseScale * Random.Range(MinScaleUniform,MaxScaleUniform);
			}
			
			Spawned = Instantiate (ObjectToSpawn[Random.Range(0,ObjectToSpawn.Count)], SpawnPos, SpawnRotation)as GameObject;

			if(RandomScale || UseScale)
			{
				Spawned.transform.localScale = SpawnScale;
			}

		}
		else
		{
			print("ERROR - " + gameObject.name + " - SpawnObject No Object To Spawn!!");
		}
	}


	void ErrorCheck()
	{
		if(!(ObjectToSpawn != null && ObjectToSpawn.Count > 0))
		{
			print("ERROR - " + gameObject.name + " - SpawnObject No Object To Spawn!!");
		}


		if(UsePosition || UseRotation  || UseScale)
		{
			if(SpawnLocations == null || SpawnLocations.Count <=0)
			{
				print("ERROR - " + gameObject.name + " - SpawnObject No Spawn Locations Linked In!!");
			}
		}
		
	
		if(UseRotation && RandomRotation)
		{
			print("ERROR - " + gameObject.name + " - SpawnObject Both Rotation Options Selected!!");
		}

		if(UseScale && RandomScale)
		{
			print("ERROR - " + gameObject.name + " - SpawnObject Both Scale Opetions Selected!!");
		}
		else if(RandomScale && UniformRandomScale)
		{
			print("ERROR - " + gameObject.name + " - SpawnObject Both Scale Opetions Selected!!");
		}
		else if(UseScale && UniformRandomScale)
		{
			print("ERROR - " + gameObject.name + " - SpawnObject Both Scale Opetions Selected!!");
		}
	}
}











































