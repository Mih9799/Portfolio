using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public InteractionTypes interactionType;

    public virtual void StartInteraction()
    {
     //   print("Interaction Started");
    }

    public virtual void StopInteraction()
    {
      //  print("Interaction Stopped");
    }

    public virtual void TickInteraction()
    {
       // print("Interaction Updated" + Time.deltaTime);
    }

}
