using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTileScript : MonoBehaviour {

    public enum TileType { yes, no, maybe }

    public TileType current_tileType;

	// Use this for initialization
	void Start () {
		
	}
	


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            switch (current_tileType)
            {
                case TileType.yes:
                    DoYesEffect();
                    break;
                case TileType.no:
                    DoNoEffect();
                    break;
                case TileType.maybe:
                    DoMaybeEffect();
                    break;
            }
        }
    }

    void DoYesEffect()
    {
        Debug.Log("YESSSSS!");
        gameObject.GetComponent<MeshRenderer>().material.color = Color.green;        
    }

    void DoNoEffect()
    {
        Rigidbody tileRB = gameObject.AddComponent<Rigidbody>();
        //tileRB.AddForce(transform.up * -1.0f * 3000, ForceMode.Impulse);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    void DoMaybeEffect()
    {
        Rigidbody tileRB = gameObject.AddComponent<Rigidbody>();
        //tileRB.AddForce(transform.up * 3000, ForceMode.Impulse);
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
}
