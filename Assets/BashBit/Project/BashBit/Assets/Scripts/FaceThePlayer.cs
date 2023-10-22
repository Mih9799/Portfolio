using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceThePlayer : MonoBehaviour
{
    public GameObject m_player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = m_player.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0), 0.75f);
    }
}
