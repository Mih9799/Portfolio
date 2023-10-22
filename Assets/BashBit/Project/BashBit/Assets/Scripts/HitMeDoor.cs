using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitMeDoor : MonoBehaviour
{
    private bool Busted = false;
    public Collider Collisions;
    public GameObject[] Fragments;
    private AudioSource _AudioSource = null;
    void Start()
    {
    _AudioSource = gameObject.GetComponent<AudioSource>();
    }
    void Remove()
    {
        Destroy(gameObject);
    }
    public void RegisterDamage(float Damage)
    {
        if (Busted == false)
        {
            Busted = true;
            Collisions.enabled = false;
            _AudioSource.Play();
            for (int i = 0; i < Fragments.Length; i++)
            {
                Fragments[i].GetComponent<Rigidbody>().useGravity = true;
                Fragments[i].GetComponent<Rigidbody>().isKinematic = false;
                Fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
                Fragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            }
            Invoke("Remove", 5f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
