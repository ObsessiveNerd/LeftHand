using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InventoryUI : Escape
{
    public GameObject MakeActive;
    public GameObject Grid;
    public GameObject Button;
    public GameObject Canvas;

    public GameObject WeaponContextMenu;
    public GameObject ItemContextMenu;

    List<GameObject> m_CreatedButtons = new List<GameObject>();
    List<GameObject> m_ContextMenus = new List<GameObject>();

    public void Open(List<GameObject> items, Func<GameObject, bool> callbackForSelectedItem = null)
    {
        MakeActive.SetActive(true);
        foreach(GameObject item in items)
        {
            if (item == null)
                continue;

            GameObject button = Instantiate(Button, Grid.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = item.name;

            //Temp, will need to add context menu at some point
            IWeapon weapon = item.GetComponent<IWeapon>();
            if (weapon != null)
            {
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ClearContextMenus();
                    Vector3 spawnPosition = button.transform.position;
                    spawnPosition.x += 50;
                    spawnPosition.y -= 25;
                    GameObject context = Instantiate(WeaponContextMenu, spawnPosition, Quaternion.identity, Canvas.transform);
                    bool clickedWeaponIsAlreadyEquiped = FindObjectOfType<PlayerController>().CurrentEquipment == weapon;
                    context.GetComponentInChildren<TextMeshProUGUI>().text = clickedWeaponIsAlreadyEquiped ? "Unequip" : "Equip";
                    context.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        if (clickedWeaponIsAlreadyEquiped)
                            FindObjectOfType<PlayerController>().UnequipWeapon();
                        else
                            FindObjectOfType<PlayerController>().Equip(weapon);
                        ClearContextMenus();
                    });
                    m_ContextMenus.Add(context);
                    
                });
            }
            else
            {
                button.GetComponent<Button>().onClick.AddListener(() =>
                {
                    ClearContextMenus();
                    Vector3 spawnPosition = button.transform.position;
                    spawnPosition.x += 50;
                    spawnPosition.y -= 25;
                    GameObject context = Instantiate(ItemContextMenu, spawnPosition, Quaternion.identity, Canvas.transform);

                    GameObject useButton = context.transform.Find("Use").gameObject;
                    GameObject combineButton = context.transform.Find("Combine").gameObject;
                    GameObject destroyButton = context.transform.Find("Discard").gameObject;

                    useButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        if (callbackForSelectedItem != null)
                        {
                            if (callbackForSelectedItem(item))
                                Debug.Log("Success!");
                            else
                                Debug.Log("Failure");
                        }
                        else if (item.GetComponent<IUseable>() != null)
                            item.GetComponent<IUseable>().Use();
                        else
                            Debug.Log("Failure");

                        ClearContextMenus();
                        Close();
                    });

                    combineButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        //TODO
                        ClearContextMenus();
                    });

                    destroyButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        FindObjectOfType<Inventory>().RemoveFromInventory(item);
                        ClearContextMenus();
                    });

                    m_ContextMenus.Add(context);
                });
            }

            m_CreatedButtons.Add(button);
        }

        GameObject closeButton = Instantiate(Button, Grid.transform);
        closeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        closeButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            Close();
        });

        m_CreatedButtons.Add(closeButton);
    }

    protected override void Cleanup()
    {
        if (m_ContextMenus.Count > 0)
            ClearContextMenus();
        else
            Close();
    }

    void ClearContextMenus()
    {
        foreach (GameObject button in m_ContextMenus)
            Destroy(button);
        m_ContextMenus.Clear();
    }

    public void Close()
    {
        MakeActive.SetActive(false);
        foreach (GameObject button in m_CreatedButtons)
            Destroy(button);
        m_CreatedButtons.Clear();
    }
}
