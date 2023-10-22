using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ChangeSceneButton : MonoBehaviour {
    
    public string SceneName;
    public GameObject m_YourButton;
    // Use this for initialization
    void Start()
    {
        Button btn = m_YourButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClicked);
    }
    void OnClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneName);
    }
    void Update () {
		
	}
}
