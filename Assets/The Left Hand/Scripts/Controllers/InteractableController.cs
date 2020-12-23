using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableController : MonoBehaviour
{
    public GameObject TextPrefab;
    public GameObject Canvas;
    public float Distance;

    IInteractable m_CurrentObject;
    GameObject m_TextInstance;
    bool m_UIEnabled;
    GameObject m_ClosestInteractable;

    void Start()
    {
        UIFactory.UIEnabled += (enabled) =>
        {
            m_UIEnabled = enabled;
        };
    }

    public void SubmitInteractable(GameObject interactable)
    {
        if (m_ClosestInteractable == null)
        {
            m_ClosestInteractable = interactable;
            return;
        }

        if (Vector3.Distance(interactable.transform.position, transform.position) < Vector3.Distance(m_ClosestInteractable.transform.position, transform.position))
            m_ClosestInteractable = interactable;

        CheckIfClosestInteractableIsCloseEnoughToInteract();
    }

    private void CheckIfClosestInteractableIsCloseEnoughToInteract()
    {
        if (m_UIEnabled)
        {
            Clear();
            return;
        }

        if (Vector3.Distance(m_ClosestInteractable.transform.position, transform.position) < Distance)
        {
            IInteractable interactable = m_ClosestInteractable.GetComponent<IInteractable>();
            if (interactable != null)
            {
                m_CurrentObject = interactable;
                if (m_TextInstance != null)
                    Destroy(m_TextInstance);
                m_TextInstance = Instantiate(TextPrefab, new Vector3(Screen.width/2, Screen.height/2, 0f), Quaternion.identity, Canvas.transform);
                m_TextInstance.GetComponent<TextMeshProUGUI>().text = m_CurrentObject.UseWord;
            }
        }
        else
        {
            Clear();
        }

        if (Input.GetKeyDown(KeyCode.E) && m_CurrentObject != null)
            m_CurrentObject.Interact();
    }

    void Clear()
    {
        if (m_TextInstance != null)
        {
            Destroy(m_TextInstance);
            m_TextInstance = null;
            m_CurrentObject = null;
        }
    }
}
