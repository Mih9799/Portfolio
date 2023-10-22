using UnityEngine;
using System.Collections;

public class ApplicationAnimatedControl : MonoBehaviour {
	
	public ButtonClickAction MyAction;

	public bool Activate = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {	

		if(Activate)
		{
			DoAction();
			Activate = false;
		}
	}

	
	
	public void DoAction()
	{
		if(MyAction.Action == Baction.Quit)
		{
			Application.Quit();
		}
		if(MyAction.Action == Baction.ResetLevel)
		{
			Application.LoadLevel(Application.loadedLevel); 
		}
		if(MyAction.Action == Baction.LoadLevelNumber)
		{
			Application.LoadLevel(MyAction.LevelNumber);
		}
	}
}
