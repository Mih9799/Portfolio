using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {


    public float CurrentHealth = 100;
    public bool AppliesDamage = false;
    public float DamageAmount = 10;
    public ParticleSystem DamageFX;

    void OnCollisionEnter(Collision other)
    {
        if (AppliesDamage)
        {
            Debug.Log("Hit Something");
            if (other.gameObject.GetComponent<DamageHandler>() != null)
            {
                other.gameObject.GetComponent<DamageHandler>().ApplyDamage(10.0f);
            }

            ParticleSystem sparks = (ParticleSystem)Instantiate(DamageFX, transform.position, transform.rotation) as ParticleSystem;

            Destroy(gameObject);
            
        }

    }



    public void ApplyDamage(float DamageAmount)
    {
        if (CurrentHealth - DamageAmount > 0)
        {
            CurrentHealth = CurrentHealth - DamageAmount;
            print(DamageAmount + " Damage Applied to: " + gameObject.name);
        }
        else
        {
            DeathEvent();
        }
    }


    void DeathEvent()
    {
        Debug.Log("This Thang He Deeed");
        Destroy(gameObject);
    }
}


