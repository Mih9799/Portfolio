using UnityEngine;
using System.Collections;

public class NPC_ChaseTarget : MonoBehaviour {

    //Transform of the GameObject we wish to move to , chase or follow.
    public Transform HomeLocation;
    public float SearchDistance = 5.0f;
    GameObject Player;

    //How far can the nav agent see to spot the player.
    public float NavSightDistance = 7.0f;
    //Vector to store the destination (can be updated in the Update() method also.)
    Vector3 destination;

    //A reference to this actor's Nav Agent Component, so we can tell it what to do and where to go!
    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
        SetNewRandomDestination(true);
    }

    void Update()
    {
        if (Vector3.Distance(agent.transform.position, destination) > 1.0f)
        {
            agent.SetDestination(destination);
            print("Moving to Rand Point");
        }
        else
        {
            SetNewRandomDestination(true);
            print("Reached Point and Getting New Destination");
        }


        //if the player is within x units of navagent, then move toward player
        if (Vector3.Distance(agent.transform.position, Player.transform.position) < NavSightDistance)
        {
            agent.SetDestination(Player.transform.position);
            print("Chasing Player");
        }

    }

    public void SetNewRandomDestination(bool AroundHomeLocation)
    {
        if (AroundHomeLocation)
        {
            //Get new random point around HomeLocation
            destination = HomeLocation.position + new Vector3(Random.Range(-SearchDistance, SearchDistance), 0, Random.Range(-SearchDistance, SearchDistance));

        }
        else
        {
            //get a random location and set new destination.
            destination = agent.transform.position + new Vector3(Random.Range(-5.0f, 5.0f), 0, Random.Range(-5.0f, 5.0f));
        }
    }
}
