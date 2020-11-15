using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public int OrderNumber;

    Bookshelf m_Bookshelf;

    void Start()
    {
        m_Bookshelf = GetComponentInParent<Bookshelf>();    
    }

    public void SubmitToBookselfPuzzle()
    {
        m_Bookshelf.SubmitBook(this);
    }
}
