using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour
{
	public Rigidbody projectile;
	public float speed = 50;
	
	// Update is called once per frame
	void Update ()
	{
		//when the fire button is pushed
		if (Input.GetButtonDown("Fire1"))
		{
			//create the bullet using the public Rigidbody variable "Projectile" which is a prefab added in the bulletspawnpoints inspector component of this script
			Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
			instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(speed, 0, 0));
		}
	}
}
