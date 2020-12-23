using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractingWithUnlocksSomething : InteractableObject
{
    public override string UseWord => "Investigate";
    public GameObject Unlockable;
    public string InteractionDialogue;
    public GameObject Puzzle;

    bool m_PuzzleComplete = false;
    IUnlockable m_Unlockable;

    void Start()
    {
        m_Unlockable = Unlockable.GetComponent<IUnlockable>();
    }

    public override bool Interact()
    {
        if (!m_PuzzleComplete)
            UIFactory.CreateDialogue(InteractionDialogue, true, null, new FactoryButton("Interact", () =>
            {
                UIFactory.Clear();
                Puzzle.SetActive(true);
                UIFactory.RegisterUI(Puzzle);
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

    public override bool Interact(GameObject objectToUse)
    {
        return false;
    }
}
