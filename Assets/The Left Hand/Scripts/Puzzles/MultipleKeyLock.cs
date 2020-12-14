using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultipleKeyLock : Escape, IPuzzle
{
    public List<GameObject> Locks;
    public bool Locked;

    InteractingWithUnlocksSomething m_Iwus;
    int m_TotalLocks = 0;
    int m_LocksUnlocked = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var bl in Locks.Select(l => l.GetComponent<BasicLock>()).ToList())
        {
            bl.UnlockCallback += LockComplete;
            m_TotalLocks++;
        }
    }

    void LockComplete()
    {
        m_LocksUnlocked++;
        if (m_TotalLocks == m_LocksUnlocked)
            Unlock();
    }

    void Unlock()
    {
        Locked = false;
        m_Iwus.CompletePuzzle();
    }

    public void Setup(InteractingWithUnlocksSomething iwus)
    {
        m_Iwus = iwus;
    }
}
