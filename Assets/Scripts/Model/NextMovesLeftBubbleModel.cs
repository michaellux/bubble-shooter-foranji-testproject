﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NextMovesLeftBubbleModel : BubbleModel
{
    public NextMovesLeftBubbleModel(BubbleTypes type, int movesLeft)
    {
        this.type = type;
        this.movesLeft = movesLeft;
    }
}
