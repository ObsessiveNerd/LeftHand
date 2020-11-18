using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicLock : MonoBehaviour, IUnlockable
{
    public bool Locked;
    public Action UnlockCallback;

    public void Unlock()
    {
        Locked = false;
        UnlockCallback();
    }
}
