using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AiStates {Idle, Wander, WaypointPatrol, Investigating, Alert, Chasing, RangedAttack, MeleeAttack, Retreating }

public class PrimitiveAIAgentController : MonoBehaviour
{
    [Header("Runtime Info")]
    
    [SerializeField] Vector3 destination;
    UnityEngine.AI.NavMeshAgent agent;
    public float DestinationAcceptRadius = 2.0f;
    //public float MeleeAttackRange = 1.0f;
    [SerializeField] AiStates currentState = AiStates.Wander;
    [SerializeField] AiStates previousState = AiStates.Idle;
    private float stateAge;
    [SerializeField] bool isAlerted = false;
    [Header("Agent Sensing Parameters")]
    [SerializeField] bool seeHostileTarget = false;
    public float AgentSightDistance = 15.0f;
    //public float AgentHearingDistance = 5.0f;
    [Header("Agent Vision Parameters")]
    //Vision related Parameters
    [SerializeField] float eyeHeight = 1.0f;
    float sightNoticeTime = 0.8f;
    float defaultsightNoticeTime = 1.2f;
    float loseSightSpeed = 0.5f;
    float defaultSightDecay = 1.6f;

    [SerializeField] float currentSight = 0.0f;
    //[SerializeField] LayerMask visibilityLayerMask;

    public Vector2 waitTimeMinMax = new Vector2(1.0f, 3.14f);

    [Header("Waypoints")]
    Vector3 HomeLocation;
    [SerializeField] int CurrentWaypointIndex = 0;
    public float MaxChaseRange = 12.0f;
    float distanceToDest = 9999.0f;

    [Header("Hostile Target Info")]
    //hostile target tracking parameters
    [SerializeField] GameObject HostileTarget;
    [SerializeField] float distanceToTarget = 9999.0f;
    Vector3 lastSeenPosition;

    [Header("Waypoint Movement Variables")]
    public Transform[] Waypoints;

    [Header("Melee Attack Variables")]
    //Melee Attack Vars
    float meleeAttackDuration = 1.2f;
    public float meleeAttackDistance = 2.5f;
    //MeleeWeaponScript meleeWeapon;
    bool meleeEnabled = false;
    [SerializeField] GameObject meleeDamageFx;
    [SerializeField] float meleeDamage = 8.0f;

    AudioSource AgentAudio;
    NavMeshPath myPath;
    // Start is called before the first frame update
    void Start()
    {
        AgentAudio = GetComponent<AudioSource>();
        if (!AgentAudio)
        {
            AgentAudio = gameObject.AddComponent<AudioSource>();
            
        }
        AgentAudio.spatialBlend = 1;
        myPath = new NavMeshPath();
        initAI();
        if (!HostileTarget)
        {
            HostileTarget = GameObject.FindGameObjectWithTag("Player");
        }       
        StartCoroutine(SwapAiState(currentState, true));

        
    }

    void initAI()
    {
        //Store Agent's initial location as 'Home'
        HomeLocation = transform.position;      

        // Cache agent component and destination
        agent = GetComponent<NavMeshAgent>();
        if (!agent)
        {
            print("agent is missing, please add a Nav Mesh Agent component to this GameObject");
            this.enabled = false;
        }
        stateAge = 0;
    }


    IEnumerator SwapAiState(AiStates newstate, bool forceState = false)
    {
        if (newstate != currentState || forceState)
        {

            stateAge = 0;

            previousState = currentState;
            switch (newstate)
            {
                case AiStates.Idle:
                    isAlerted = false;
                    yield return new WaitForSeconds(1.6f);
                    print("Idle loopin");
                    //AgentAudio.PlayOneShot(awakeSound, 1);
                    
                    break;

                case AiStates.Wander:                    
                    FindNewRandomDestination();
                    AgentAudio.PlayOneShot(wanderSound, 1);
                    //Debug.Log("Wandering");
                    break;

                case AiStates.Chasing:
                    chaseTarget();
                    //print("Pursuing Target");
                    //AgentAudio.PlayOneShot(chasingSound, 1);
                    break;

                case AiStates.Alert:
                    isAlerted = true;
                    //AgentAudio.PlayOneShot(AlertSound, 1);
                    break;


                case AiStates.Retreating:
                    FindRetreatPosition();
                    //AgentAudio.PlayOneShot(retreatingSound);
                    break;
                case AiStates.MeleeAttack:
                    StartCoroutine(doMeleeAttack());
                    //AgentAudio.PlayOneShot(meleeSound);
                    break;
                case AiStates.WaypointPatrol:
                    nextWaypoint();
                    break;

                case AiStates.RangedAttack:
                    //AgentAudio.PlayOneShot(rangedAttackSound);
                    break;

            }
            currentState = newstate;
        }
    }

