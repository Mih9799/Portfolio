using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GreyboxHovererAI : MonoBehaviour
{
    private CharacterController characterController;
    public Collider Collisions;
    public GameObject Orientation;
    public GameObject Aimer;
    public GameObject RArm;
    public GameObject LArm;
    public GameObject[] Fragments;
    private GameObject LevelMaster;
    public Slider HealthSlider;
    public Image HealthDye;
    public float MaxHealth = 10f;
    public float Health = 10f;
    private GameObject m_player = null; // Player to pursue
    private float FlinchingThreshold = 1f;
    private float StunningThreshold = 5f;
    private float StunDuration = 5f;
    private bool dead = false;
    public bool activated = false;
    private bool stunned = false;
    private bool Attacking = false;
    private bool m_follow = false; // determines if it is following or not
    private bool Spawning = true;
    private bool IsRightD = true;
    private bool IsRightV = true;
    private bool Inrange = false;
    private Vector3 Velocitys = new Vector3(5,0,0);
    private bool CanTurn = true;
    Animator animator;
    void Start()
    {
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = MaxHealth;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Remove()
    {
        Destroy(gameObject);
    }
    public void Activate()
    {
        activated = true;
        animator.SetBool("Active", true);
    }
    void AntiTurnLoop()
    {
        CanTurn = true;
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
            characterController.enabled = false;
            for (int i = 0; i < Fragments.Length; i++) {
                Fragments[i].GetComponent<Rigidbody>().useGravity = true;
                Fragments[i].GetComponent<Rigidbody>().isKinematic = false;
                Fragments[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-3f, -1f)) * Damage / 2;
                Fragments[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-30f, 30f), Random.Range(-30f, 30f), Random.Range(-30f, 30f)) * Damage;
            }
            Invoke("Remove", 5f);
        }
        else if (Damage >= StunningThreshold)
        {
            stunned = true;
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
            animator.SetBool("Stunned", true);
            Invoke("Awakening", StunDuration);
        }
        else if (Damage >= FlinchingThreshold)
        {
            animator.ResetTrigger("Hit");
            animator.SetTrigger("Hit");
        }
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
                if (Inrange == true)
                {
                    animator.SetBool("Inrange", true);
                    Attacking = true;
                }
                else
                {
                    animator.SetBool("Inrange", false);
                    Attacking = false;
                }
                Vector3 direction = m_player.transform.position - Aimer.transform.position;
                if (direction.x < -0.25)
                    {
                        IsRightD = false;
                    }
                    else if (direction.x > 0.25)
                    {
                    IsRightD = true;
                    }
                Aimer.transform.rotation = Quaternion.Slerp(Aimer.transform.rotation,
                Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0), 0.75f);
                Vector3 direction2 = m_player.transform.position - RArm.transform.position;
                RArm.transform.rotation = Quaternion.Slerp(RArm.transform.rotation,
                Quaternion.LookRotation(direction2) * Quaternion.Euler(0, -90, 0), 0.75f);
                Vector3 direction3 = m_player.transform.position - LArm.transform.position;
                LArm.transform.rotation = Quaternion.Slerp(LArm.transform.rotation,
                Quaternion.LookRotation(direction3) * Quaternion.Euler(0, -90, 0), 0.75f);
            if (IsRightD == true)
            {
                    Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 0, 0), 25f * Time.deltaTime);
            }
            else
            {
                    Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 180, 0), 25f * Time.deltaTime);
            }
            }
            else
            {
                animator.SetBool("Inrange", false);
            }
            
            if (IsRightV == true)
            {
                if (m_player == null)
                {
                    Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 0, 0), 25f * Time.deltaTime);
                }
                Velocitys = Vector3.Lerp(Velocitys, new Vector3(5, 0, 0), 25f * Time.deltaTime); 
            }
            else
            {
                if (m_player == null)
                {
                    Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 180, 0), 25f * Time.deltaTime);
                }
                Velocitys = Vector3.Lerp(Velocitys, new Vector3(-5, 0, 0), 25f * Time.deltaTime);
            }
            if (Attacking == true || stunned == true)
            {
                Velocitys = Vector3.Lerp(Velocitys, new Vector3(0, 0, 0), 50f * Time.deltaTime);
            }
            characterController.Move(Velocitys * Time.deltaTime);
            if (gameObject.transform.position.z != 0f)
            {
                characterController.Move(new Vector3(0, 0, gameObject.transform.position.z * -1));
            }

            if (((characterController.collisionFlags & CollisionFlags.Sides) != 0)  && CanTurn == true)
            {
                IsRightV = !IsRightV;
                CanTurn = false;
                Invoke("AntiTurnLoop", 0.2f);
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