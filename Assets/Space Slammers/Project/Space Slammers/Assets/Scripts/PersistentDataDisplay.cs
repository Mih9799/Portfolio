using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistentDataDisplay : MonoBehaviour
{
    public enum DataType
    {
        DEFAULT,      //Fall-back state, should never happen
        HIGHSCORE,    //Spawning enemies
        SCORE,    //Player has beat the level, show level complete GUI
        KILLS,       //Player has lost all lives, show level failed GUI
        BOSSKILLS,      //The game is trying to return to menu.
        DEATHS,       //The game is waiting for an enumerator to finish.
        POWERUPS,       //The game is waiting for an enumerator to finish.
    };
    public DataType chosen;
    public string compound;
    // Start is called before the first frame update
    void Start()
    {

        GameObject data = GameObject.FindWithTag("Data");
        if (data != null && data.GetComponent<DataPersistence>() != null)
        {
            switch (chosen)
            {
                case DataType.HIGHSCORE:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().highscore;
                    break;
                case DataType.SCORE:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().score;
                    break;
                case DataType.KILLS:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().kills;
                    break;
                case DataType.BOSSKILLS:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().bosskills;
                    break;
                case DataType.DEATHS:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().deaths;
                    break;
                case DataType.POWERUPS:
                    gameObject.GetComponent<Text>().text = compound + data.GetComponent<DataPersistence>().powerups;
                    break;
                case DataType.DEFAULT:
                    break;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
