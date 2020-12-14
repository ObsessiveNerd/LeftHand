using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour, IInteractable
{
    public int NumberOfBullets;

    public string UseWord => throw new System.NotImplementedException();

    public bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
