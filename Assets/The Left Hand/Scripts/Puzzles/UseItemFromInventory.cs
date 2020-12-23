using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemFromInventory : MonoBehaviour
{
    public GameObject key;

    public void Use()
    {
        BasicLock bLock = GetComponent<BasicLock>();
        if (bLock == null || bLock.Locked)
        {
            UIFactory.Clear();
            FindObjectOfType<Inventory>().OpenForUseWith((item) => Interact(item));
        }
        else
        {
            UIFactory.CreateDialogue("Nothing more I can use here.");
        }
    }

    bool Interact(GameObject go)
    {
        if(go.GetComponent<Key>().KeyName == key.GetComponent<Key>().KeyName)
        {
            GetComponent<IUnlockable>().Unlock();
            if (go.GetComponent<Key>().DestroyAfterUse)
                FindObjectOfType<Inventory>().RemoveFromInventory(go);
            return true;
        }

        return false;
    }
}
