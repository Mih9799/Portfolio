using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSwitchObjectOn : Interactable
{
    public GameObject targetGameObject;
    public override void StartInteraction()
    {
        //base.StartInteraction();
        targetGameObject.SetActive(!targetGameObject.activeSelf);
    }
}
