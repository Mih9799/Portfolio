using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInOut : MonoBehaviour {

    bool isInCar = false;

    private void OnTriggerStay(Collider other)
    {
        //Ascertain if the Player is the overlapping object
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if the player is not already in the vehicle
                if (isInCar == false)
                {
                    EnterCar();
                }

                if (isInCar == true)
                {
                    ExitCar();
                }
            }
        }
    }

    void EnterCar()
    {
        //do all the acr etner stuff here

    }

    void ExitCar()
    {
        //All the exiut functoinality yay
    }
}
