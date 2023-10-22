using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraigsCameraCollision : MonoBehaviour
{

    public Transform FollowTarget;

    public float m_MinDistance = 1.0f, m_MaxDistance = 4.0f;
    public float m_Smooth = 15.0f;

    private Vector3 m_DollyDirection = new Vector3(0,0,1);

    public Vector3 m_DollyDirAdjusted;

    public float m_Distance;

    public Vector3 cameraOffset;
    public float cameraCollisionScalePercent = 0.87f;
    Vector3 camTargetLocation;
    public float cameraLeadDistance = 2.0f;


    // Use this for initialization
    void Awake()
    {

        m_DollyDirection = new Vector3(0, 0, -1);


    }



    // Update is called once per frame
    void Update()
    {
        //Calculate the Ideal Desired Camera Position
        m_DollyDirection = FollowTarget.transform.forward * -1.0f + cameraOffset;
        Vector3 desiredCamPos = FollowTarget.position + (FollowTarget.transform.forward * -1.0f  * m_MaxDistance);
       // Debug.Log("TargetTFM " + FollowTarget.position.ToString());
       // Debug.Log("FollowPos " +  desiredCamPos.ToString());

        RaycastHit hit;

        //Trace from the Target Transform out to the Desired Camera Position to determine if it would be blocked
        if (Physics.Linecast(FollowTarget.position, desiredCamPos, out hit))
        {
            //If blocked, set the Distance to Offset the camera by to the new value of m_Dsitance
            m_Distance = Mathf.Clamp((hit.distance * cameraCollisionScalePercent), m_MinDistance, m_MaxDistance);
            //Debug.Log(hit.transform.gameObject.name);
        }
        else
        {
            //If no collision occures, use the maximum ideal distance
            m_Distance = m_MaxDistance;

        }

        //Lerp from the Camera's current position to the new desired position, using the Target Transform as a starting poitn and offsetting by the m_distance value
        transform.position = Vector3.Lerp(transform.position, FollowTarget.position +  m_DollyDirection * m_Distance, Time.deltaTime * m_Smooth);
       
        //Brute force set rotation of camera to ensure player remnains in view

        ////transform.rotation = FollowTarget.transform.rotation;

        //Investigation: add a rotation Lerp here the same way as the Position to get super smoov rotation on camera

        //Comment out if undesirable, set the camera rotation to look at a point 'ahead' of the player to allow improved visibility (in theory)
        //Comment out these lines and re-enable line 66 for 'Brute force' reliable method.
        camTargetLocation = (FollowTarget.transform.position + (FollowTarget.transform.forward * cameraLeadDistance));
        transform.LookAt(camTargetLocation, FollowTarget.transform.up);

    }

}



