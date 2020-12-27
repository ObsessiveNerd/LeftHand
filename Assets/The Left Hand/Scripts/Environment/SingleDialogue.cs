using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDialogue : InteractableObject
{
    public string Dialogue;
    public override string UseWord { get { return "Investigate"; } }

    public override bool Interact()
    {
        UIFactory.CreateDialogue(Dialogue);
        return true;
    }

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
