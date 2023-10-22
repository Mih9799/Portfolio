using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StalkCamera : MonoBehaviour
{
    public GameObject TargetCamera;
    public Vector3 Displacement = new Vector3(-7.75f, 3.5f, 12.5f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, TargetCamera.transform.position + Displacement, 5 * Time.deltaTime);
    }
}
