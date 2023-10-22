using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLightSwitch : Interactable
{
    public Light targetLight;
    public override void StartInteraction()
    {
        //base.StartInteraction();
        targetLight.enabled = (!targetLight.enabled);
    }
}
