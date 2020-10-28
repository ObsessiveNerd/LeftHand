using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public Key KeyToUnlock;
    public bool Locked;

    public string UseWord
    {
        get
        {
            if (Locked)
                return "Unlock";
            return "Open";
        }
    }

    public bool Interact()
    {
        if (Locked)
        {
            FindObjectOfType<Inventory>().OpenForUseWith(this);
            return true;
        }

        Open();
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        if (objectToUse == null)
            return false;

        Key key = objectToUse.GetComponent<Key>();
        if (key != null && key.KeyId == KeyToUnlock.KeyId)
        {
            Unlock();
            return true;
        }

        return false;
    }

    void Open()
    {
        if (!Locked)
            Destroy(gameObject);
    }

    void Unlock()
    {
        Locked = false;
    }
}
