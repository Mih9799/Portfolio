using UnityEngine;
using System.Collections;

public enum Baction
{
	ResetLevel,
	Quit,
	LoadLevelNumber,
	DoNothing,
};

[System.Serializable]
public class ButtonClickAction
{
	public Baction Action ;
	public int LevelNumber = 0;
}

[AddComponentMenu("AIE Scripts/Buttons/Button Simple")]
public class ButtonSimple : MonoBehaviour 
{
	public ButtonClickAction ClickAction;

	public bool UseColors = true;

	public Color BaseColor = new Color(137/255.0f,137/255.0f,137/255.0f);
	public Color MouseDownColor = new Color(1,240/255.0f,30/255.0f);
	public Color MouseHoverColor = new Color(1,1,1);
	
	//bool Enter =false;

	//bool Click =false;

	void Start()
	{
		if(UseColors)
		{
			GetComponent<Renderer>().material.color = BaseColor;
		}
	}

	void OnMouseEnter() 
	{
		if(UseColors)
			{
		GetComponent<Renderer>().material.color = MouseHoverColor;
		}
	}

	void OnMouseExit()
	{
		if(UseColors)
		{
			GetComponent<Renderer>().material.color = BaseColor;
		}
	}


	void OnMouseDown()
	{
		if(UseColors)
		{
			GetComponent<Renderer>().material.color = MouseDownColor;
		}
	}


//	void OnMouseUp() 
//	{
//	}


	void OnMouseUpAsButton()
	{
		if(ClickAction.Action == Baction.Quit)
		{
			Application.Quit();
		}
		if(ClickAction.Action == Baction.ResetLevel)
		{
			Application.LoadLevel(Application.loadedLevel); 
		}
		if(ClickAction.Action == Baction.LoadLevelNumber)
		{
			Application.LoadLevel(ClickAction.LevelNumber);
		}
	}
}































