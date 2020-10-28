using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public string KeyName;

    public int KeyId
    {
        get
        {
            return KeyName.GetHashCode();
        }
    }

    public string UseWord
    {
        get
        {
            return $"Pickup {KeyName}";
        }
    }

    public bool Interact()
    {
        FindObjectOfType<Inventory>().AddToInventory(gameObject);
        gameObject.SetActive(false);
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
