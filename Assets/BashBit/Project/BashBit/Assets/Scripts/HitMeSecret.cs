using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMeSecret : MonoBehaviour
{
    private bool Busted = false;
    public Collider Collisions;
    public GameObject[] BurstFragments;
    public GameObject[] TimedFragments;
    void Start()
    {
        
    }
    IEnumerator Drop(float DamageAftershock)
    {
        for (int i = 0; i < TimedFragments.Length; i++)
        {
            TimedFragments[i].GetComponent<Rigidbody>().useGravity = true;
            TimedFragments[i].GetComponent<Rigidbody>().isKinematic = false;
            TimedFragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * DamageAftershock / 2;
            TimedFragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * DamageAftershock;
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator Clean()
    {
        yield return new WaitForSeconds(5f);
        for (int i = 0; i < BurstFragments.Length; i++)
        {
            Destroy(BurstFragments[i]);
        }
        for (int i = 0; i < TimedFragments.Length; i++)
        {
           yield return new WaitForSeconds(0.06f);
           Destroy(TimedFragments[i]);
        }
        Destroy(gameObject);
    }
    public void RegisterDamage(float Damage)
    {
        if (Busted == false)
        {
            Busted = true;
            Collisions.enabled = false;
            for (int i = 0; i < BurstFragments.Length; i++)
            {
                BurstFragments[i].GetComponent<Rigidbody>().useGravity = true;
                BurstFragments[i].GetComponent<Rigidbody>().isKinematic = false;
                BurstFragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
                BurstFragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            }
            StartCoroutine(Drop(Damage));
            StartCoroutine("Clean");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
