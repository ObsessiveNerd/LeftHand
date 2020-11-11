using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    [Serializable]
    public struct ChessSquare
    {
        public string Alpha;
        public string Numeric;
        public Vector2 Position;
        
        public ChessSquare(string alpha, int numeric, Vector2 position)
        {
            Alpha = alpha;
            Numeric = numeric.ToString();
            Position = position;
        }

        public static bool operator==(ChessSquare lhs, ChessSquare rhs)
        {
            return lhs.Alpha == rhs.Alpha && lhs.Numeric == rhs.Numeric;
        }

        public static bool operator!=(ChessSquare lhs, ChessSquare rhs)
        {
            return lhs.Alpha != rhs.Alpha || lhs.Numeric != rhs.Numeric;
        }
    }

    [Serializable]
    public struct ChessMove
    {
        public ChessSquare Square;
        public ChessPiece Piece;
    }

    public ChessMove WinningMove;

    InteractingWithUnlocksSomething m_InteractionObject;
    Dictionary<ChessSquare, ChessPiece> m_PieceArrangement = new Dictionary<ChessSquare, ChessPiece>();
    Dictionary<Vector2, ChessSquare> m_LocationToSquareMap = new Dictionary<Vector2, ChessSquare>();
    RectTransform m_Rect;
    float startPosSpacing = 45f;
    float spawnSpacing = 29f;

    void Awake()
    {
        m_Rect = GetComponent<RectTransform>();

        float startX = m_Rect.transform.position.x - Mathf.Abs(m_Rect.rect.x) + startPosSpacing;
        float startY = m_Rect.transform.position.y - Mathf.Abs(m_Rect.rect.y) + startPosSpacing;

        float maxX = Mathf.Abs(m_Rect.rect.x) + m_Rect.transform.position.x;
        float maxY = Mathf.Abs(m_Rect.rect.y) + m_Rect.transform.position.y;

        float xSpacing = Mathf.Abs(m_Rect.rect.x / 8) + spawnSpacing;
        float ySpacing = Mathf.Abs(m_Rect.rect.y / 8) + spawnSpacing;

        int baseXIndex = 0;
        int baseYIndex = 0;
        for(float i = startX; i < maxX; i += xSpacing)
        {
            for (float k = startY; k < maxY; k += ySpacing)
            {
                var position = new Vector2(i, k);
                var square = new ChessSquare(ConvertNumberToAlpha(baseXIndex), (baseYIndex + 1), position);
                m_PieceArrangement.Add(square, null);
                m_LocationToSquareMap.Add(position, square);
                baseYIndex++;
            }
            baseXIndex++;
            baseYIndex = 0;
        }
    }

    public void Setup(InteractingWithUnlocksSomething interactionObject)
    {
        m_InteractionObject = interactionObject;
    }

    public bool MovePiece(ChessPiece piece, Vector2 dropLocation)
    {
        ChessSquare nearestSquare = FindNearest(dropLocation);
        piece.transform.position = nearestSquare.Position;

        if (WinningMove.Piece == piece && WinningMove.Square == nearestSquare)
        {
            Debug.Log("Winning move");
            m_InteractionObject?.CompletePuzzle();
            return true;
        }
        Debug.Log("Bad move");
        return false;
    }

    ChessSquare FindNearest(Vector2 position)
    {
        ChessSquare nearest = default(ChessSquare);
        foreach(var key in m_LocationToSquareMap.Keys)
        {
            var square = m_LocationToSquareMap[key];
            if (Vector2.Distance(position, key) < Vector2.Distance(position, nearest.Position))
                nearest = square;
        }

        return nearest;
    }

    string ConvertNumberToAlpha(float alpha)
    {
        int a = (int)alpha;
        switch(a)
        {
            case 0:
                return "a";
            case 1:
                return "b";
            case 2:
                return "c";
            case 3:
                return "d";
            case 4:
                return "e";
            case 5:
                return "f";
            case 6:
                return "g";
            case 7:
                return "h";
        }
        return "";
    }
}
