using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSequence : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator Credits()
    {
        yield return new WaitForSeconds(65);
        SceneManager.LoadScene("Menu");
    }
    void Start()
    {
        PlayerPrefs.SetInt("Credits", 0);
        StartCoroutine(Credits());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
