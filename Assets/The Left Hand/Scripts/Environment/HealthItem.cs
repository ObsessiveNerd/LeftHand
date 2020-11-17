using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour, IInteractable, IUseable
{
    public int HealthRecover;

    public string UseWord => "Pickup Health Potion";

    public bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }

    public void Use()
    {
        Inventory inventory = FindObjectOfType<Inventory>();
        HealthController healthController = inventory.gameObject.GetComponent<HealthController>();

        healthController.Heal(HealthRecover);
        inventory.RemoveFromInventory(gameObject);
    }
}
