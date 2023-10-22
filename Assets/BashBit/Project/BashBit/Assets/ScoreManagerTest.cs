using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerTest : MonoBehaviour {

    public int CurrentScore = 0;
    public Text ScoreText;
    //create an array to store all these objects in
    public CollectibleThing[] currentCollectibles;

    void findAllCollectibles()
    {
        //collect all object in the scene that have a given component or tag or other identifying feature
        //we will add these to a list, then we will loop over these items and essentially tell them that they belong to this Manager Script.

        currentCollectibles = FindObjectsOfType<CollectibleThing>();

        //set the currentscore to equal the number of items that were found
        CurrentScore = currentCollectibles.Length;

        //Loop over all of the found objects or Components and perform some operation
        foreach (CollectibleThing item in currentCollectibles)
        {
            item.gameScoreActor = this;
        }
    }

    private void Awake()
    {
        findAllCollectibles();
        //Write the new score out to a UI Text element.
        ScoreText.text = CurrentScore.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void modifyScore(int scoreToModifyBy)
    {
        CurrentScore -= scoreToModifyBy;

        if (CurrentScore <= 0)
        {
            GameOver(true);
        }        
        //Write the new score out to a UI Text element.
        ScoreText.text = CurrentScore.ToString();
    }

    void GameOver(bool PlayerWon)
    {
        print("You Won!!!");
    }
}
