using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UIScript master_ui;
    public PlayerScript player;
    public enum StateType
    {
        DEFAULT,      //Fall-back state, should never happen
        DEFENDING,    //Spawning enemies
        ETERNAL,      //Constantly spawn enemies until the player dies for good
        SUCCEEDED,    //Player has beat the level, show level complete GUI
        FAILED,       //Player has lost all lives, show level failed GUI
        EXITING,      //The game is trying to return to menu.
        WAITING       //The game is waiting for an enumerator to finish.
    };
    public StateType starter;
    StateType currentstate;
    public GameObject[] squads;
    int squadindex = 0;
    private GameObject endlesssquad;
    // Start is called before the first frame update
    void Start()
    {
        currentstate = starter;
        if (starter == StateType.DEFENDING)
        {
            squads[squadindex].SetActive(true);
        }
    }

    public void SwitchState(int Type)
    {
        switch (Type)
        {
            case 1:
                currentstate = StateType.DEFENDING;
                break;
            case 2:
                currentstate = StateType.SUCCEEDED;
                break;
            case 3:
                currentstate = StateType.FAILED;
                break;
            case 4:
                currentstate = StateType.EXITING;
                break;
            case 5:
                currentstate = StateType.WAITING;
                break;
            case 0:
                currentstate = StateType.DEFAULT;
                break;
        }
    }

    IEnumerator Victory()
    {
        player.invincible = true;
        GameObject data = GameObject.FindWithTag("Data");
        if (data != null && data.GetComponent<DataPersistence>() != null)
        {
            data.GetComponent<DataPersistence>().highscore = data.GetComponent<DataPersistence>().score;
        }
        yield return new WaitForSeconds(1f);
        master_ui.VictoryScreen();
        yield return new WaitForSeconds(7f);
        Cursor.visible = true;
        SceneManager.LoadScene("Main_Menu", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentstate == StateType.ETERNAL)
        {
            if (endlesssquad == null)
            {
                GameObject newsquad = Instantiate(squads[UnityEngine.Random.Range(0, squads.Length - 1)]);
                foreach (Transform child in newsquad.transform)
                {
                    child.gameObject.GetComponent<EnemyAI>().master_ui = master_ui;
                }
                endlesssquad = newsquad;
            }
        }
        if (currentstate == StateType.DEFENDING)
        {
            if (squads[squadindex] == null && squadindex + 1 < squads.Length)
            {
                squadindex++;
                squads[squadindex].SetActive(true);
                print("Next Wave!");
            }
            if (squads[squadindex] == null && squadindex + 1 >= squads.Length)
            {
                currentstate = StateType.SUCCEEDED;
            }
        }
        if (currentstate == StateType.SUCCEEDED)
        {
            StartCoroutine(Victory());
            currentstate = StateType.WAITING;
        }
        if (currentstate == StateType.FAILED)
        {
            Cursor.visible = true;
            GameObject data = GameObject.FindWithTag("Data");
            if (data != null && data.GetComponent<DataPersistence>() != null && data.GetComponent<DataPersistence>().highscore < data.GetComponent<DataPersistence>().score)
            {
                data.GetComponent<DataPersistence>().highscore = data.GetComponent<DataPersistence>().score;
            }
            SceneManager.LoadScene("Game_Over", LoadSceneMode.Single);
        }
    }
}
