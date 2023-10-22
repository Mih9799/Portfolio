using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsEnableTriggerScript : MonoBehaviour
{
    //we will explicitly decalre the Physics object we want to affect here by declaring apublic variable.
    public Rigidbody TargetRigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Overlapped with Trigger");
            //Disabling isKinematic will enable simulation on the target RigidBody
            TargetRigidBody.isKinematic = false;
        }
        else
        {
            Debug.Log(other.gameObject.name);
        }

    }
    
}
