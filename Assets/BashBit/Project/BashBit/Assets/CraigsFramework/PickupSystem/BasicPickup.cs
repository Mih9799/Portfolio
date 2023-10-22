using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPickup : MonoBehaviour {

    public PickupTypes itemType;
    public float itemValue = 0.0f;
    public bool destroyOnPickup = true;
    public PlayerController Pcon;
    
    private void Awake()
    {

    }
    private void Start()
    {
        if (PlayerController.instance)
        {
            Pcon = PlayerController.instance;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ApplyPickup(itemType, itemValue);
        }
    }

    public virtual void ApplyPickup(PickupTypes thisItemType, float itemValue)
    {
        print("Item Picked up: " + itemValue + thisItemType.ToString());

        if (Pcon)
        {
            Pcon.ApplyPickuptoPlayerCon(thisItemType, itemValue);
        }

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}
