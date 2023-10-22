using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public GameObject[] Enemies;
    public Collider Collisions;
    public GameObject Shield;
    bool Unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Unlocked = true;
        for (int i = 0; i < Enemies.Length; i++)
        {
            if (Enemies[i] != null)
            {
                Unlocked = false;
            }
        }
        if (Unlocked == true)
        {
            Collisions.enabled = true;
            Destroy(Shield);
            Destroy(this);
        }
    }
}
