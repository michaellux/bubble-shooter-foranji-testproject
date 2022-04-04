using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public abstract class BubbleModel
{
    public Vector3 position;
    public BubbleTypes type;
    public bool isExists;
    public int movesLeft;

    public int positionInRow;
    public int positionInColumn;
}

