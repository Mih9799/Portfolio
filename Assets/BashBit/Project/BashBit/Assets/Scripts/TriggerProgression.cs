using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerProgression : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerScript targetHealth = other.GetComponent<PlayerScript>();
        if (targetHealth != null)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
