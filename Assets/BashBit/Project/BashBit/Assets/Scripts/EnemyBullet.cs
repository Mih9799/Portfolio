using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public float Damage = 1f;
    public bool BypassInvincibility = false;
    // Use this for initialization
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    void OnParticleCollision(GameObject other)
    {
        PlayerScript targetHealth = other.GetComponent<PlayerScript>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(Damage, BypassInvincibility);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
