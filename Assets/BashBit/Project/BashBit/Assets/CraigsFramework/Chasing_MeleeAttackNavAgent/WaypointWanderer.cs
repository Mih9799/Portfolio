using UnityEngine;
using System.Collections;

public class WaypointWanderer : MonoBehaviour {


    //Transform of the GameObject we wish to move to , chase or follow.
    public Transform HomeLocation;

    public Transform[] Waypoints;
    int CurrentWaypointIndex = 0;

    Vector3 destination;

    GameObject Player;
    //How far can the nav agent see to spot the player.
    public float NavSightDistance = 7.0f;
    //specify a maximum distance the agent can chase the player away from t heir destination waypoint.
    public float MaxChaseRange = 12.0f;

    //A reference to this actor's Nav Agent Component, so we can tell it what to do and where to go!
    UnityEngine.AI.NavMeshAgent agent;

    public Transform HeadTfm;

    void Start()
    {
        // Cache agent component and destination
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //set our home location to our start point when the game runs.
        HomeLocation = agent.transform;

        //Get the player's gameobject
        Player = GameObject.FindGameObjectWithTag("Player");

        //brute force the agent to move to the first waypoint
        destination = Waypoints[CurrentWaypointIndex].position;
        agent.SetDestination(destination);
    }

    bool isFiringWeapon = false;
    void Update()
    {
        Vector3 LookDir = HeadTfm.transform.position - Player.transform.position;
        Debug.DrawRay(HeadTfm.transform.position, (HeadTfm.transform.position - Player.transform.position).normalized * 150.0f, Color.red, 1.0f);

        //CheckLineOfSight();
        RaycastHit hit;
        if (Physics.Raycast(HeadTfm.position, (HeadTfm.transform.position - Player.transform.position).normalized, out hit, 300.0f))

        {
            Debug.Log(hit.collider.name);
        }

        //if the player is within x units of navagent, then move toward player
        if (Vector3.Distance(agent.transform.position, Player.transform.position) < NavSightDistance && Vector3.Distance(agent.transform.position, destination) < MaxChaseRange)
        {
            agent.SetDestination(Player.transform.position);
            print("Chasing Player");

            if (!isFiringWeapon)
            {
                EnemyStartFiringWeapon();
                isFiringWeapon = true;
            }
            
        }
        else
        {
            //fall back to previous waypoint if the player gets away from us.
            agent.SetDestination(destination);

            if (isFiringWeapon)
            {
                EnemyStopFiringWeapon();
                isFiringWeapon = false;
            }

        }
        //
        if (Vector3.Distance(agent.transform.position, destination) < 1.0f)
        {
            //call new waypoint here
            SetNewWaypointDest();
            print("Moving to Next WayPoint");
        }


    }

    void SetNewWaypointDest()
    {
        //ensure the agent loops back to the first waypoint when it reaches the last Index in the Array
        if (CurrentWaypointIndex + 1 < Waypoints.Length)
        {
            CurrentWaypointIndex++;
        }
        else
        {
            CurrentWaypointIndex = 0;
        }

        destination = Waypoints[CurrentWaypointIndex].position;
        agent.SetDestination(destination);
    }


    bool canSeePlayer = false;
    //Determine if we can trace to the player, if we can, we can see them.
    void CheckLineOfSight()
    {

        Vector3 LookDir = HeadTfm.transform.position - Player.transform.position;
        //Quaternion LookRotation(Vector3 forward, Vector3 upwards = Vector3.up)
        //create a direcitonal ray from our head forward.
       // Debug.DrawRay(HeadTfm.transform.position, LookDir, Color.red, 10.0f);
        RaycastHit hit;
        if (Physics.Raycast(HeadTfm.transform.position, LookDir, out hit))
        {
            print("Performed Trace");

            if (hit.collider.gameObject.tag == "Player")
            {
                canSeePlayer = true;
               
            }
            else
            {
                canSeePlayer = false;
                //Debug.DrawRay(HeadTfm.transform.position, Player.transform.position, Color.red, 5.0f);
            }

        }

        //Debug.DrawRay(MuzzlePoint.transform.position, MuzzlePoint.transform.forward * 10.0f, Color.red);

    }

    void EnemyStartFiringWeapon()
    {
        gameObject.GetComponentsInChildren<WeaponScript>()[0].ActivateWeapon();
    }

    void EnemyStopFiringWeapon()
    {
        gameObject.GetComponentsInChildren<WeaponScript>()[0].DeactivateWeapon();
    }
}
