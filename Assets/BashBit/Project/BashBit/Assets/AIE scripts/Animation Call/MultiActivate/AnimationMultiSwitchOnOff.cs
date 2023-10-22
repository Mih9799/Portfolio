using UnityEngine;
using System.Collections;

public class AnimationMultiSwitchOnOff : MultiSwitchBaseClass 
{
	// Update is called once per frame
	void Update () 	
	{

		if(AllActivated )
		{
			if(!AllDone)
			{
				
				DelayTimer += Time.deltaTime;
				AllDone  = true;
				
				foreach(AnimationOject AObject in TheAnimations)
				{
					if(!AObject.IsDone())
					{
						AllDone  = false;
						if(AObject.Delay < DelayTimer)
						{
							AObject.Target.GetComponent<Animation>()[AObject.AnimationName].speed = 1;
					
								AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);
							
							AObject.Done();
						}
					}
				}
			}
		}
		else if(!AllDone)
		{
			
			AllDone  = true;
			if(DelayTimer > 0)
			{
				DelayTimer -= Time.deltaTime;
			}
			foreach(AnimationOject AObject in TheAnimations)
			{
				if(AObject.IsDone())
				{
					AllDone  = false;
					if(AObject.Delay > DelayTimer)
					{
						AObject.Target.GetComponent<Animation>()[AObject.AnimationName].speed *= -1;
						AObject.Target.GetComponent<Animation>()[AObject.AnimationName].enabled = true;
						if(AObject.Target.GetComponent<Animation>()[AObject.AnimationName].normalizedTime == 0)
						{
							AObject.Target.GetComponent<Animation>()[AObject.AnimationName].normalizedTime = 1;
						}
							AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);
							
						AObject.UnDone();
					}
				}			
			}
		}
	
	}
}