    IEnumerator waitAtLocation(float duration)
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(duration);
        agent.isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        //update Hostile Target info
        if (!HostileTarget)
        {
            StopCoroutine(doMeleeAttack());
            StartCoroutine(SwapAiState(AiStates.Wander));
        }

        stateAge += Time.deltaTime;

        switch (currentState)
        {
            case AiStates.Idle:                
                print("Idle loopin");
                StopAllCoroutines();
                if (isAlerted)
                {
                    sphereLineOfSight();
                }
                break;

            case AiStates.Wander:
                sphereLineOfSight();
                
                if (hasReachedDestination())
                {
                    FindNewRandomDestination();
                    if (wayPointSound)
                    {
                        AgentAudio.PlayOneShot(wayPointSound);
                    }                    
                    StartCoroutine(waitAtLocation(Random.Range(waitTimeMinMax.x, waitTimeMinMax.y)));
                }
                break;
                
            case AiStates.Chasing:
                agent.updateRotation = true;
                sphereLineOfSight();
                chaseTarget();
                //Debug.Log("Pursuing Target");                               
                break;

            case AiStates.Retreating:
                FindRetreatPosition();
                break;

            case AiStates.Alert:
                sphereLineOfSight();
                StartCoroutine(SwapAiState(AiStates.Chasing));
                break;

            case AiStates.MeleeAttack:
                agent.updateRotation = false;
                Vector3 hostileLoc = new Vector3(HostileTarget.transform.position.x, agent.GetComponent<Collider>().bounds.center.y, HostileTarget.transform.position.z);

                //This line forces the actor to rotate and face the player on the xz plane
                //agent.transform.rotation = Quaternion.LookRotation(hostileLoc - agent.GetComponent<Collider>().bounds.center, Vector3.up);
                agent.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(hostileLoc - agent.GetComponent<Collider>().bounds.center, Vector3.up), Time.deltaTime * agent.angularSpeed);
                break;

