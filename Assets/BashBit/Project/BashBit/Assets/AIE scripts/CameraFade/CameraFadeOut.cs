﻿using UnityEngine;
using System.Collections;

[AddComponentMenu("AIE Scripts/Camera Fade/Camera Fade Out")]
public class CameraFadeOut : MonoBehaviour {

	
	public bool IsFading = false;
	public bool UseDelay = false;
	public float DelayTimer = 0;
	float Ftimer = 0;
	public Color FadeColor = Color.black;
	public float FadeTime = 1;
	
	
	
	private GUIStyle m_BackgroundStyle = new GUIStyle();		// Style for background tiling
	private Texture2D m_FadeTexture;				// 1x1 pixel texture used for fading
	private int m_FadeGUIDepth = -1000;				// make sure this texture is drawn on top of everything
	
	
	// initialize the texture, background-style and initial color:
	private void Awake()
	{		
		m_FadeTexture = new Texture2D(1, 1);        
		m_BackgroundStyle.normal.background = m_FadeTexture;
	
			SetScreenOverlayColor(Color.clear);

	}
	
	void Update()
	{
		if(IsFading && UseDelay)
		{
			DelayTimer -= Time.deltaTime;
			if(DelayTimer < 0)
			{
				UseDelay = false;
			}
		}
		else if(IsFading)
		{
			Ftimer += Time.deltaTime;

			SetScreenOverlayColor(Color.Lerp(Color.clear,FadeColor,Ftimer/FadeTime));
		}
		
	}
	
	// draw the texture and perform the fade:
	private void OnGUI()
	{   
		// only draw the texture when the alpha value is greater than 0:
		if (IsFading )
		{			
			GUI.depth = m_FadeGUIDepth;
			GUI.Label(new Rect(-10, -10, Screen.width + 10, Screen.height + 10), m_FadeTexture, m_BackgroundStyle);
		}
	}
	
	public void SetScreenOverlayColor(Color newScreenOverlayColor)
	{
		m_FadeTexture.SetPixel(0, 0, newScreenOverlayColor);
		m_FadeTexture.Apply();
	}
}




















