using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorSwitch : MonoBehaviour {

    public FlickeringLight[] targetLights;

    public virtual void toggleSwitch()
    {
        print("switch Toggled");

        foreach (FlickeringLight currentLight in targetLights)
        {
            currentLight.toggleLight();
        }

    }
}
