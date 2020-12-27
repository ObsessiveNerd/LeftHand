using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCredits : Escape
{
    public GameObject Credits;

    public void DisplayCredits()
    {
        Credits.SetActive(true);
    }

    protected override void Cleanup()
    {
        Credits.SetActive(false);
    }
}
