using UnityEngine;
using System.Collections;

public class DeathScript : MonoBehaviour {

	public GameObject player;		//Player we want to reset
	public GameObject spawnPoint;	//Spawn point

	public int damage;				//Damage to the player

	// Use this for initialization
	void Start () 
	{
	}

	// Called when another game object collides with this collider
//	void OnTriggerEnter(Collider c)
//	{
//		//Check to see if the script is enabled.
//		if(enabled)
//		{
//			//If player touches Deathzone Then
//			if(c.gameObject == player) 
//			{
//				//Reset player to beginning of level
//				player.transform.position = spawnPoint.transform.position;	
//			}
//		}
//	}

	void OnTriggerStay(Collider c)
	{
		//Check to see if the script is enabled.
		if(enabled)
		{
			//While the player touches Deathzone Then
			if(c.gameObject == player) 
			{
				//Reset player to beginning of level
				player.GetComponent<PlayerScript>().health = player.GetComponent<PlayerScript>().health - damage;

				if(player.GetComponent<PlayerScript>().health <= 0)
				{
					//respawn, and reset player health
					player.GetComponent<PlayerScript>().isDead = false;
					player.GetComponent<PlayerScript>().health = player.GetComponent<PlayerScript>().maxHealth;
					player.transform.position = spawnPoint.transform.position;	
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
