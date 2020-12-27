using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : InteractableObject
{
    public int NumberOfBullets;

    public override string UseWord => "Pickup ammo";

    bool m_PickedUp;

    public override bool Interact()
    {
        if (FindObjectOfType<Inventory>().GetAmmoCount() == 0)
            Pickup.PickupItem(gameObject);
        else if(!m_PickedUp)
        {
            FindObjectOfType<Inventory>().AddAmmo(NumberOfBullets);
            m_PickedUp = true;
            Destroy(gameObject);
        }
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
