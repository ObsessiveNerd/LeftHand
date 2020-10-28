using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public static void PickupItem(GameObject gameObjectToPickup)
    {
        FindObjectOfType<Inventory>().AddToInventory(gameObjectToPickup);

        //Maybe temporary?
        gameObjectToPickup.transform.position = new Vector3(1000, 1000, 1000);
    }
}
