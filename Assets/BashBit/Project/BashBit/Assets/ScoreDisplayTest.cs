using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayTest : MonoBehaviour {

    public Text ScoreText;

    int Currentscore = 5;

	// Use this for initialization
	void Start () {
        ScoreText.text = Currentscore.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpdateScoreText(1);
        }
		
	}

    void UpdateScoreText(int scoreToRemove)
    {
        Currentscore = Currentscore - scoreToRemove;
        Currentscore = Mathf.Clamp(Currentscore, 0, 5000);
        //this might be the area wherein you can perform any extyernal actions based upon the score result
        //eg id Currentscore <= 0 then end match or perform other action

        ScoreText.text = Currentscore.ToString();
    }
}
