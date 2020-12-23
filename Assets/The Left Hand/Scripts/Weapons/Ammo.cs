using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : InteractableObject
{
    public int NumberOfBullets;

    public override string UseWord => throw new System.NotImplementedException();

    public override bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
