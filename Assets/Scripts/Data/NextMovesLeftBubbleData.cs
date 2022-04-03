using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class NextMovesLeftBubbleData : BubbleData
{
    [SerializeField] private int leftMoves;

    public int GetLeftMoves()
    {
        return leftMoves;
    }

    public void SetLeftMoves(int leftMoves)
    {
        this.leftMoves = leftMoves;
    }
}
