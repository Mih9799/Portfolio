using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBallThrower : MonoBehaviour {

    public GameObject DodgeBallPrefab;
    GameObject playerGO;

    public LineOfSight LOS;

    private void Start()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(throwBalls());
        LOS = gameObject.GetComponent<LineOfSight>();
    }

    IEnumerator throwBalls()
    {
        while (true)
        {           
            if (LOS.seePlayer)
            {
                Instantiate(DodgeBallPrefab, transform.position, transform.rotation);
            }      
            
            yield return new WaitForSeconds(1);
        }
    }
}
