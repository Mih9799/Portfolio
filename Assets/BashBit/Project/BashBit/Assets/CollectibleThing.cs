using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleThing : MonoBehaviour {

    public ScoreManagerTest gameScoreActor;
    public int ScoreValue = 1;

    //detect the player overlap
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //signal score manager that players score should change
            gameScoreActor.modifyScore(ScoreValue);
            //destroy self
            Destroy(gameObject);
        }
    }




    
}
