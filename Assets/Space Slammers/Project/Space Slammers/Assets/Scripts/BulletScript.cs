using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject effect;
    public AudioClip collide;
    public bool is_evil;
    public int damage;
    public int projectile_id;
    public GameObject child_bullet;
    public GameObject particle;
    public Sprite[] engine;
    public Sprite[] explosion;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartEngine());
        StartCoroutine(Expand());
    }

    IEnumerator StartEngine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (projectile_id == 2)
            {
                GameObject fuel = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                Vector2 velo = (this.transform.rotation * Quaternion.Euler(0,0,UnityEngine.Random.Range(-30, 30))) * Vector2.up * 3;
                fuel.GetComponent<Rigidbody2D>().velocity = velo;
                fuel.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                fuel.GetComponent<SpriteRenderer>().sprite = engine[UnityEngine.Random.Range(0, engine.Length)];
                fuel.GetComponent<ParticleScript>().lifetime = 0.5f;
                fuel.GetComponent<ParticleScript>().scale = new Vector3(0.2f, 0.2f, 1);
                fuel.GetComponent<ParticleScript>().loaded = true;
            }
        }
    }

    IEnumerator Expand()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (projectile_id == 4)
            {
                gameObject.transform.localScale = Vector3.Scale(gameObject.transform.localScale, new Vector3(1.2f, 1, 1));
            }
        }
    }

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((is_evil == false && col.gameObject.tag == "Enemy")|| (is_evil == true && col.gameObject.tag == "Player"))
        {
            if (col.gameObject.GetComponent<PlayerScript>() != null)
            {
                if (col.gameObject.GetComponent<PlayerScript>().invincible == false)
                {
                    col.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
                    GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                    SoundAffect.GetComponent<SoundEffect>().sound = collide;
                    if (projectile_id != 3 && projectile_id != 4)
                    {
                        for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                    {
                        GameObject splatter = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                        splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
                        splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                        splatter.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
                        splatter.GetComponent<SpriteRenderer>().sprite = explosion[UnityEngine.Random.Range(0, explosion.Length)];
                        splatter.GetComponent<ParticleScript>().lifetime = 1;
                        splatter.GetComponent<ParticleScript>().scale = new Vector3(0.3f, 0.3f, 1);
                        splatter.GetComponent<ParticleScript>().loaded = true;
                    }
                        Destroy(gameObject);
                    }
                }
            }
            if (col.gameObject.GetComponent<EnemyAI>() != null)
            {
                col.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
                GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                SoundAffect.GetComponent<SoundEffect>().sound = collide;
                if (projectile_id == 1)
                {
                    for (int i = 30; i < 180; i += 30)
                    {
                        GameObject bullet = Instantiate(child_bullet, gameObject.transform.position, Quaternion.identity);
                        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(i * Mathf.Deg2Rad) * 15, Mathf.Sin(i * Mathf.Deg2Rad) * 15);
                        bullet.transform.rotation = Quaternion.Euler(0, 0, i - 90);
                    }
                }
                if (projectile_id != 3 && projectile_id != 4)
                {
                    for (int i = 0; i < UnityEngine.Random.Range(4, 7); i++)
                    {
                        GameObject splatter = Instantiate(particle, gameObject.transform.position, Quaternion.identity);
                        splatter.GetComponent<Rigidbody2D>().velocity = new Vector2(UnityEngine.Random.Range(-2, 2), UnityEngine.Random.Range(-2, 2));
                        splatter.GetComponent<Rigidbody2D>().angularVelocity = UnityEngine.Random.Range(-45, 45);
                        splatter.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
                        splatter.GetComponent<SpriteRenderer>().sprite = explosion[UnityEngine.Random.Range(0, explosion.Length)];
                        splatter.GetComponent<ParticleScript>().lifetime = 1;
                        splatter.GetComponent<ParticleScript>().scale = new Vector3(0.3f, 0.3f, 1);
                        splatter.GetComponent<ParticleScript>().loaded = true;
                    }
                    Destroy(gameObject);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < -20 || gameObject.transform.position.x > 20 || gameObject.transform.position.y < -15 || gameObject.transform.position.y > 15)
        {
            Destroy(gameObject);
        }
        if (projectile_id == 2)
        {
            Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            GameObject target = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Enemy"));
            if (target != null)
            {
                var dif = target.transform.position - gameObject.transform.position;
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(velocity, new Vector2(dif.x, dif.y).normalized * 10, Time.deltaTime * 1.5f);
                transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg - 90, Vector3.forward);
            }
        }
    }
}
