using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable, IUnlockable
{
    public Key KeyToUnlock;
    public bool Locked;
    public string DescriptionText;

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
            UIFactory.CreateDialogue(DescriptionText, true, null, 
                new FactoryButton("Use Item", 
                () =>
                {
                    UIFactory.Clear();
                    FindObjectOfType<Inventory>().OpenForUseWith((item) => Interact(item));
                }));
            
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

    public void Open()
    {
        if (!Locked)
            Destroy(gameObject);
    }

    public void Unlock()
    {
        UIFactory.CreateDialogue("The door is unlocked now.");
        Locked = false;
    }
}
