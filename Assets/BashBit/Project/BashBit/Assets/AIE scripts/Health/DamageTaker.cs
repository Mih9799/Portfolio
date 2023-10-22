using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageTaker : MonoBehaviour 
{
	[System.Serializable]
	public class AnimationOject
	{
		public GameObject Target;
		public string AnimationName;
		public float Delay;
		
		bool DelayDone = false; 
		public bool IsDone()
		{
			return DelayDone;
		}
		public void Done()
		{
			DelayDone = true;
		}
		public void UnDone()
		{
			DelayDone = false;
		}
	};

	public bool UseHud = false;

	public List<GameObject> HudHearts;

	public int StartingHealth = 5;
	public int TotalHealth = 10;
	int CurrentHealth = 0;//StartingHealth;
	bool IsDead = false;

	
	public List<AnimationOject> TakeDamageAnimations;
	public List<AnimationOject> DeathAnimations;
	
	public bool DestroyGameObjectOnDeath = false;
	public float DestroyDelay = 0;

	public bool LoadLevelOnDeath = false;
	public float LoadLevelDelay = 0;
	public bool ThisLevel = true;
	public int OtherLevel = 0;

	// Use this for initialization
	void Start () 
	{

		CurrentHealth = StartingHealth;
		SetHud();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateTakeDamageAnimations();
		if(IsDead)
		{
			PlayDeathAnimation() ;

			if(DestroyGameObjectOnDeath)
			{
				DestroyDelay -= Time.deltaTime;
				if(DestroyDelay <= 0)
				{
					Destroy(gameObject);
				}
			}
			if(LoadLevelOnDeath)
			{
				LoadLevelDelay -= Time.deltaTime;
				if(LoadLevelDelay <= 0)
				{
					if(ThisLevel)
					{
						Application.LoadLevel(Application.loadedLevel);
					}
					else
					{
						Application.LoadLevel(OtherLevel);
					}
				}
			}
		}	
	}

	void SetHud()
	{
		if(UseHud)
		{
			for(int i = 0; i < HudHearts.Count; ++i)
			{
				if(i < CurrentHealth)
				{
					HudHearts[i].SetActive(true);
				}
				else
				{
					HudHearts[i].SetActive(false);
				}
			}
		}
	}

	public void TakeDamage(int Damage)
	{
		CurrentHealth -= Damage;

		if(CurrentHealth > TotalHealth)
		{
			CurrentHealth = TotalHealth;
		}

		SetHud();
		if(Damage > 0)
		{
			PlayTakeDamageAnimations();
		}

		if(!IsDead && CurrentHealth <=0)
		{
			IsDead = true;
		}
	}

	float DeathAnimationDelay = 0;
	bool DeathAnimationAllDone = false;

	void PlayDeathAnimation() 
	{
		if(DeathAnimations != null && !DeathAnimationAllDone)
		{			
			DeathAnimationDelay += Time.deltaTime;
			DeathAnimationAllDone  = true;
			
			foreach(AnimationOject AObject in DeathAnimations)
			{
				if(!AObject.IsDone())
				{
					DeathAnimationAllDone  = false;
					if(AObject.Delay < DeathAnimationDelay)
					{
						AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);
						
						AObject.Done();
					}
				}
			}
		}	
	}

	bool TakeDamageAnimationOn = false;
	float TakeDamageDelayTimer = 0;
	bool TakeDamgeAllDone = false; 

	void PlayTakeDamageAnimations()
	{
		
		TakeDamageAnimationOn = true;
		TakeDamgeAllDone = false;
		TakeDamageDelayTimer = 0;
		
		foreach(AnimationOject AObject in TakeDamageAnimations)
		{
			if(AObject.IsDone())
			{
				AObject.Target.GetComponent<Animation>()[AObject.AnimationName].enabled = true;
					
				AObject.Target.GetComponent<Animation>()[AObject.AnimationName].normalizedTime = 0;
					
				AObject.UnDone();

			}	 		
		}



	}

	void UpdateTakeDamageAnimations()
	{
		if(TakeDamageAnimationOn )
		{
			if(!TakeDamgeAllDone)
			{
				
				TakeDamageDelayTimer += Time.deltaTime;
				TakeDamgeAllDone  = true;
				
				foreach(AnimationOject AObject in TakeDamageAnimations)
				{
					if(!AObject.IsDone())
					{
						TakeDamgeAllDone  = false;
						if(AObject.Delay <= TakeDamageDelayTimer)
						{							
							AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);
							
							AObject.Done();
						}
					}
				}
			}
		}
	}
}


















