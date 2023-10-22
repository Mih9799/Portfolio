using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public UIScript master_ui;
    public GameObject default_bullet;
    public GameObject cluster_bullet;
    public GameObject homing_rocket;
    public GameObject supersonic_wave;
    public AudioClip default_fire;
    public AudioClip rocket_fire;
    public AudioClip scatter_fire;
    public AudioClip supersonic_fire;
    public AudioClip hurt;
    public AudioClip die;
    public AudioClip respawned;
    public GameObject particle;
    public Sprite[] gore;
    public Sprite[] fire;
    public Sprite[] engine;
    private int hp;
    private int lives;
    private int fire_delay;
    private bool dead;
    private int cluster = 0;
    private int homing = 0;
    private int scatter = 0;
    private int supersonic = 0;
    public bool invincible = false;
    // Start is called before the first frame update
    public enum Ammo
    {
        ASSAULT,    //Default weapon and fallback, has infinite ammo.
        CLUSTER,    //Splitting bullets that leave smaller and weaker bullets behind.
        HOMING,     //Powerful bullets that always home into an enemy, almost never misses.
        SCATTER,    //Buckshot that fires in a huge spread dealing unfathomable damage up close.
        SUPERSONIC  //Has infinite pierce but not too much damage.
    };
    Ammo selectedammo;
    void Start()
    {
        hp = 1;
        lives = 3;
        fire_delay = 0;
        selectedammo = Ammo.ASSAULT;
        StartCoroutine(FireTick());
        StartCoroutine(StartEngine());
        gameObject.GetComponent<AudioSource>().clip = default_fire;
        master_ui.UpdateAmmo(1, cluster);
        master_ui.UpdateAmmo(2, homing);
        master_ui.UpdateAmmo(3, scatter);
        master_ui.UpdateAmmo(4, supersonic);
    }

    public void Resupply(int id)
    {
        switch (id)
        {
            case 1:
                cluster = 24;
                master_ui.UpdateAmmo(1, cluster);
                break;
            case 2:
                homing = 12;
                master_ui.UpdateAmmo(2, homing);
                break;
            case 3:
                scatter = 8;
                master_ui.UpdateAmmo(3, scatter);
                break;
            case 4:
                supersonic = 3;
                master_ui.UpdateAmmo(4, supersonic);
                break;
            case 0:
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (invincible == false)
        {
            hp -= damage;
        }
    }

    IEnumerator FireTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (fire_delay > 0 && dead == false)
            {
                fire_delay -= 1;
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        lives--;
        master_ui.UpdateHealth(lives);
        dead = false;
        gameObject.GetComponent<AudioSource>().PlayOneShot(respawned, 1);
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        yield return new WaitForSeconds(3f);
        invincible = false;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);
        master_ui.GameOver();
    }

    void SwitchAmmo(bool down)
    {
        if (down == true)
        {
            switch (selectedammo)
            {
                case Ammo.ASSAULT:
                    selectedammo = Ammo.CLUSTER;
                    break;
                case Ammo.CLUSTER:
                    selectedammo = Ammo.HOMING;
                    break;
                case Ammo.HOMING:
                    selectedammo = Ammo.SCATTER;
                    break;
                case Ammo.SCATTER:
                    selectedammo = Ammo.SUPERSONIC;
                    break;
                case Ammo.SUPERSONIC:
                    selectedammo = Ammo.ASSAULT;
                    break;
            }
        }
        else
        {
            switch (selectedammo)
            {
                case Ammo.ASSAULT:
                    selectedammo = Ammo.SUPERSONIC;
                    break;
                case Ammo.CLUSTER:
                    selectedammo = Ammo.ASSAULT;
                    break;
                case Ammo.HOMING:
                    selectedammo = Ammo.CLUSTER;
                    break;
                case Ammo.SCATTER:
                    selectedammo = Ammo.HOMING;
                    break;
                case Ammo.SUPERSONIC:
                    selectedammo = Ammo.SCATTER;
                    break;
            }
        }
        switch (selectedammo)
        {
            case Ammo.ASSAULT:
                master_ui.SelectAmmo(0);
                break;
            case Ammo.CLUSTER:
                if (cluster <= 0)
                {
                    SwitchAmmo(down);
                }
                else
                {
                    master_ui.SelectAmmo(1);
                }
                break;
            case Ammo.HOMING:
                if (homing <= 0)
                {
                    SwitchAmmo(down);
                }
                else
                {
                    master_ui.SelectAmmo(2);
                }
                break;
            case Ammo.SCATTER:
                if (scatter <= 0)
                {
                    SwitchAmmo(down);
                }
                else
                {
                    master_ui.SelectAmmo(3);
                }
                break;
            case Ammo.SUPERSONIC:
                if (supersonic <= 0)
                {
                    SwitchAmmo(down);
                }
                else
                {
                    master_ui.SelectAmmo(4);
                }
                break;
        }
    }

    IEnumerator StartEngine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (dead == false)
            {
                GameObject fuel = Instantiate(particle, gameObject.transform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
                fuel.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-6, -4));
                fuel.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                fuel.GetComponent<Rigidbody2D>().gravityScale = 1f;
                fuel.GetComponent<SpriteRenderer>().sprite = engine[UnityEngine.Random.Range(0, engine.Length)];
                fuel.GetComponent<ParticleScript>().lifetime = 0.75f;
                fuel.GetComponent<ParticleScript>().scale = new Vector3(0.3f, 0.3f, 1);
                fuel.GetComponent<ParticleScript>().loaded = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hurt, 1);
            gameObject.GetComponent<AudioSource>().PlayOneShot(die, 1);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            invincible = true;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            dead = true;
            for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
            {
                GameObject splatter = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-3, 3), UnityEngine.Random.Range(-3, 3));
                splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                splatter.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                splatter.GetComponent<SpriteRenderer>().sprite = gore[UnityEngine.Random.Range(0, gore.Length)];
                splatter.GetComponent<ParticleScript>().lifetime = 1;
                splatter.GetComponent<ParticleScript>().scale = new Vector3(0.6f, 0.6f, 1);
                splatter.GetComponent<ParticleScript>().loaded = true;
            }
            GameObject data = GameObject.FindWithTag("Data");
            if (data != null && data.GetComponent<DataPersistence>() != null)
            {
                data.GetComponent<DataPersistence>().deaths++;
            }
            if (lives > 0)
            {
                hp = 1;
                StartCoroutine(Respawn());
            }
            else
            {
                hp = 1;
                StartCoroutine(GameOver());
            }
        }
        else
        {
            if (Input.GetKeyDown("e"))
            {
                SwitchAmmo(true);
            }
            if (Input.GetKeyDown("q"))
            {
                SwitchAmmo(false);
            }
            if ((gameObject.transform.position.x > -15 && Input.GetAxisRaw("Horizontal") == -1) || (gameObject.transform.position.x < 15 && Input.GetAxisRaw("Horizontal") == 1))
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 10, 0);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            }
            if (Input.GetAxisRaw("Fire1") == 1 && fire_delay <= 0)
            {
                switch (selectedammo)
                {
                    case Ammo.ASSAULT:
                        GameObject bullet = Instantiate(default_bullet, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
                        gameObject.GetComponent<AudioSource>().PlayOneShot(default_fire, 1);
                        for (int i = 0; i < UnityEngine.Random.Range(3, 6); i++)
                        {
                            GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                            splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(0, 3));
                            splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                            splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                            splatter.GetComponent<ParticleScript>().lifetime = 0.25f;
                            splatter.GetComponent<ParticleScript>().scale = new Vector3(0.25f, 0.25f, 1);
                            splatter.GetComponent<ParticleScript>().loaded = true;
                        }
                        fire_delay = 20;
                        break;
                    case Ammo.CLUSTER:
                        if (cluster <= 0)
                        {
                            selectedammo = Ammo.ASSAULT;
                            master_ui.SelectAmmo(0);
                            break;
                        }
                        GameObject bullet2 = Instantiate(cluster_bullet, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                        bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
                        gameObject.GetComponent<AudioSource>().PlayOneShot(default_fire, 1);
                        for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                        {
                            GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                            splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(0, 3));
                            splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                            splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                            splatter.GetComponent<ParticleScript>().lifetime = 0.25f;
                            splatter.GetComponent<ParticleScript>().scale = new Vector3(0.25f, 0.25f, 1);
                            splatter.GetComponent<ParticleScript>().loaded = true;
                        }
                        fire_delay = 40;
                        cluster--;
                        master_ui.UpdateAmmo(1, cluster);
                        break;
                    case Ammo.HOMING:
                        if (homing <= 0)
                        {
                            selectedammo = Ammo.ASSAULT;
                            master_ui.SelectAmmo(0);
                            break;
                        }
                        GameObject bullet3 = Instantiate(homing_rocket, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                        bullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
                        gameObject.GetComponent<AudioSource>().PlayOneShot(rocket_fire, 1);
                        for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                        {
                            GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                            splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(0, 3));
                            splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                            splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                            splatter.GetComponent<ParticleScript>().lifetime = 0.25f;
                            splatter.GetComponent<ParticleScript>().scale = new Vector3(0.25f, 0.25f, 1);
                            splatter.GetComponent<ParticleScript>().loaded = true;
                        }
                        fire_delay = 30;
                        homing--;
                        master_ui.UpdateAmmo(2, homing);
                        break;
                    case Ammo.SCATTER:
                        if (scatter <= 0)
                        {
                            selectedammo = Ammo.ASSAULT;
                            master_ui.SelectAmmo(0);
                            break;
                        }
                        for (int i = 15; i < 180; i += 15)
                        {
                            GameObject bullet4 = Instantiate(default_bullet, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                            bullet4.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 15, Mathf.Sin(i * Mathf.Deg2Rad) * 15);
                            bullet4.transform.rotation = Quaternion.Euler(0, 0, i - 90);
                        }
                        for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                        {
                            GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                            splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(0, 3));
                            splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                            splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                            splatter.GetComponent<ParticleScript>().lifetime = 0.25f;
                            splatter.GetComponent<ParticleScript>().scale = new Vector3(0.25f, 0.25f, 1);
                            splatter.GetComponent<ParticleScript>().loaded = true;
                        }
                        gameObject.GetComponent<AudioSource>().PlayOneShot(scatter_fire, 1);
                        fire_delay = 100;
                        scatter--;
                        master_ui.UpdateAmmo(3, scatter);
                        break;
                    case Ammo.SUPERSONIC:
                        if (supersonic <= 0)
                        {
                            selectedammo = Ammo.ASSAULT;
                            master_ui.SelectAmmo(0);
                            break;
                        }
                        GameObject bullet5 = Instantiate(supersonic_wave, gameObject.transform.position + new Vector3(0, 0.6f, 0), Quaternion.identity);
                        bullet5.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
                        gameObject.GetComponent<AudioSource>().PlayOneShot(supersonic_fire, 1);
                        fire_delay = 50;
                        supersonic--;
                        master_ui.UpdateAmmo(4, supersonic);
                        break;
                }
            }
        }
    }
}