            case AiStates.WaypointPatrol:
                sphereLineOfSight();
                if (hasReachedDestination())
                {
                    nextWaypoint();
                }
                break;
        }
    }


    void FindNewRandomDestination()
    {
        //Debug.Log("New Random Dest Requested");
        if (RandomPoint(agent.transform.position, 10.0f, out destination))
        {
            agent.SetDestination(destination);
        }
        
    }

    void FindPositionNearTarget(GameObject tgtObject)
    {
        print("New Random Dest Requested");
        if (RandomPoint(HostileTarget.transform.position, 10.0f, out destination))
        {
            agent.SetDestination(destination);            
        }
    }

    void FindRetreatPosition()
    {
        RandomPoint(agent.transform.position, 10.0f, out destination);
    }

    void nextWaypoint()
    {
        if (CurrentWaypointIndex + 1 < Waypoints.Length)
        {
            CurrentWaypointIndex++;
        }
        else
        {
            CurrentWaypointIndex = 0;
        }

        StartCoroutine(waitAtLocation(1.4f));
        destination = (Waypoints[CurrentWaypointIndex].position);
        agent.SetDestination(destination);
    }

    

    /// This method generates a number of randomised positions in a sphere around the agent, then samples that position for vailidity on the navmesh using the SamplePosition method.
    bool RandomPoint(Vector3 center, float range, out Vector3 result)

    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + (Random.insideUnitSphere) * Random.Range(DestinationAcceptRadius + (DestinationAcceptRadius * 0.5f), range); //range;
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                agent.CalculatePath(hit.position, myPath);

                if (myPath.status != NavMeshPathStatus.PathComplete)
                {
                    result = transform.position;
                    return false;
                }
                else
                {
                    result = hit.position;

                    Debug.DrawLine(hit.position, hit.position + new Vector3(0, 2, 0));
                    return true;
                }                
            }
        }
        result = transform.position;
        return false;
    }
    

    //This method uses a spherecast between the agent and the HostileTarget's position (with an added offset)
    void sphereLineOfSight()
    {
        RaycastHit hit;

        Vector3 p1 = agent.transform.position + (transform.up * eyeHeight);
        float distanceToObstacle = 0;

        // Cast a sphere wrapping character controller 10 meters forward
        // to see if it is about to hit anything.
        if (HostileTarget)
        {
            Debug.DrawRay(p1, ((HostileTarget.GetComponent<Collider>().bounds.center - p1).normalized * 150.0f), Color.green);
            if (Physics.SphereCast(p1, 0.05f, (HostileTarget.GetComponent<Collider>().bounds.center - p1), out hit, AgentSightDistance))
            {
                distanceToObstacle = hit.distance;
                if (hit.collider.gameObject == HostileTarget.gameObject)
                {
                    seeHostileTarget = true;
                    lastSeenPosition = hit.point;
                    if (currentSight < defaultsightNoticeTime)
                    {
                        currentSight += Time.fixedDeltaTime;

                        if (currentSight >= defaultsightNoticeTime)
                        {
                            currentSight = defaultsightNoticeTime;
                            isAlerted = true;
                            StartCoroutine(SwapAiState(AiStates.Alert));
                            AgentAudio.PlayOneShot(spottedSound);
                        }
                    }



                    Debug.DrawLine(p1, hit.point, Color.red, Time.deltaTime);
                    //Debug.Log(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject != HostileTarget || !hit.collider)
                {
                    if (currentSight > 0)
                    {
                        //hold off on losing sight until timer expired
                        currentSight -= Time.deltaTime * loseSightSpeed;

                        //Debug.Log("Losing sight:" + sightDecayTime);
                        Debug.DrawRay(p1, ((HostileTarget.GetComponent<Collider>().bounds.center - p1).normalized * 150.0f), Color.yellow);
                        if (currentSight <= 0)
                        {
                            currentSight = 0;
                            seeHostileTarget = false;
                            StartCoroutine(SwapAiState(AiStates.Wander));
                            //AgentAudio.PlayOneShot(lostSightSound);
                        }
                    }




                }
            }
        }
    }

    void chaseTarget()
    {
        if (seeHostileTarget)
        {
            destination = HostileTarget.transform.position + (HostileTarget.transform.forward * (meleeAttackDistance * 0.5f));
        }
        else
        {
            destination = lastSeenPosition;
        }
        tickDistanceToTarget();
        
        //destination = HostileTarget.transform.position + (HostileTarget.transform.forward * (meleeAttackDistance * 0.5f));

        //destination = lastSeenPosition;
        //Offset Hostile Position from player to prevent 'crowding'
        //destination = HostileTarget.transform.position + (HostileTarget.transform.forward * (meleeAttackDistance * 0.5f));

        //offset destination based upon approach direction
        //destination = HostileTarget.transform.position + ( HostileTarget.transform.position + (HostileTarget.transform.position - transform.position).normalized * (meleeAttackDistance * 0.5f));
        
        //FindPositionNearTarget(HostileTarget);
        agent.SetDestination(destination);
        //agent.
    }



    void tickDistanceToTarget()
    {
        distanceToTarget = Vector3.Distance(HostileTarget.transform.position, agent.transform.position);

        //test whether the current HostileTarget is within X units (EnemySightDistance) of the agent
        if (distanceToTarget < meleeAttackDistance)
        {
            StartCoroutine(SwapAiState(AiStates.MeleeAttack));
        }
    }

    bool hasReachedDestination()
    {
        distanceToDest = Vector3.Distance(destination, agent.transform.position);

        //test whether the current Destination is within X units (DestinationAcceptRadius) of the agent
        if (distanceToDest < DestinationAcceptRadius)
        {
            //agent.isStopped = true;
            //Debug.Log("Agent Reached Destination");
            
            return true;
            
            //chase the HostileTarget by setting our destination to the HostileTarget location evey frame
            //  destination = GameObject.FindGameObjectWithTag("HostileTarget").transform.position;
        }
        else
        {
            return false;
        }
    }

    

    IEnumerator doMeleeAttack()
    {

        //Debug.Log("Melee Attacking");
        RaycastHit meleeHit;
        if (Physics.BoxCast(GetComponent<Collider>().bounds.center, new Vector3(0.5f, 0.5f, 0.5f), transform.forward, out meleeHit, transform.rotation, meleeAttackDistance))
        {
            AgentAudio.PlayOneShot(meleeSound);
            Debug.DrawRay(GetComponent<Collider>().bounds.center, transform.forward, Color.cyan);
            //Debug.log("DAMAGIN!!");
            DamageHandler dh;
            if (dh = meleeHit.collider.gameObject.GetComponent<DamageHandler>())
            {
                dh.ApplyDamage(meleeDamage);
                if (meleeDamageFx)
                {
                    GameObject DamageParticles = GameObject.Instantiate(meleeDamageFx, gameObject.GetComponent<Collider>().bounds.center, transform.rotation);
                    Destroy(DamageParticles, 3.14f);
                }
            }            
        }
        yield return new WaitForSeconds(meleeAttackDuration);
        StartCoroutine(SwapAiState(previousState));
    }

    IEnumerator doRangeAttack()
    {
        yield return new WaitForSeconds(2.4f);

    }

    //section
    [Header("Audio Parameters")]
    public AudioClip AlertSound;
    public AudioClip awakeSound;
    public AudioClip wanderSound;
    public AudioClip chasingSound;
    public AudioClip retreatingSound;
    public AudioClip meleeSound;
    public AudioClip spottedSound;
    public AudioClip lostSightSound;
    public AudioClip rangedAttackSound;
    public AudioClip wayPointSound;

}
