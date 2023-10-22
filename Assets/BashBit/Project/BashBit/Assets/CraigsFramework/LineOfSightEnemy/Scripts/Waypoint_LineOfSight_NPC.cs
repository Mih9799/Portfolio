using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineOfSight))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class Waypoint_LineOfSight_NPC : MonoBehaviour {

    
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
    LineOfSight Sight;
    public Transform HeadTfm;

    void Start()
    {
        Sight = GetComponent<LineOfSight>();
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
        //Line of sight handled outside of this component, see LineOfSight for implementation

        //if the player is within x units of navagent, then move toward player
        if (Vector3.Distance(agent.transform.position, Player.transform.position) < NavSightDistance && Vector3.Distance(agent.transform.position, destination) < MaxChaseRange && Sight.seePlayer)
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

    void EnemyStartFiringWeapon()
    {
        gameObject.GetComponentsInChildren<WeaponScript>()[0].ActivateWeapon();
    }

    void EnemyStopFiringWeapon()
    {
        gameObject.GetComponentsInChildren<WeaponScript>()[0].DeactivateWeapon();
    }
}
