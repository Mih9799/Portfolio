using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public GameObject effect;
    public AudioClip collide;
    public int item_id;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (col.gameObject.GetComponent<PlayerScript>() != null)
            {
                col.gameObject.GetComponent<PlayerScript>().Resupply(item_id);
                GameObject data = GameObject.FindWithTag("Data");
                if (data != null && data.GetComponent<DataPersistence>() != null)
                {
                    data.GetComponent<DataPersistence>().powerups++;
                }
                GameObject SoundAffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
                SoundAffect.GetComponent<SoundEffect>().sound = collide;
                Destroy(gameObject);
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
    }
}
