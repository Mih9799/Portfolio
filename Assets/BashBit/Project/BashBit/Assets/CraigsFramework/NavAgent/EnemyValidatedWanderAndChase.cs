using UnityEngine;
using System.Collections;

public class EnemyValidatedWanderAndChase : MonoBehaviour {


    public Transform target;
    public Vector3 destination;
    UnityEngine.AI.NavMeshAgent agent;

    public float EnemySightDistance = 5.0f;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //destination = agent.destination;
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
        if (Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, agent.transform.position) < EnemySightDistance)
        {
            //chase the player by setting our destination to the player location evey frame
            destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    void FindNewRandomDestination()
    {
        RandomPoint(agent.transform.position, 10.0f, out destination);
    }

    //For more info and reference to this method see https://docs.unity3d.com/ScriptReference/NavMesh.SamplePosition.html
    public float range = 10.0f;
    public float moveDistanceRange = 10.0f;


    bool RandomPoint(Vector3 center, float range, out Vector3 result)

    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.DrawLine(hit.position, hit.position + new Vector3(0, 2, 0));
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
