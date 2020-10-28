using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour, IInteractable
{
    public string UseWord { get { return "Investigate"; } }

    public bool Interact()
    {
        UIFactory.CreateDialogue("It looks like this was barricaded intentionally... I wonder why.");
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
