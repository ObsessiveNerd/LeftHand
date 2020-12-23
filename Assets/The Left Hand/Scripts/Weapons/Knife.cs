using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : InteractableObject, IWeapon
{
    public int Damage;
    public float Distance;
    public GameObject Prefab;

    Vector3 m_StartPosition;
    Quaternion m_StartRot;
    Vector3 m_StartScale;
    Cooldown m_Cooldown;
    public override string UseWord
    {
        get 
        {
            return "Pickup knife";
        }
    }

    public string WeaponName
    {
        get
        {
            return "Knife";
        }
    }

    void Start()
    {
        m_Cooldown = new Cooldown();
    }

    public void Attack(GameObject source, Vector3 direction)
    {
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "Enemy" &&
                Vector3.Distance(hitInfo.collider.gameObject.transform.position, source.transform.position) < Distance &&
                m_Cooldown.IsReady)
            {
                var healthController = hitInfo.collider.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(Damage);
                StartCoroutine(m_Cooldown.BeginCooldown());
            }
        }
    }

    public override bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
