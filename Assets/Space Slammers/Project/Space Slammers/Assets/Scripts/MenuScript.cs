﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour 
{
	public void LoadScene(string sceneName)
	{
		if (sceneName == null)
			Debug.Log("<color=orange>"+gameObject.name+": No Scene Name Was given for LoadScene function!</color>");
		SceneManager.LoadScene(sceneName); //load a scene
	}
	public void Exit()
	{
		Application.Quit();
	}
}
