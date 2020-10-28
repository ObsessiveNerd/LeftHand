using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    string UseWord { get; }
    bool Interact();
    bool Interact(GameObject objectToUse);
}
