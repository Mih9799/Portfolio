using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorGuardAI : MonoBehaviour
{
    public Collider Collisions;
    public GameObject[] Fragments;
    private GameObject LevelMaster;
    public Slider HealthSlider;
    public Image HealthDye;
    public float MaxHealth = 10f;
    public float Health = 10f;
    private GameObject m_player = null; // Player to pursue
    private float FlinchingThreshold = 1f;
    private float StunningThreshold = 5f;
    private float StunDuration = 3f;
    private bool dead = false;
    public bool activated = false;
    private bool Protecting = false;
    private bool stunned = false;
    private bool Attacking = false;
    private bool m_follow = false; // determines if it is following or not
    private bool Spawning = true;
    private bool Inrange = false;
    public AudioClip StunConfuse;
    public AudioClip Screech;
    private AudioSource _AudioSource = null;
    Animator animator;
    void Start()
    {
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = MaxHealth;
        animator = GetComponent<Animator>();
        _AudioSource = gameObject.GetComponent<AudioSource>();
    }
    void Remove()
    {
        Destroy(gameObject);
    }

    void NotChicken()
    {
        Protecting = false;
    }

    void Protect()
    {
        Protecting = true;
        animator.ResetTrigger("Dodge");
        animator.SetTrigger("Dodge");
        Invoke("NotChicken", 5);
    }

    void Awakening()
    {
        stunned = false;
        animator.SetBool("Stunned", false);
    }
    public void RegisterDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            _AudioSource.clip = Screech;
            _AudioSource.Play();
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            dead = true;
            animator.enabled = false;
            Collisions.enabled = false;
            for (int i = 0; i < Fragments.Length; i++)
            {
                Fragments[i].GetComponent<Rigidbody>().useGravity = true;
                Fragments[i].GetComponent<Rigidbody>().isKinematic = false;
                Fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
                Fragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            }
            Invoke("Remove", 5f);
        }
        else if (Damage >= StunningThreshold)
        {
            _AudioSource.clip = StunConfuse;
            _AudioSource.Play();
            stunned = true;
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            animator.SetBool("Stunned", true);
            Invoke("Awakening", StunDuration);
            Invoke("Protect", StunDuration + 0.5f);
        }
        else if (Damage >= FlinchingThreshold)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            Invoke("Protect", 0.5f);
        }
    }
    void Strike()
    {
        Attacking = false;
    }
    void Update()
    {
        if (dead == false && activated == true)
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
            if (m_player != null && stunned == false)
            {
                if (Inrange == true && Attacking == false && Protecting == false)
                {
                    animator.ResetTrigger("Attack");
                    animator.SetTrigger("Attack");
                    Attacking = true;
                    Invoke("Strike", 8);
                }
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
            Inrange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player") == true)
        {
            Inrange = false;
        }
    }
}
