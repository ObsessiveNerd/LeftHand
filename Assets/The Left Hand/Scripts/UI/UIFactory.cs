using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct FactoryButton
{
    public FactoryButton(string text, Action action, bool isItem = false)
    {
        ButtonAction = action;
        ButtonText = text;
        IsItem = isItem;
    }

    public bool IsItem;
    public Action ButtonAction;
    public string ButtonText;
}

public class UIFactory : MonoBehaviour
{
    public static Action<bool> UIEnabled;
    public GameObject DialogueOption;
    public GameObject Button;

    static UIFactory m_Instance;
    static GameObject m_Canvas;

    static GameObject m_CurrentDialoge = null;
    static List<GameObject> m_PuzzleUIs = new List<GameObject>();

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
            UIEnabled(false);
        }

        foreach (var ui in m_PuzzleUIs)
            ui.SetActive(false);
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

        UIEnabled(true);
    }

    public static void RegisterUI(GameObject UI)
    {
        if (!m_PuzzleUIs.Contains(UI))
            m_PuzzleUIs.Add(UI);

        UIEnabled(UI.activeInHierarchy);
    }

    static void CreateButtons(Transform parent, FactoryButton[] buttons)
    {
        foreach (FactoryButton button in buttons)
        {
            GameObject newButton = Instantiate(m_Instance.Button, parent);
            if(!button.IsItem)
                newButton.GetComponentInChildren<TextMeshProUGUI>().text = button.ButtonText;
            newButton.GetComponent<Button>().onClick.AddListener(() => button.ButtonAction());

            if (button.IsItem)
            {
                var image = newButton.GetComponent<Image>();
                image.sprite = Services.Sprite.GetSpriteForKey(button.ButtonText);
            }
        }
    }

    public static void CreateCustomUI(GameObject uiObject, params FactoryButton[] buttons)
    {
        Clear();
        m_CurrentDialoge = Instantiate(uiObject, m_Canvas.transform);
        CreateButtons(m_CurrentDialoge.transform, buttons);
        UIEnabled(true);
    }

    public static void CreatePuzzleInput(GameObject uiObject, Action submit)
    {
        UIEnabled(true);
    }

    public static void CreatePuzzleInput(GameObject uiObject, Action<object> submit)
    {
        UIEnabled(true);
    }
}
