using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool HasBeenPickedUp = false;

    public static void PickupItem(GameObject gameObjectToPickup)
    {
        if (gameObjectToPickup.GetComponent<Pickup>().HasBeenPickedUp)
            return;

        FindObjectOfType<Inventory>().AddToInventory(gameObjectToPickup);

        //Maybe temporary?
        gameObjectToPickup.transform.position = new Vector3(1000, 1000, 1000);
        gameObjectToPickup.GetComponent<Pickup>().HasBeenPickedUp = true;
    }
}
