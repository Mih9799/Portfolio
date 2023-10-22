using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public GameObject TargetPlayer;
    public Vector3 Displacement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, Displacement.z));
        mouseWorld.z = 0;
        Vector3 MouseDisplacement = (TargetPlayer.transform.position - mouseWorld) / 10;
        transform.position = Vector3.Lerp(transform.position, TargetPlayer.transform.position + Displacement + MouseDisplacement, 15 * Time.deltaTime);
    }
}
