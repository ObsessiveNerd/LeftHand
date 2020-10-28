using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct FactoryButton
{
    public FactoryButton(string text, Action action)
    {
        ButtonAction = action;
        ButtonText = text;
    }

    public Action ButtonAction;
    public string ButtonText;
}

public class UIFactory : MonoBehaviour
{
    public GameObject DialogueOption;
    public GameObject Button;

    static UIFactory m_Instance;
    static GameObject m_Canvas;

    void Start()
    {
        m_Canvas = FindObjectOfType<Canvas>().gameObject;
        if (m_Instance != null)
            Destroy(m_Instance);
        m_Instance = this;
    }

    public static void CreateDialogue(string text, params FactoryButton[] buttons)
    {
        GameObject dialogueOption = Instantiate(m_Instance.DialogueOption, m_Canvas.transform);
        dialogueOption.GetComponentInChildren<TextMeshProUGUI>().text = text;
        Transform buttonArea = dialogueOption.transform.Find("ButtonArea");
        foreach(FactoryButton button in buttons)
        {
            GameObject newButton = Instantiate(m_Instance.Button, buttonArea);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = button.ButtonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => button.ButtonAction());
        }

        GameObject cancelButton = Instantiate(m_Instance.Button, buttonArea);
        cancelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";
        cancelButton.GetComponent<Button>().onClick.AddListener(() => Destroy(dialogueOption));
    }

    public static void CreatePuzzleInput(GameObject uiObject, Action submit)
    {

    }

    public static void CreatePuzzleInput(GameObject uiObject, Action<object> submit)
    {

    }
}
