using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorMinibossAI : MonoBehaviour
{
    public Collider Collisions;
    public GameObject[] Fragments;
    private GameObject LevelMaster;
    public Slider HealthSlider;
    public Image HealthDye;
    public float MaxHealth = 40f;
    public float Health = 40f;
    private GameObject m_player = null; // Player to pursue
    private float FlinchingThreshold = 1f;
    private float StunningThreshold = 5f;
    private float StunDuration = 3f;
    private bool dead = false;
    public bool activated = false;
    private bool stunned = false;
    private bool Attacking = false;
    private bool m_follow = false; // determines if it is following or not
    private bool Spawning = true;
    private bool Inrange = false;
    public AudioClip StunConfuse;
    public AudioClip Screech;
    public AudioSource _AudioSource1;
    public AudioSource _AudioSource2;
    Animator animator;
    void Start()
    {
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = MaxHealth;
        animator = GetComponent<Animator>();
    }
    void Remove()
    {
        Destroy(gameObject);
    }
    void Revenge()
    {
        animator.ResetTrigger("Burn");
        animator.SetTrigger("Burn");
        Invoke("Burn", 4);
    }
    void Awakening()
    {
        stunned = false;
        animator.SetBool("Stunned", false);
        Invoke("Revenge", 0.25f);
    }
    IEnumerator Explode(float Damaged)
    {
        yield return new WaitForSeconds(1.4f);
        animator.enabled = false;
            Collisions.enabled = false;
            for (int i = 0; i < Fragments.Length; i++)
            {
                Fragments[i].GetComponent<Rigidbody>().useGravity = true;
                Fragments[i].GetComponent<Rigidbody>().isKinematic = false;
                Fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damaged / 2;
                Fragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damaged;
            }
        yield return new WaitForSeconds(5f);
        Remove();
        yield return null;
    }
    public void RegisterDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0 && dead == false)
        {
            _AudioSource1.clip = Screech;
            _AudioSource1.Play();
            _AudioSource2.Play();
            animator.SetTrigger("Death");
            animator.SetBool("Dead", true);
            dead = true;
            StartCoroutine(Explode(Damage));
        }
        else if (Damage >= StunningThreshold)
        {
            _AudioSource1.clip = StunConfuse;
            _AudioSource1.Play();
            stunned = true;
            Attacking = true;
            CancelInvoke();
            animator.SetBool("Stunned", true);
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            Invoke("Awakening", StunDuration);
        }
        else if (Damage >= FlinchingThreshold && stunned == true)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
        }
    }
    void Burn()
    {
        Attacking = false;
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
                if (Inrange == true && Attacking == false)
                {
                    animator.ResetTrigger("Laser");
                    animator.SetTrigger("Laser");
                    Attacking = true;
                    Invoke("Strike", 14.5f);
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
