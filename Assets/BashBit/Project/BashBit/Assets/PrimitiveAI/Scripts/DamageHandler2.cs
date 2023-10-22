using UnityEngine;
using System.Collections;

public class DamageHandler2 : MonoBehaviour {

    public bool destroyOnDeath = true;
    public float CurrentHealth = 100;
    public bool AppliesDamage = false;
    public float DamageAmount = 10;
    public GameObject DamageFX;

    [SerializeField] bool isBouncer = false;
    int numBounces = 0;
    int maxBounces = 2;

    [SerializeField] AudioSource damageAudioSrc;
    bool useAudioforDesctruction = true;
    private void Awake()
    {

        if (GetComponent<AudioSource>() && useAudioforDesctruction)
        {
            damageAudioSrc = GetComponent<AudioSource>();
        }
        else if (!GetComponent<AudioSource>())
        {
            useAudioforDesctruction = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (AppliesDamage)
        {
            if (!isBouncer)
            {
                Debug.Log("Hit Something");
                if (other.gameObject.GetComponent<DamageHandler>() != null)
                    {
                        other.gameObject.GetComponent<DamageHandler>().ApplyDamage(10.0f);
                    }

                //GameObject sparks = (GameObject)Instantiate(DamageFX, transform.position, transform.rotation);
                GameObject sparks = GameObject.Instantiate(DamageFX, transform.position, transform.rotation);
                Destroy(sparks, 3.14f);

                if (useAudioforDesctruction)
                {
                    damageAudioSrc.Play();
                    //destroy this actor once the duration of the audio has elapsed
                    if (destroyOnDeath)
                    {
                        Destroy(gameObject, damageAudioSrc.clip.length);
                    }                    
                }
                else if (!useAudioforDesctruction)
                {
                    if (destroyOnDeath)
                    {
                        Destroy(gameObject, damageAudioSrc.clip.length);
                    }
                }
                

                
            }
            else if (isBouncer)
            {
                numBounces++;
                if (numBounces >= maxBounces)
                {
                    if (other.gameObject.GetComponent<DamageHandler>() != null)
                    {
                        other.gameObject.GetComponent<DamageHandler>().ApplyDamage(10.0f);
                    }

                    spawnImpactFX();
                    if (useAudioforDesctruction)
                    {
                        damageAudioSrc.Play();
                        Destroy(gameObject, damageAudioSrc.clip.length);
                    }
                    else if (!useAudioforDesctruction)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

    }

    void spawnImpactFX()
    {
        GameObject sparks = GameObject.Instantiate(DamageFX, transform.position, transform.rotation);
        Destroy(sparks, 3.14f);
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
        spawnImpactFX();
    }


    void DeathEvent()
    {
        Debug.Log("This Thang He Deeed");
        Destroy(gameObject);
    }
}


