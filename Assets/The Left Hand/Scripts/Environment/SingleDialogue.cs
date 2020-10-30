using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDialogue : MonoBehaviour, IInteractable
{
    public string Dialogue;
    public string UseWord { get { return "Investigate"; } }

    public bool Interact()
    {
        UIFactory.CreateDialogue(Dialogue);
        return true;
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
