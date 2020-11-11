using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingWithUnlocksSomething : MonoBehaviour, IInteractable
{
    public string UseWord => "Investigate";
    public GameObject Unlockable;
    public string InteractionDialogue;

    bool m_PuzzleComplete = false;
    IUnlockable m_Unlockable;

    void Start()
    {
        m_Unlockable = Unlockable.GetComponent<IUnlockable>();
    }

    public bool Interact()
    {
        if (!m_PuzzleComplete)
            UIFactory.CreateDialogue(InteractionDialogue, true, null);
        else
            UIFactory.CreateDialogue("Nothing of interest here.");
        return true;
    }

    public void CompletePuzzle()
    {
        m_Unlockable?.Unlock();
        m_PuzzleComplete = true;
        UIFactory.Clear();
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
