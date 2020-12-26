using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    TextMeshProUGUI m_Text;
    InteractableObject m_IObject;
    bool m_IsAmmo;

    public void Init(TextMeshProUGUI t, InteractableObject iObj)
    {
        m_Text = t;
        m_IObject = iObj;
        m_IsAmmo = m_IObject is Ammo;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Text.text = m_IObject.name;
        if (m_IsAmmo)
            m_Text.text += $" x{m_IObject.GetComponent<Ammo>().NumberOfBullets}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_Text.text = "";
    }
}
