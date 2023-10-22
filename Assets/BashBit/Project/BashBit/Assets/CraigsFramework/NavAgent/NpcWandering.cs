using UnityEngine;
using System.Collections;

public class NpcWandering : MonoBehaviour {

    public Transform target;
    public Vector3 destination;
    UnityEngine.AI.NavMeshAgent agent;

    public float EnemySightDistance = 5.0f;

    void Start()
    {
        // Cache agent component and select new random destination
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();        
        FindNewRandomDestination();
    }

    void Update()
    {
        // Update destination if the target is beyond 1 unit from the agent
        if (Vector3.Distance(destination, agent.transform.position) > 1.0f)
        {
                //destination = target.position;
                agent.destination = destination;
        }
        else
        {
            FindNewRandomDestination();
        }

        //test whether the current Player is within X units (EnemySightDistance) of the agent
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, agent.transform.position) <  EnemySightDistance)
        {
            //chase the player by setting our destination to the player location evey frame
            destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    void FindNewRandomDestination()
    {
        
        //Find a new random location within 20 units either side of the agent and move to it.
        destination = agent.transform.position + new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
        
        {

        }
    }
}