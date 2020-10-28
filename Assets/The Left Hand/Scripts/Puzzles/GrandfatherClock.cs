using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrandfatherClock : MonoBehaviour, IInteractable
{
    public string CorrectTime;
    public string UseWord { get { return "Look"; } }
    public GameObject SubmitTime;

    string m_Time = "2:46";

    public bool Interact()
    {
        UIFactory.CreateDialogue($"The clock says it's {m_Time}...", new FactoryButton("Change clock", () =>
        {
            GameObject go = Instantiate(SubmitTime, GameObject.Find("Canvas").transform);
            go.GetComponentInChildren<TMP_InputField>().onEndEdit.AddListener((data) =>
            {
                Submit(data);
                Destroy(go);
            });
        }));
        //UIFactory.CreateDialogue("The clock says it's 2:48... that isn't right.");
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }

    void Submit(string data)
    {
        if (DateTime.TryParse(data, out DateTime res))
        {
            m_Time = data;
            if (data == CorrectTime)
                Debug.Log("That worked!");
            else
                Debug.Log("Hmm... nothing changed.");
        }
        else
            Debug.Log("That... it's a time...");
    }
}
