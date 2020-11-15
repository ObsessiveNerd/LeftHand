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
    bool m_IsInteracting = false;
    bool m_UIEnabled;
    Transform m_Player;

    void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;
        UIFactory.UIEnabled += (enabled) =>
        {
            m_UIEnabled = enabled;
        };
    }

    void Update()
    {
        if(m_UIEnabled)
        {
            Clear();
            return;
        }

        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);

        if(Physics.Raycast(ray, out RaycastHit info))
        {
            IInteractable interactable = info.collider.gameObject.GetComponent<IInteractable>();
            if (interactable != null && Vector3.Distance(info.collider.transform.position, m_Player.position) < Distance)
            {
                m_CurrentObject = interactable;
                if (m_TextInstance != null)
                    Destroy(m_TextInstance);
                m_TextInstance = Instantiate(TextPrefab, mouse, Quaternion.identity, Canvas.transform);
                m_TextInstance.GetComponent<TextMeshProUGUI>().text = m_CurrentObject.UseWord;
            }
            else if(m_CurrentObject != null && !m_IsInteracting)
            {
                Clear();
            }
        }
        else
        {
            Clear();
        }

        if (Input.GetMouseButtonDown(0) && m_CurrentObject != null)
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
