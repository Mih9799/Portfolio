using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public Vector3 TargetLocation;
    public float MoveSpeed = 1.0f;
    public int MoveAmount = 1;
    float distanceToTravel = 0;
    public bool playerControlled = true;

    private bool CanMove()
    {
        return (transform.position - TargetLocation).magnitude < 0.001f;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("MoveLeft"))
        {
            if (CanMove())
                TargetLocation = transform.position + transform.right * -MoveAmount;
            setMoveDistance();
        }
        else if (Input.GetButtonDown("MoveRight"))
        {
            if (CanMove())
                TargetLocation = transform.position + transform.right * MoveAmount;
            setMoveDistance();
        }
        else if (Input.GetButtonDown("MoveForward"))
        {
            if (CanMove())
                TargetLocation = transform.position + transform.forward * MoveAmount;
            setMoveDistance();
        }
        else if (Input.GetButtonDown("MoveBack"))
        {
            if (CanMove())
                TargetLocation = transform.position + transform.forward * -MoveAmount;
            setMoveDistance();
        }
        //place a condition aroudn executing the movement code
        //check the distance to the desired location
        //could apply a trace downward from the object to determine if it is over a surface/tile, if not, then do not execute the Movement code and allow physics full control
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerControlled = !playerControlled;
        }

        MoveObjectToLocation();
    }

    void MoveObjectToLocation()
    {
        if (playerControlled)
        {
            TargetLocation.x = Mathf.Floor(TargetLocation.x + 0.5f);
            TargetLocation.z = Mathf.Floor(TargetLocation.z + 0.5f);
            this.gameObject.transform.position = Vector3.Lerp(transform.position, TargetLocation, (Time.deltaTime * MoveSpeed));

            //update the current distance from target location to a float for comparison to the total movement distance.
            float MoveDelta = Vector3.Distance(this.gameObject.transform.position, TargetLocation);

            print(MoveDelta);
        }
 
    }

    void setMoveDistance()
    {
        //store the total distance to be traversed by the player object
        distanceToTravel = (this.gameObject.transform.position - TargetLocation).magnitude;
    }
}

