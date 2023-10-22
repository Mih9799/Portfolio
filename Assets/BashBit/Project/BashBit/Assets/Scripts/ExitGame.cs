using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExitGame : MonoBehaviour {

    public GameObject m_YourButton;
    // Use this for initialization
    void Start()
    {
        Button btn = m_YourButton.GetComponent<Button>();
        btn.onClick.AddListener(OnClicked);
    }
    void OnClicked()
    {
        Application.Quit();
    }
    void Update()
    {

    }
}
