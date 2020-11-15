using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingWithUnlocksSomething : MonoBehaviour, IInteractable
{
    public string UseWord => "Investigate";
    public GameObject Unlockable;
    public string InteractionDialogue;
    public GameObject Puzzle;

    bool m_PuzzleComplete = false;
    IUnlockable m_Unlockable;

    void Start()
    {
        m_Unlockable = Unlockable.GetComponent<IUnlockable>();
    }

    public bool Interact()
    {
        if (!m_PuzzleComplete)
            UIFactory.CreateDialogue(InteractionDialogue, true, null, new FactoryButton("Interact", () =>
            {
                UIFactory.Clear();
                Puzzle.SetActive(true);
                var puzzle = Puzzle.GetComponent<IPuzzle>();
                puzzle.Setup(this);
            }));
        else
            UIFactory.CreateDialogue("Nothing of interest here.");
        return true;
    }

    public void CompletePuzzle()
    {
        m_Unlockable?.Unlock();
        m_PuzzleComplete = true;
        Puzzle.SetActive(false);
        UIFactory.Clear();
    }

    public bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
