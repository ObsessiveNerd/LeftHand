using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : InteractableObject
{
    public string KeyName;
    public bool DestroyAfterUse;

    public int KeyId
    {
        get
        {
            return KeyName.GetHashCode();
        }
    }

    public override string UseWord
    {
        get
        {
            return $"Pickup {KeyName}";
        }
    }

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
