using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandfatherClock : InteractableObject
{
    public string CorrectTime;
    public override string UseWord { get { return "Look"; } }
    public GameObject SubmitTime;
    public GameObject KeyReward;

    bool m_IsOpen = false;
    bool m_KeyTook = false;
    string m_Time = "2:46";

    public override bool Interact()
    {
        if (!m_IsOpen)
        {
            UIFactory.CreateDialogue($"The clock says it's {m_Time}... There appears to be a pair of bolt cutters locked inside the clock.",
                true,
                null,
                new FactoryButton("Change clock", () =>
            {
                GameObject go = Instantiate(SubmitTime, GameObject.Find("Canvas").transform);
                go.GetComponentInChildren<TMP_InputField>().onEndEdit.AddListener((data) =>
                {
                    Submit(data);
                    Destroy(go);
                });
            }));
        }
        else if(m_IsOpen && !m_KeyTook)
        {
            GiveKey();
        }
        else
        {
            UIFactory.CreateDialogue("It's a beautiful grandfather clock.");
        }
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }

    void Submit(string data)
    {
        if (DateTime.TryParse(data, out DateTime res))
        {
            m_Time = data;
            if (data == CorrectTime)
            {
                UIFactory.CreateDialogue("The door on the clock is unlocked now!", true, () => GiveKey());
                m_IsOpen = true;
            }
            else
                UIFactory.CreateDialogue("Hmm... That didn't appear to do anything.");
        }
        else
            UIFactory.CreateDialogue("That isn't a time you fuck.");
    }

    void GiveKey()
    {
        UIFactory.CreateDialogue("Take the pair of bolt cutters?", true, null, 
            new FactoryButton("Take", () =>
        {
            FindObjectOfType<Inventory>().AddToInventory(KeyReward);
            m_KeyTook = true;
            UIFactory.Clear();
        }));
    }
}
