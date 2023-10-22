using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDespawn : MonoBehaviour
{
    public float Deathtime;
    void Start()
    {
        Invoke("Kill", Deathtime);
    }
    void Kill()
    {
     Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
