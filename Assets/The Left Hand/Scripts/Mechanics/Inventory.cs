using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int kMaxInventory = 10;
    public List<GameObject> InventoryList => m_Inventory.ToList();
    public InventoryUI UI;

    GameObject[] m_Inventory = new GameObject[kMaxInventory];
    int m_NumberOfItems = 0;

    public void Open()
    {
        Debug.Log("Open inventory");
        UI.Open(InventoryList);
    }

    public void OpenForUseWith(IInteractable itemForUse)
    {
        Debug.Log("Open inventory");
        UI.Open(InventoryList, itemForUse);
    }

    public void Close()
    {
        Debug.Log("Close inventory");
    }

    public bool AddToInventory(GameObject objectToAdd)
    {
        if (m_NumberOfItems == kMaxInventory)
            return false;

        for (int i = 0; i < kMaxInventory; i++)
        {
            if (m_Inventory[i] == null)
            {
                m_Inventory[i] = objectToAdd;
                break;
            }
        }

        m_NumberOfItems++;
        return true;
    }

    public void RemoveFromInventory(GameObject itemToRemove)
    {
        for(int i = 0; i < kMaxInventory; i++)
        {
            if(m_Inventory[i] == itemToRemove)
            {
                m_Inventory[i] = null;
                m_NumberOfItems--;
                break;
            }
        }
    }
}
