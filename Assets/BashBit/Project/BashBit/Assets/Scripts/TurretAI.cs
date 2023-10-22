using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretAI : MonoBehaviour
{
    public Collider Collisions;
    public GameObject[] Fragments;
    public GameObject Aimer;
    public GameObject Head;
    private GameObject LevelMaster;
    public Slider HealthSlider;
    public Image HealthDye;
    public float attackDistance = 1.5f; // Distance to start attack
    public float MaxHealth = 1f;
    public float Health = 1f;
    private GameObject m_player = null; // Player to pursue
    private float FlinchingThreshold = 1f;
    private float StunningThreshold = 5f;
    private float StunDuration = 5f;
    private bool dead = false;
    private bool stunned = false;
    private bool m_follow = false; // determines if it is following or not
    private bool Spawning = true;
    private AudioSource _AudioSource = null;
    public ParticleSystem Bullets;
    Animator animator;
    void Start()
    {
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = MaxHealth;
        animator = GetComponent<Animator>();
        Bullets.Stop();
        _AudioSource = gameObject.GetComponent<AudioSource>();
    }
    void Remove()
    {
        Destroy(gameObject);
    }
    public void RegisterDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            _AudioSource.Play();
            dead = true;
            animator.enabled = false;
            Collisions.enabled = false;
            Bullets.Stop();
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
    void Update()
    {
        if (dead == false)
        {
            HealthSlider.value = Health;
            if (Health != MaxHealth)
            {
                HealthSlider.gameObject.SetActive(true);
            }
            else
            {
                HealthSlider.gameObject.SetActive(false);
            }
            HealthDye.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), Health / MaxHealth);
            if (m_follow == true && stunned == false) // Checks if it is following
            {
                if (Bullets.isPlaying == false)
                {
                    Bullets.Play();
                }
                Vector3 direction = m_player.transform.position - Aimer.transform.position;
                Aimer.transform.rotation = Quaternion.Slerp(Aimer.transform.rotation,
                Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0), 0.75f);
                Vector3 direction2 = m_player.transform.position - Head.transform.position;
                Head.transform.rotation = Quaternion.Slerp(Head.transform.rotation,
                Quaternion.LookRotation(direction2) * Quaternion.Euler(0, 90, 0), 0.75f);
            }
            else
            {
                Bullets.Stop();
            }
        }
        else
        {
            HealthSlider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_player = other.gameObject;
            m_follow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            m_follow = false;
        }
    }
}
