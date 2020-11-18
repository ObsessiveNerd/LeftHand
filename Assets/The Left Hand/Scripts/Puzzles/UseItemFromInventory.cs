using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItemFromInventory : MonoBehaviour
{
    public GameObject key;

    public void Use()
    {
        UIFactory.Clear();
        FindObjectOfType<Inventory>().OpenForUseWith((item) => Interact(item));
    }

    bool Interact(GameObject go)
    {
        if(go == key)
        {
            GetComponent<IUnlockable>().Unlock();
            return true;
        }

        return false;
    }
}
