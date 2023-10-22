using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour {
    Animator animator;
    private Vector3 lookPos;
	private CharacterController characterController;
    private Rigidbody CharacterPhysics;
    private bool Reeling = false;
    public GameObject PlayerCamera;
    public Slider HealthSlider;
    public Image HealthDye;
    public Slider PowerSlider;
    public Slider SpeedSlider;
    public Image PowerDye;
    public Image SpeedDye;
    public bool CanCamTamp = true;
    public float speed = 6;
	public float jumpSpeed = 8;
	public float gravity = 20;
    public bool FistJumping = false;
    private float FistJumperTimer = 0f;
	public float health = 40;
	public float maxHealth = 40;
    public GameObject Head;
    public GameObject Torso;
    public GameObject RightLeg;
    public GameObject LeftLeg;
    public GameObject RightArm;
    public GameObject LeftArm;
    public GameObject RArm;
    public GameObject LArm;
    public GameObject Orientation;
    private Collider RightFist;
    private Collider LeftFist;
    private float desiredmovement;
    private Vector3 movementVector;
    private Vector3 CharacterVelocity;
	public bool isDead = false;
    private bool FacingForward = true;
    private bool IsRightArm = true;
    private bool Invincible = false;
    private Vector3 Knockback;
    private float TimerA;
    private float PunchAmount;
    private float AverageRate;
    private AudioSource _AudioSource = null;
    Scene m_Scene;
    string sceneName;
    // Use this for initialization
    void Start () 
	{
        _AudioSource = gameObject.GetComponent<AudioSource>();
        HealthSlider.minValue = 0f;
        HealthSlider.maxValue = maxHealth;
        PowerSlider.minValue = 0f;
        PowerSlider.maxValue = 20f;
        SpeedSlider.minValue = 0f;
        SpeedSlider.maxValue = 12f;
        m_Scene = SceneManager.GetActiveScene();
        sceneName = m_Scene.name;
        animator = GetComponent<Animator>();
        CharacterPhysics = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        RightFist = RArm.GetComponent<Collider>();
        LeftFist = LArm.GetComponent<Collider>();
    }
    public void Recoil(float Power, GameObject HitArm)
    {
        GameObject Recoiler = HitArm.transform.parent.gameObject;
        Knockback = (Recoiler.transform.position - HitArm.transform.position).normalized * Power * 2;
        if (CanCamTamp == true)
        {
            PlayerCamera.GetComponent<TrackPlayer>().Displacement.z = -15 - (0.5f * Power);
        }
        FistJumperTimer = 0.25f;
        FistJumping = true;
    }
    private void DamageCooldown()
    {
        Invincible = false;
    }
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.3f);
        if (CanCamTamp == true)
        {
            PlayerCamera.GetComponent<TrackPlayer>().Displacement.z = -15;
        }
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
        yield return null;
    }
    public void TakeDamage(float Damaged, bool InvincibilityBypass)
    {
        if (Invincible == true && InvincibilityBypass == false)
        {
            return;
        }
        else
        {
            health -= Damaged;
            Invincible = true;
            Invoke("DamageCooldown", 0.1f);
            if (health <= 0 && isDead == false)
            {
                _AudioSource.Play();
                isDead = true;
                animator.enabled = false;
                Time.timeScale = 0.25f;
                if (CanCamTamp == true)
                {
                    PlayerCamera.GetComponent<TrackPlayer>().Displacement.y = 0f;
                    PlayerCamera.GetComponent<TrackPlayer>().Displacement.z = -5f;
                }
                Head.GetComponent<Rigidbody>().useGravity = true;
                Head.GetComponent<Rigidbody>().isKinematic = false;
                Head.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                Head.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                Torso.GetComponent<Rigidbody>().useGravity = true;
                Torso.GetComponent<Rigidbody>().isKinematic = false;
                Torso.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                Torso.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                RArm.GetComponent<Rigidbody>().useGravity = true;
                RArm.GetComponent<Rigidbody>().isKinematic = false;
                RArm.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                RArm.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                LArm.GetComponent<Rigidbody>().useGravity = true;
                LArm.GetComponent<Rigidbody>().isKinematic = false;
                LArm.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                LArm.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                RightLeg.GetComponent<Rigidbody>().useGravity = true;
                RightLeg.GetComponent<Rigidbody>().isKinematic = false;
                RightLeg.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                RightLeg.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                LeftLeg.GetComponent<Rigidbody>().useGravity = true;
                LeftLeg.GetComponent<Rigidbody>().isKinematic = false;
                LeftLeg.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 1f), Random.Range(-1f, 1f)) * Damaged / 2;
                LeftLeg.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)) * Damaged;
                StartCoroutine(Die());
            }
        }
    }
    // Update is called once per frame
    IEnumerator Charge()
    {
        bool Looping = true;
        float Power = 0;
        GameObject TargetArm;
        PlayerFist HurtFist;
        ParticleSystem Particles;
        Vector3 OraFactor = new Vector3(0,Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
        if (IsRightArm == true)
        {
            TargetArm = RArm;
            HurtFist = TargetArm.GetComponent<PlayerFist>();
            Particles = TargetArm.GetComponent<ParticleSystem>();
            IsRightArm = false;
        }
        else
        {
            TargetArm = LArm;
            HurtFist = TargetArm.GetComponent<PlayerFist>();
            Particles = TargetArm.GetComponent<ParticleSystem>();
            IsRightArm = true;
        }
        PowerSlider.gameObject.SetActive(true);
        while (Looping == true)
        {
            if (Input.GetMouseButton(0) == true)
            {
                if (Power < 20f)
                {
                    Power += 0.1f;
                    PowerSlider.value = Power;
                    PowerDye.color = Color.Lerp(new Color(0, 1, 0), new Color(0, 0.5f, 1), Power / 20f);
                    var emission = Particles.emission;
                    emission.rateOverTime = Power * 5;
                    TargetArm.transform.localPosition = Vector3.Lerp(new Vector3(0.15f, 0.1f, 0), new Vector3(0.15f - (1.35f * (Power / 20)), 0.1f, 0), 0.75f);
                }
            }
            else
            {
                Looping = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
        if (Power < 1)
        {
            Power = 1;
        }
        PowerSlider.gameObject.SetActive(false);
        PowerDye.color = new Color(0, 1, 0);
        PowerSlider.value = 0f;
        Reeling = false;
        TargetArm.GetComponent<PlayerFist>().Damage = Power;
        HurtFist.Striking = true;
        PunchAmount += 1f;
        for (float I = 1f; I >= 0; I -= 0.20f)
        {
            TargetArm.transform.localPosition = Vector3.Lerp(TargetArm.transform.localPosition, new Vector3(1f + (1f * (Power / 20)), 0.1f, 0) + OraFactor, 0.75f);
            yield return new WaitForSeconds(0.01f);
        }
        var emissions = Particles.emission;
        emissions.rateOverTime = 0;
        yield return new WaitForSeconds(0.002f * Power);
        HurtFist.Striking = false;
        for (float I = 1f; I >= 0; I -= 0.2f)
        {
            TargetArm.transform.localPosition = Vector3.Lerp(TargetArm.transform.localPosition, new Vector3(0.15f, 0.1f, 0), 0.75f);
            yield return new WaitForSeconds(0.01f);
        }
        
        yield return null;
    }
	void Update () 
	{
        if (Time.timeScale == 0)
        {
            return;
        }
        HealthSlider.value = health;
            HealthDye.color = Color.Lerp(new Color(1, 0, 0), new Color(0, 1, 0), health / maxHealth);
        CharacterVelocity = movementVector;
        if (isDead == false)
        {
            if (TimerA < 0.5f)
        {
            TimerA += Time.deltaTime;
        }
        else
        {
            TimerA = 0f;
            AverageRate = PunchAmount / 0.5f;
            if (AverageRate > 12)
            {
                AverageRate = 12f;
            }
            PunchAmount = 0f;
        }
        if (AverageRate > 0)
        {
            SpeedSlider.gameObject.SetActive(true);
            SpeedSlider.value = Mathf.Lerp(SpeedSlider.value, AverageRate, 20 * Time.deltaTime);
            SpeedDye.color = Color.Lerp(new Color(0, 1, 0), new Color(1, 0, 0), SpeedSlider.value / 12);
        }
        else
        {
            SpeedSlider.value = Mathf.Lerp(SpeedSlider.value, AverageRate, 20 * Time.deltaTime);
            SpeedDye.color = Color.Lerp(new Color(0, 1, 0), new Color(1, 0, 0), SpeedSlider.value / 12);
            if (SpeedSlider.value <= 0.0001f)
            {
                SpeedSlider.gameObject.SetActive(false);
            }
        }
            if (maxHealth > health)
            {
                health += 0.5f * Time.deltaTime;
            }
            else if (health > maxHealth)
            {
                health = maxHealth;
            }
            if (Input.GetMouseButtonDown(0) && Reeling == false)
            {
                Reeling = true;
                StartCoroutine("Charge");
            }

            if (FacingForward == false)
            {
                Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 180, 0), 25f * Time.deltaTime);
            }
            else
            {
                Orientation.transform.rotation = Quaternion.Slerp(Orientation.transform.rotation, Quaternion.Euler(0, 0, 0), 25f * Time.deltaTime);
            }
            Vector3 mouse = Input.mousePosition;
            //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 15)));
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 15));
            mouseWorld.z = 0;
            Vector3 headforward = mouseWorld - Head.transform.position;
            if (headforward.x < -0.25)
            {
                FacingForward = false;
            }
            else if (headforward.x > 0.25)
            {
                FacingForward = true;
            }
            Vector3 RightArmforward = mouseWorld - RightArm.transform.position;
            Vector3 LeftArmforward = mouseWorld - LeftArm.transform.position;
            if (Vector3.Distance(Head.transform.position, mouseWorld) > 0.5)
            { 
                Head.transform.rotation = Quaternion.Slerp(Head.transform.rotation, Quaternion.LookRotation(headforward) * Quaternion.Euler(0, -90, 0), 25f * Time.deltaTime);
            RightArm.transform.rotation = Quaternion.Slerp(Head.transform.rotation, Quaternion.LookRotation(RightArmforward) * Quaternion.Euler(0, -90, 0), 25f * Time.deltaTime);
            LeftArm.transform.rotation = Quaternion.Slerp(Head.transform.rotation, Quaternion.LookRotation(LeftArmforward) * Quaternion.Euler(0, -90, 0), 25f * Time.deltaTime);
            }

            if (CharacterVelocity.x < speed && Input.GetAxis("Horizontal") > 0f)
            {
                animator.SetBool("Moving", true);
                desiredmovement = Input.GetAxis("Horizontal");
                desiredmovement *= speed;
                movementVector = Vector3.Lerp(CharacterVelocity, new Vector3(desiredmovement, CharacterVelocity.y, CharacterVelocity.z), 25f * Time.deltaTime);
            }
            else if (CharacterVelocity.x > speed * -1 && Input.GetAxis("Horizontal") < 0f)
            {
                animator.SetBool("Moving", true);
                desiredmovement = Input.GetAxis("Horizontal");
                desiredmovement *= speed;
                movementVector = Vector3.Lerp(CharacterVelocity, new Vector3(desiredmovement, CharacterVelocity.y, CharacterVelocity.z), 25f * Time.deltaTime);
            }
            else if (FistJumping == false)
            {
                animator.SetBool("Moving", false);
                movementVector = Vector3.Lerp(CharacterVelocity, new Vector3(0f, CharacterVelocity.y, CharacterVelocity.z), 25f * Time.deltaTime);
            }
            if (FistJumperTimer > 0)
            {
                FistJumperTimer -= Time.deltaTime;
            }
            if (characterController.isGrounded && FistJumperTimer <=0)
            {
                FistJumping = false;
                //if the player pushes jump
                if (Input.GetButton("Jump"))
                {
                    movementVector.y = jumpSpeed;
                }
                animator.SetBool("Grounded", true);
                if (CanCamTamp == true)
                {
                    PlayerCamera.GetComponent<TrackPlayer>().Displacement.z = Mathf.Lerp(PlayerCamera.GetComponent<TrackPlayer>().Displacement.z, -15, 0.15f);
                }
             }
            else
            {
                animator.SetBool("Grounded", false);
                movementVector.y -= gravity * Time.deltaTime;
                if (CanCamTamp == true)
                {
                    PlayerCamera.GetComponent<TrackPlayer>().Displacement.z = Mathf.Lerp(PlayerCamera.GetComponent<TrackPlayer>().Displacement.z, -15, 0.005f);
                }
            }
            if ((characterController.collisionFlags & CollisionFlags.Above) != 0 && movementVector.y > 0f)
            {
                movementVector.y = 0f;
            }
            Knockback.z = 0;
            movementVector += Knockback;
            Knockback = new Vector3(0, 0, 0);
            //check if player health is low and set isDead value
            if (health <= 0)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
            }
            characterController.Move(movementVector * Time.deltaTime);
           if(gameObject.transform.position.z != 0f)
            {
                characterController.Move(new Vector3(0, 0, gameObject.transform.position.z * -1));
            }
            
        }
        else
        {
            SpeedSlider.gameObject.SetActive(false);
            PowerSlider.gameObject.SetActive(false);
        }
	}


}
