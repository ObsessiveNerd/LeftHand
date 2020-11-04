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

    static GameObject m_CurrentDialoge = null;

    void Start()
    {
        m_Canvas = FindObjectOfType<Canvas>().gameObject;
        if (m_Instance != null)
            Destroy(m_Instance);
        m_Instance = this;
    }

    public static void Clear()
    {
        if (m_CurrentDialoge != null)
        {
            Destroy(m_CurrentDialoge);
            m_CurrentDialoge = null;
        }
    }

    public static void CreateDialogue(string text, bool hasLeaveButton = true, Action leaveCallback = null, params FactoryButton[] buttons)
    {
        Clear();

        m_CurrentDialoge = Instantiate(m_Instance.DialogueOption, m_Canvas.transform);
        m_CurrentDialoge.GetComponentInChildren<TextMeshProUGUI>().text = text;
        Transform buttonArea = m_CurrentDialoge.transform.Find("ButtonArea");
        CreateButtons(buttonArea, buttons);

        if (hasLeaveButton)
        {
            GameObject cancelButton = Instantiate(m_Instance.Button, buttonArea);
            cancelButton.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";
            cancelButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                Clear();
                leaveCallback?.Invoke();
            });
        }
    }

    static void CreateButtons(Transform parent, FactoryButton[] buttons)
    {
        foreach (FactoryButton button in buttons)
        {
            GameObject newButton = Instantiate(m_Instance.Button, parent);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = button.ButtonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => button.ButtonAction());
        }
    }

    public static void CreateCustomUI(GameObject uiObject, params FactoryButton[] buttons)
    {
        Clear();
        m_CurrentDialoge = Instantiate(uiObject, m_Canvas.transform);
        CreateButtons(m_CurrentDialoge.transform, buttons);
    }

    public static void CreatePuzzleInput(GameObject uiObject, Action submit)
    {

    }

    public static void CreatePuzzleInput(GameObject uiObject, Action<object> submit)
    {

    }
}
