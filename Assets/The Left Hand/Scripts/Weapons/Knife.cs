using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Cooldown, IWeapon, IInteractable
{
    public int Damage;
    public float Distance;

    public string UseWord
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

    public void Attack(GameObject source, Vector3 direction)
    {
        Vector3 mouse = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "Enemy" &&
                Vector3.Distance(hitInfo.collider.gameObject.transform.position, source.transform.position) < Distance &&
                IsReady)
            {
                var healthController = hitInfo.collider.gameObject.GetComponent<HealthController>();
                healthController.TakeDamage(Damage);
                StartCoroutine(BeginCooldown());
            }
        }
    }

    public bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
