using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public UIScript master_ui;
    public GameObject effect;
    public AudioClip hurt;
    public AudioClip die;
    public GameObject default_bullet;
    public GameObject item;
    public GameObject particle;
    public Sprite[] gore;
    public Sprite[] fire;
    public int ai_id;
    private int max_hp;
    private int hp;
    private int fire_delay;
    private int points;
    private int deathraydelay;
    private bool descending = false;
    private bool goingright = true;
    private bool dying = false;
    // Start is called before the first frame update
    void Start()
    {
        if (ai_id == 0) //Biter
        {
            max_hp = 10;
            hp = 10;
            points = 1;
        }
        if (ai_id == 1) //Spitter
        {
            max_hp = 10;
            hp = 10;
            points = 2;
            StartCoroutine(FireTick());
            fire_delay = UnityEngine.Random.Range(150, 450);
        }
        if (ai_id == 2) //Mothership
        {
            max_hp = 100;
            hp = 100;
            points = 50;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
            StartCoroutine(FireTick());
            fire_delay = UnityEngine.Random.Range(150, 450);
            deathraydelay = fire_delay;
            descending = true;
        }
        if (ai_id == 3) //Vextroyah
        {
            max_hp = 1000;
            hp = 1000;
            points = 1000;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3);
            StartCoroutine(FireTick());
            fire_delay = UnityEngine.Random.Range(150, 450);
            deathraydelay = fire_delay;
            descending = true;
        }
        if (ai_id == 4) //Turret
        {
            max_hp = 100;
            hp = 100;
            points = 50;
            StartCoroutine(FireTick());
            fire_delay = UnityEngine.Random.Range(150, 300);
        }
        if (ai_id == 5) //Cruncher
        {
            max_hp = 30;
            hp = 30;
            points = 10;
        }
        if (ai_id == 6) //Sprayer
        {
            max_hp = 10;
            hp = 10;
            points = 20;
            StartCoroutine(FireTick());
            fire_delay = UnityEngine.Random.Range(150, 450);
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp > 0)
        {
            GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            SoundAffect.GetComponent<SoundEffect>().sound = hurt;
        }
    }

    int EdgeCollision()
    {
        if (ai_id == 2)
        {
            if (gameObject.transform.position.x >= 14 && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                return 1; // It is colliding with the right! Go Left!
            }
            if (gameObject.transform.position.x <= -14 && gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                return 2; // It is colliding with the left! Go Right!
            }
        }
        if (ai_id == 3)
        {
            if (gameObject.transform.position.x >= 8.5 && gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
            {
                return 1; // It is colliding with the right! Go Left!
            }
            if (gameObject.transform.position.x <= -8.5 && gameObject.GetComponent<Rigidbody2D>().velocity.x < 0)
            {
                return 2; // It is colliding with the left! Go Right!
            }
        }
        return 0; // Theres no collisions detected. Keep going!
    }

    IEnumerator FireTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            if (fire_delay > 0)
            {
                fire_delay -= 1;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<PlayerScript>() != null)
            {
                col.gameObject.GetComponent<PlayerScript>().TakeDamage(1);
            }
        }
    }

    IEnumerator GloriousDeath()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).GetComponent<EnemyAI>().hp = 0;
            }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        for (int i = 0; i < 10; i ++)
        {
            for (int o = 0; o < UnityEngine.Random.Range(5, 10); o++)
            {
                Vector3 offset = new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(0, 3), 0);
                GameObject splatter = Instantiate(particle, gameObject.transform.position + offset, Quaternion.identity);
                splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-6, 6), UnityEngine.Random.Range(-6, 6));
                splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                splatter.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                splatter.GetComponent<SpriteRenderer>().sprite = gore[UnityEngine.Random.Range(0, gore.Length)];
                splatter.GetComponent<ParticleScript>().lifetime = 2;
                splatter.GetComponent<ParticleScript>().scale = new Vector3(1.6f, 1.6f, 1);
                splatter.GetComponent<ParticleScript>().loaded = true;
            }
            GameObject SoundAffect3 = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
            SoundAffect3.GetComponent<SoundEffect>().sound = hurt;
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
        {
            GameObject splatter = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
            splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-8, 8));
            splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
            splatter.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
            splatter.GetComponent<SpriteRenderer>().sprite = gore[UnityEngine.Random.Range(0, gore.Length)];
            splatter.GetComponent<ParticleScript>().lifetime = 3;
            splatter.GetComponent<ParticleScript>().scale = new Vector3(3.2f, 3.2f, 1);
            splatter.GetComponent<ParticleScript>().loaded = true;
        }
        GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        SoundAffect.GetComponent<SoundEffect>().sound = hurt;
        GameObject SoundAffect2 = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        SoundAffect2.GetComponent<SoundEffect>().sound = die;
        GameObject data = GameObject.FindWithTag("Data");
        if (data != null && data.GetComponent<DataPersistence>() != null)
        {
            data.GetComponent<DataPersistence>().bosskills++;
        }
        master_ui.UpdateScore(points);
        Destroy(gameObject);
    }

    IEnumerator DeathLaser()
    {
        fire_delay = deathraydelay + 150;
        if (goingright == true)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
        }
        float nozzle = -0.6f;
        if (ai_id == 3)
        {
            nozzle = -4.5f;
        }
        while (fire_delay > 100 + deathraydelay)
        {
            yield return new WaitForSeconds(0.02f);
            for (int i = 0; i < UnityEngine.Random.Range(2, 4); i++)
            {
                GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, nozzle, 0), Quaternion.identity);
                splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 0));
                splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                splatter.GetComponent<Rigidbody2D>().gravityScale = 1f;
                splatter.GetComponent<ParticleScript>().lifetime = 0.4f;
                splatter.GetComponent<ParticleScript>().scale = new Vector3(0.5f, 0.5f, 1);
                splatter.GetComponent<ParticleScript>().loaded = true;
            }
            if (dying == true) { break; }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        while (fire_delay > deathraydelay)
        {
            yield return new WaitForSeconds(0.001f);
            for (int i = 0; i < UnityEngine.Random.Range(1, 4); i++)
            {
                GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, nozzle, 0), Quaternion.identity);
                splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-8, 0));
                splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                splatter.GetComponent<Rigidbody2D>().gravityScale = 1f;
                splatter.GetComponent<ParticleScript>().lifetime = 0.5f;
                splatter.GetComponent<ParticleScript>().scale = new Vector3(1f, 1f, 1);
                splatter.GetComponent<ParticleScript>().loaded = true;
            }
            GameObject bullet = Instantiate(default_bullet, gameObject.transform.position + new Vector3(0, nozzle, 0), Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -20);
            if (dying == true) { break; }
        }
        if (dying == false)
        {
            if (goingright == true)
            {
                if (ai_id == 2)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
                }
            }
            else
            {
                if (ai_id == 2)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ai_id == 2 || ai_id == 3)
        {
            if (hp <= 0)
            {
                if (ai_id == 3)
                {
                    if (dying == false)
                    {
                        dying = true;
                        StartCoroutine(GloriousDeath());
                    }
                }
                if (ai_id == 2)
                {
                    GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                    SoundAffect.GetComponent<SoundEffect>().sound = die;
                    GameObject data = GameObject.FindWithTag("Data");
                    if (data != null && data.GetComponent<DataPersistence>() != null)
                    {
                        data.GetComponent<DataPersistence>().kills++;
                    }
                    master_ui.UpdateScore(points);
                    GameObject drop = Instantiate(item, gameObject.transform.position, Quaternion.identity);
                    drop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
                    int chosen = UnityEngine.Random.Range(1, 5);
                    drop.GetComponent<ItemScript>().item_id = chosen;
                    switch (chosen)
                    {
                        case 1:
                            drop.GetComponent<SpriteRenderer>().color = Color.yellow;
                            break;
                        case 2:
                            drop.GetComponent<SpriteRenderer>().color = Color.green;
                            break;
                        case 3:
                            drop.GetComponent<SpriteRenderer>().color = Color.blue;
                            break;
                        case 4:
                            drop.GetComponent<SpriteRenderer>().color = Color.cyan;
                            break;
                    }
                    for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
                    {
                        GameObject splatter = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                        splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-6, 6), UnityEngine.Random.Range(-6, 6));
                        splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                        splatter.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                        splatter.GetComponent<SpriteRenderer>().sprite = gore[UnityEngine.Random.Range(0, gore.Length)];
                        splatter.GetComponent<ParticleScript>().lifetime = 2;
                        splatter.GetComponent<ParticleScript>().scale = new Vector3(1.6f, 1.6f, 1);
                        splatter.GetComponent<ParticleScript>().loaded = true;
                    }
                    Destroy(gameObject);
                }
            }
            else
            {
                if (descending == false)
                {
                    int Status = EdgeCollision();
                    switch (Status)
                    {
                        case 1:
                            if (fire_delay > 100 + deathraydelay)
                            {
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
                            }
                            else
                            {
                                if (ai_id == 2)
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                                }
                            }
                            goingright = false;
                            break;
                        case 2:
                            if (fire_delay > 100 + deathraydelay)
                            {
                                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
                            }
                            else
                            {
                                if (ai_id == 2)
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
                                }
                            }
                            goingright = true;
                            break;
                        default:
                            break;
                    }
                    if (fire_delay <= 0)
                    {
                        deathraydelay = UnityEngine.Random.Range(150, 300);
                        StartCoroutine(DeathLaser());
                    }
                }
                else
                {
                    if ((ai_id == 2 && transform.position.y <= 8.5) || (ai_id == 3 && transform.position.y <= 5))
                    {
                        switch (goingright)
                        {
                            case false:
                                if (ai_id == 2)
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-5, 0);
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                                }
                                break;
                            case true:
                                if (ai_id == 2)
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
                                }
                                else
                                {
                                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
                                }
                                break;
                        }
                        descending = false;
                    }
                }
            }
        }
        else
        {
            if (ai_id == 4)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {

                    transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position, Vector3.down) * Quaternion.Euler(0, 90, -90);
                }
            }
            if (hp <= 0)
            {
                GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                SoundAffect.GetComponent<SoundEffect>().sound = die;
                GameObject data = GameObject.FindWithTag("Data");
                if (data != null && data.GetComponent<DataPersistence>() != null)
                {
                    data.GetComponent<DataPersistence>().kills++;
                }
                master_ui.UpdateScore(points);
                if (UnityEngine.Random.Range(1, 10) == 1)
                {
                    GameObject drop = Instantiate(item, gameObject.transform.position, Quaternion.identity);
                    drop.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
                    int chosen = UnityEngine.Random.Range(1, 5);
                    drop.GetComponent<ItemScript>().item_id = chosen;
                    switch (chosen)
                    {
                        case 1:
                            drop.GetComponent<SpriteRenderer>().color = Color.yellow;
                            break;
                        case 2:
                            drop.GetComponent<SpriteRenderer>().color = Color.green;
                            break;
                        case 3:
                            drop.GetComponent<SpriteRenderer>().color = Color.blue;
                            break;
                        case 4:
                            drop.GetComponent<SpriteRenderer>().color = Color.cyan;
                            break;
                    }
                }
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
                Destroy(gameObject);
            }
            if (gameObject.transform.position.y < -15)
            {
                hp = 0;
            }
            if (ai_id == 1 && fire_delay <= 0)
            {
                for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                {
                    GameObject splatter = Instantiate(particle, gameObject.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                    splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 0));
                    splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                    splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                    splatter.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    splatter.GetComponent<ParticleScript>().lifetime = 0.3f;
                    splatter.GetComponent<ParticleScript>().scale = new Vector3(0.3f, 0.3f, 1);
                    splatter.GetComponent<ParticleScript>().loaded = true;
                }
                GameObject bullet = Instantiate(default_bullet, gameObject.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5);
                fire_delay = UnityEngine.Random.Range(150, 450);
            }
            if (ai_id == 4 && fire_delay <= 0)
            {
                for (int i = 0; i < UnityEngine.Random.Range(3, 5); i++)
                {
                    GameObject splatter = Instantiate(particle, gameObject.transform.position - transform.up * 2, Quaternion.identity);
                    splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 0));
                    splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                    splatter.GetComponent<SpriteRenderer>().sprite = fire[UnityEngine.Random.Range(0, fire.Length)];
                    splatter.GetComponent<Rigidbody2D>().gravityScale = 1f;
                    splatter.GetComponent<ParticleScript>().lifetime = 0.3f;
                    splatter.GetComponent<ParticleScript>().scale = new Vector3(0.5f, 0.5f, 1);
                    splatter.GetComponent<ParticleScript>().loaded = true;
                }
                GameObject bullet = Instantiate(default_bullet, gameObject.transform.position - transform.up * 2, transform.rotation * Quaternion.Euler(0,0,180));
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.up.x, transform.up.y) * -10;
                fire_delay = UnityEngine.Random.Range(150, 300);
            }
            if (ai_id == 6 && fire_delay <= 0)
            {
                for (int i = 45; i <= 135; i += 45)
                {
                    GameObject bullet4 = Instantiate(default_bullet, gameObject.transform.position + new Vector3(0, -0.5f, 0), Quaternion.identity);
                    bullet4.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * -5, Mathf.Sin(i * Mathf.Deg2Rad) * -5);
                    bullet4.transform.rotation = Quaternion.Euler(0, 0, i - 90);
                }
                fire_delay = UnityEngine.Random.Range(150, 450);
            }
        }
    }
}