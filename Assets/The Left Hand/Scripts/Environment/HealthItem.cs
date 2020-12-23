using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : InteractableObject, IUseable
{
    public int HealthRecover;

    public override string UseWord => "Pickup Health Potion";

    public override bool Interact()
    {
        Pickup.PickupItem(gameObject);
        return true;
    }

    public override bool Interact(GameObject objectToUse)
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
