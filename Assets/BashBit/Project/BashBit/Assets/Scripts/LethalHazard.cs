using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LethalHazard : MonoBehaviour
{
    private float Damage = 40;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerScript targetHealth = other.GetComponent<PlayerScript>();
        if (targetHealth != null && targetHealth.health > 0)
        {
            targetHealth.TakeDamage(Damage, true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
