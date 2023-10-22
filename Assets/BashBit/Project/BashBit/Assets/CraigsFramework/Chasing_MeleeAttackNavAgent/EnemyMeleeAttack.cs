using UnityEngine;
using System.Collections;

public class EnemyMeleeAttack : MonoBehaviour {

    public GameObject Player;
    public float AttackDistance = 2.0f;
    public float AttackDelayTime = 0.5f;
    bool isAttacking = false;

    public AudioClip[] AttackSounds;

    UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, Player.transform.position) < AttackDistance)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                AudioSource MyAudio;
                print("ATAACKKK!!");                
                MyAudio = this.GetComponent<AudioSource>();

                MyAudio.clip = AttackSounds[Random.Range(0, AttackSounds.Length)];
                MyAudio.pitch = Random.Range(0.3f, 1.5f);
                MyAudio.Play();
                StartCoroutine(AttackDelay());
            }

        }

	}

    //This enumerator will hold execution of the code below until it 'returns', at which point it will switch our boolean back to false, allowing the Update function to execute the attack again.
    IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(AttackDelayTime);
        isAttacking = false;
    }
}
