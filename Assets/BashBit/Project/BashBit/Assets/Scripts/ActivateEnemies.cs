using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour
{
    public GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerScript targetHealth = other.GetComponent<PlayerScript>();
        if (targetHealth != null)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                if (Enemies[i].GetComponent<GreyboxHovererAI>() != null)
                {
                    Enemies[i].GetComponent<GreyboxHovererAI>().activated = true;
                }
                if (Enemies[i].GetComponent<TrainerBotAI>() != null)
                {
                    Enemies[i].GetComponent<TrainerBotAI>().activated = true;
                }
            }
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
