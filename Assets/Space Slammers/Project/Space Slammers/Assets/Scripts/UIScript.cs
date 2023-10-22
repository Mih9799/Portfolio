using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public GameManager master_script;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public Text scoretext;
    public GameObject ammo_indicator;
    public Text cluster;
    public Text homing;
    public Text scatter;
    public Text supersonic;
    public GameObject victory;
    public AudioSource background_music;
    public AudioClip victory_music;
    public GameObject effect;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }
    public void UpdateHealth(int hp)
    {
        switch (hp)
        {
            case 1:
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;
            case 2:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                break;
            case 3:
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                break;
            default:
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                break;

        }
    }
    public void SelectAmmo(int weaponid)
    {
        Vector3 aPos = ammo_indicator.GetComponent<RectTransform>().anchoredPosition;
        switch (weaponid)
        {
            case 1:
                aPos.y = 5;
                break;
            case 2:
                aPos.y = -5;
                break;
            case 3:
                aPos.y = -14;
                break;
            case 4:
                aPos.y = -24;
                break;
            case 0:
                aPos.y = 16;
                break;
        }
        ammo_indicator.GetComponent<RectTransform>().anchoredPosition = aPos;
    }

    public void UpdateAmmo(int weaponid, int ammo)
    {
        switch (weaponid)
        {
            case 1:
                cluster.text = "Cluster - " + ammo + "/24";
                break;
            case 2:
                homing.text = "Homing Missile - " + ammo + "/12";
                break;
            case 3:
                scatter.text = "Scattershot - " + ammo + "/8";
                break;
            case 4:
                supersonic.text = "Supersonic Wave - " + ammo + "/3";
                break;
            case 0:
                break;
        }
    }
    public void UpdateScore(int scored)
    {
        score += scored;
        if (score > 999999999)
        {
            score = 999999999;
        }
        GameObject data = GameObject.FindWithTag("Data");
        if (data != null && data.GetComponent<DataPersistence>() != null)
        {
            data.GetComponent<DataPersistence>().score = score;
        }
        scoretext.text = "Score - " + score.ToString("000000000");
    }

    public void VictoryScreen()
    {
        victory.SetActive(true);
        background_music.Stop();
        GameObject Yeah = Instantiate(effect, new Vector3(0,0,0), Quaternion.identity);
        Yeah.GetComponent<SoundEffect>().sound = victory_music;
    }

    public void GameOver()
    {
        master_script.SwitchState(3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
