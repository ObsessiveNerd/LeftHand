using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    const int kMaxInventory = 20;
    public List<GameObject> InventoryList => m_Inventory.ToList();
    public InventoryUI UI;
    public AudioClip PickupClip;

    AudioSource m_Audio;
    GameObject[] m_Inventory = new GameObject[kMaxInventory];
    int m_NumberOfItems = 0;
    bool m_Open;

    public void Open()
    {
        UI.Open(InventoryList);
        m_Open = true;
    }

    public void OpenForUseWith(Func<GameObject, bool> callbackForSelectedItem)
    {
        UI.Open(InventoryList, callbackForSelectedItem);
        m_Open = true;
    }

    public void Close()
    {
        UI.Close();
        m_Open = false;
    }

    public bool AddToInventory(GameObject objectToAdd)
    {
        if (m_NumberOfItems == kMaxInventory)
            return false;

        if (m_Audio == null)
            m_Audio = GetComponent<AudioSource>();
        m_Audio.PlayOneShot(PickupClip);

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

    public int GetAmmoCount()
    {
        foreach(var go in m_Inventory)
        {
            if(go != null && go.GetComponent<Ammo>() != null)
                return go.GetComponent<Ammo>().NumberOfBullets;
        }
        return 0;
    }

    public void AddAmmo(int amount)
    {
        foreach (var go in m_Inventory)
        {
            if (go != null && go.GetComponent<Ammo>() != null)
                go.GetComponent<Ammo>().NumberOfBullets += amount;
        }
    }

    public void RemoveAmmo(int amount)
    {
        foreach (var go in m_Inventory)
        {
            if (go != null && go.GetComponent<Ammo>() != null)
            {
                go.GetComponent<Ammo>().NumberOfBullets = Mathf.Clamp(go.GetComponent<Ammo>().NumberOfBullets - amount, 0, go.GetComponent<Ammo>().NumberOfBullets);
                if (go.GetComponent<Ammo>().NumberOfBullets == 0)
                    RemoveFromInventory(go);
            }
        }
    }
}
