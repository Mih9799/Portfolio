using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageDealer : MonoBehaviour 
{

	public enum DamageDealType
	{
		Once,
		EveryHit,
		OverTime,
	}

	public DamageDealType DamageType;

	public int Damage = 1;

	List<DamageTaker> OverTimeList;

	public float DamageTickTime = 1;
	float TickTimer = 0;
	

	public List<string> TriggerTags = new List<string>();	

	// Use this for initialization
	void Start () 
	{
		OverTimeList = new List<DamageTaker>();
	}
	
	// Update is called once per frame
	void Update ()
	{		
		if(DamageType == DamageDealType.OverTime && 
		   OverTimeList != null &&
		   OverTimeList.Count > 0 )
		{
			TickTimer += Time.deltaTime;

			if(TickTimer > DamageTickTime)
			{
				TickTimer -= DamageTickTime;

				for(int i = 0 ; i < OverTimeList.Count; ++i)
				{
					OverTimeList[i].TakeDamage(Damage);
				}
			}
		}
	
	}

	void ApplyMyDamage(DamageTaker Other )
	{
		if(DamageType == DamageDealType.Once)
		{
			Other.TakeDamage(Damage);
			Destroy(this);
		}
		if(DamageType == DamageDealType.EveryHit)
		{
			Other.TakeDamage(Damage);
		}
		if(DamageType == DamageDealType.OverTime)
		{
			OverTimeList.Add(Other);
		}
	}
	
	
	void OnTriggerEnter(Collider collided)
	{
		if(TriggerTags.Count > 0)
		{
			foreach(string TAG in TriggerTags)
			{
				if(collided.gameObject.CompareTag(TAG))
				{
					DamageTaker IHit = collided.gameObject.GetComponent<DamageTaker>();
					if(IHit != null)
					{
						ApplyMyDamage(IHit);
					}
					break;				
				}
			}
		}
	}
	
	void OnTriggerExit(Collider collided)
	{
		if(DamageType == DamageDealType.OverTime && 
		   OverTimeList != null &&
		   OverTimeList.Count > 0 )
		{
			for(int i = 0 ; i < OverTimeList.Count; ++i)
			{
				if(collided.gameObject.transform == OverTimeList[i].transform)
				{
					OverTimeList.RemoveAt(i);
					break;
				}
			}
		}
	}
}























