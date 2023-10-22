using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AnimationMultiSwitchOneShot : MultiSwitchBaseClass 
{
	// Update is called once per frame
	void Update () 
	{
		if(AllActivated && !AllDone)
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
						AObject.Target.GetComponent<Animation>().Play(AObject.AnimationName);

						AObject.Done();
					}
				}
			}
		}	
	}
}
































