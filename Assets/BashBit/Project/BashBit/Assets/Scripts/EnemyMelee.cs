using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float Damage = 1;
    private float Cooldown = 0;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        PlayerScript targetHealth = other.GetComponent<PlayerScript>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(Damage,false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
