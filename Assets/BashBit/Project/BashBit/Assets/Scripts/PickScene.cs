using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickScene : MonoBehaviour
{
    private int DoCredits = 0;
    // Start is called before the first frame update
    void Start()
    {
        DoCredits = PlayerPrefs.GetInt("Credits");
            if (DoCredits == 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            SceneManager.LoadScene("Credits");
        }
    }
}
