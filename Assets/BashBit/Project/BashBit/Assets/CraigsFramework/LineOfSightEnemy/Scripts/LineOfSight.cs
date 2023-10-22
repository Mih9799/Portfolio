using UnityEngine;
using System.Collections;

public class LineOfSight : MonoBehaviour
{

    public GameObject Player;
    public float heightMultiplier = 0.2f;
    public bool seePlayer = false;
  

    public float defaultSightDecay = 1.0f;
    float sightDecayTime = 1.0f;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Draw a line for the forward direction of the agent
        //Debug.DrawRay(transform.position, (transform.forward * 150.0f));

        //Draw a Ray between the nav agent and the player
        Debug.DrawRay(transform.position, ((Player.transform.position + Vector3.up * heightMultiplier - transform.position).normalized * 150.0f));

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player.transform.position + Vector3.up * heightMultiplier - transform.position).normalized * 150.0f, out hit, 150.0f))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Hit a thing: " + hit.collider.name);
                seePlayer = true;
                sightDecayTime = defaultSightDecay;
            }
            else
            {
                //hold off on losing sight until timer expired
                sightDecayTime -= Time.deltaTime;
                Debug.Log("Losing sight:" + sightDecayTime);

                if (sightDecayTime <= 0)
                {
                    sightDecayTime = 0;                    
                    seePlayer = false;
                }                
            }
        }
    }
}
