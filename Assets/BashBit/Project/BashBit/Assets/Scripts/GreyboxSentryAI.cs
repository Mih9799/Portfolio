using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreyboxSentryAI : MonoBehaviour
{
    public Collider Collisions;
    public GameObject Base;
    public GameObject Cube;
    public GameObject Eye;
    public GameObject Aimer;
    private GameObject LevelMaster;
    public Slider HealthSlider;
    public Image HealthDye;
    public float attackDistance = 1.5f; // Distance to start attack
    public float MaxHealth = 100f;
    public float Health = 100f;
    private GameObject m_player = null; // Player to pursue
    private float FlinchingThreshold = 1f;
    private float StunningThreshold = 5f;
    private float StunDuration = 5f;
    private bool dead = false;
    private bool stunned = false;
    private bool m_follow = false; // determines if it is following or not
    private bool Spawning = true;
    Animator animator;
    void Start()
    {
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = MaxHealth;
        //LevelMaster = GameObject.FindWithTag("LevelController");
        animator = GetComponent<Animator>();
    }
    void Remove()
    {
        Destroy(gameObject);
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
            dead = true;
            animator.enabled = false;
            Collisions.enabled = false;
            Base.GetComponent<Rigidbody>().useGravity = true;
            Base.GetComponent<Rigidbody>().isKinematic = false;
            Base.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
            Base.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            Cube.GetComponent<Rigidbody>().useGravity = true;
            Cube.GetComponent<Rigidbody>().isKinematic = false;
            Cube.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
            Cube.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            Eye.GetComponent<Rigidbody>().useGravity = true;
            Eye.GetComponent<Rigidbody>().isKinematic = false;
            Eye.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
            Eye.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            Invoke("Remove", 5f);
        }
        else if (Damage >= StunningThreshold)
        {
            stunned = true;
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            animator.SetBool("Stunned",true);
            Invoke("Awakening",StunDuration);
        }
        else if (Damage >= FlinchingThreshold)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
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
                float distance = (m_player.transform.position - transform.position).magnitude;
                if (distance < attackDistance)
                {
                    animator.SetBool("PlayerDetected", true);
                }
                else
                {
                    animator.SetBool("PlayerDetected", false);
                }
                Vector3 direction = m_player.transform.position - Aimer.transform.position;
                Aimer.transform.rotation = Quaternion.Slerp(Aimer.transform.rotation,
                Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0), 0.75f);
            }
            else
            {
                animator.SetBool("PlayerDetected", false);
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
