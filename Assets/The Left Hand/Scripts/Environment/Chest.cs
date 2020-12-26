using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject, IUnlockable
{
    public List<GameObject> ItemsContained;
    public bool Locked;
    public GameObject Key;
    public GameObject ChestUI;
    public string Description;

    bool m_UsesKey = false;
    bool m_IsOpen = false;
    string m_KeyId;

    public override string UseWord
    {
        get
        {
            if (Locked)
                return "Unlock";
            return "Open";
        }
    }

    public void Unlock()
    {
        Locked = false;
    }

    public override bool Interact()
    {
        if (Locked)
        {
            if (m_UsesKey)
                FindObjectOfType<Inventory>().OpenForUseWith((item) => Interact(item));
            else
                UIFactory.CreateDialogue(Description);
        }
        else
            Open();

        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        if ((!m_UsesKey || objectToUse.name == m_KeyId) && Locked)
        {
            Unlock();
            return true;
        }
        else
            Open();

        return false;
    }

    protected virtual void Open()
    {
        if (ItemsContained.Count > 0)
        {
            List<FactoryButton> buttons = new List<FactoryButton>();
            foreach (var objectInChest in ItemsContained)
            {
                buttons.Add(new FactoryButton(objectInChest.name, () =>
                {
                    ItemsContained.Remove(objectInChest);
                    FindObjectOfType<Inventory>().AddToInventory(objectInChest);
                    if (ItemsContained.Count > 0)
                        Open();
                    else
                        Close();
                }, true));
            }
            UIFactory.CreateCustomUI(ChestUI, buttons.ToArray());
        }
        else
        {
            UIFactory.CreateDialogue("There's nothing of interest here.", true, () => m_IsOpen = false);
        }

        m_IsOpen = true;
    }

    void Close()
    {
        UIFactory.Clear();
        m_IsOpen = false;
    }

    void Start()
    {
        if (Key != null)
        {
            m_KeyId = Key.name;
            m_UsesKey = true;
        }
    }

    new void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape) && m_IsOpen)
            Close();
    }
}
