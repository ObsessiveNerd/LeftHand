using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPerson : Chest
{
    public override string UseWord => "Investigate";

    protected override void Open()
    {
        UIFactory.CreateDialogue(Description, true, null, new FactoryButton("Search", () => base.Open()));
    }
}
