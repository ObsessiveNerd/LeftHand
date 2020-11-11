using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChessPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 m_LastPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (FindObjectOfType<ChessBoard>().MovePiece(this, Input.mousePosition))
            m_LastPosition = transform.position;
        else
            transform.position = m_LastPosition;
    }

    void Start()
    {
        FindObjectOfType<ChessBoard>().MovePiece(this, transform.position);
        m_LastPosition = transform.position;
    }
}
