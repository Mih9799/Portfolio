using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFist : MonoBehaviour
{
    public GameObject BangPoint;
    public GameObject Partycool;
    public bool Striking = false;
    public float Damage = 1;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger || Striking == false)
        {
            return;
        }
        EnemyHealth targetHealth = other.GetComponent<EnemyHealth>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(Damage);
            Striking = false;
            GameObject Effect = Instantiate(Partycool, BangPoint.transform.position, Quaternion.identity);
        }
        else if (other.tag == "Obstacle" && Damage >= 2)
        {
            Player.GetComponent<PlayerScript>().Recoil(Damage,gameObject);
            Striking = false;
            GameObject Effect = Instantiate(Partycool, BangPoint.transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
