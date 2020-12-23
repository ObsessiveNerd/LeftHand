using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Door>().OnUnlock += WinGame;
    }

    void WinGame()
    {
        UIFactory.CreateDialogue("You won!");
    }
}
