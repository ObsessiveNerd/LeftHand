using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bookshelf : Escape, IPuzzle
{
    public List<Book> m_Books = new List<Book>();
    InteractingWithUnlocksSomething m_Iwus;
    int m_CurrentNumber = 0;

    public void Setup(InteractingWithUnlocksSomething iwus)
    {
        m_Iwus = iwus;
    }

    public void SubmitBook(Book book)
    {
        if(m_CurrentNumber != book.OrderNumber)
        {
            UIFactory.CreateDialogue("Hmmm... something seems wrong.", true, () => UIFactory.UIEnabled(true));
            m_CurrentNumber = 0;
        }
        else
        {
            m_CurrentNumber++;
            if (m_CurrentNumber == m_Books.Count)
                UIFactory.CreateDialogue("That appears to have done something.", true, () =>
                {
                    m_Iwus.CompletePuzzle();
                });
        }
    }

    protected override void Cleanup()
    {
        m_CurrentNumber = 0;
        gameObject.SetActive(false);
    }
}
