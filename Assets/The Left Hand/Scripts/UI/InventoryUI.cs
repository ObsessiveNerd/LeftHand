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
    public TextMeshProUGUI ItemName;
    public GameObject WeaponContextMenu;
    public GameObject ItemContextMenu;
    public Image EquipedItem;

    List<GameObject> m_CreatedButtons = new List<GameObject>();
    List<GameObject> m_ContextMenus = new List<GameObject>();

    public void Open(List<GameObject> items, Func<GameObject, bool> callbackForSelectedItem = null)
    {
        MakeActive.SetActive(true);
        foreach (GameObject item in items)
        {
            if (item == null)
                continue;

            GameObject button = Instantiate(Button, Grid.transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = "";
            
            var image = button.GetComponent<Image>();
            image.sprite = Services.Sprite.GetSpriteForKey(item.name);

            button.AddComponent<InventoryButtonBehavior>().Init(ItemName, item.GetComponent<InteractableObject>());

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

                    GameObject equipButton = context.transform.Find("Equip").gameObject;
                    equipButton.GetComponentInChildren<TextMeshProUGUI>().text = clickedWeaponIsAlreadyEquiped ? "Unequip" : "Equip";
                    equipButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        if (clickedWeaponIsAlreadyEquiped)
                        {
                            FindObjectOfType<PlayerController>().UnequipWeapon();
                            ItemUnequiped();
                        }
                        else
                        {
                            FindObjectOfType<PlayerController>().Equip(weapon);
                            ItemEquiped(weapon.WeaponName);
                        }
                        ClearContextMenus();
                    });
                    m_ContextMenus.Add(context);

                    GameObject destroyButton = context.transform.Find("Discard").gameObject;
                    destroyButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        FindObjectOfType<Inventory>().RemoveFromInventory(item);
                        ClearContextMenus();
                    });

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
                    //GameObject combineButton = context.transform.Find("Combine").gameObject;
                    GameObject destroyButton = context.transform.Find("Discard").gameObject;

                    useButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        if (callbackForSelectedItem != null)
                            callbackForSelectedItem(item);
                        else if (item.GetComponent<IUseable>() != null)
                            item.GetComponent<IUseable>().Use();

                        ClearContextMenus();
                        Close();
                    });

                    //combineButton.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    //{
                    //    //TODO
                    //    ClearContextMenus();
                    //});

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

        //GameObject closeButton = Instantiate(Button, Grid.transform);
        //closeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
        //closeButton.GetComponent<Button>().onClick.AddListener(() =>
        //{
        //    Close();
        //});

        //m_CreatedButtons.Add(closeButton);
        UIFactory.RegisterUI(MakeActive);
    }

    protected override void Cleanup()
    {
        if (m_ContextMenus.Count > 0)
            ClearContextMenus();
        else
            Close();
    }

    public void ItemEquiped(string ItemName)
    {
        EquipedItem.sprite = Services.Sprite.GetSpriteForKey(ItemName);
    }

    public void ItemUnequiped()
    {
        EquipedItem.sprite = null;
    }

    //protected override bool AlternativeCloseRequirementMet()
    //{
    //    return Input.GetKeyDown(KeyCode.I) && MakeActive.activeInHierarchy;
    //}

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
        UIFactory.UIEnabled(false);
    }
}
