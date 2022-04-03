using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class GoalBubbleModel : BubbleModel, IModel
{
    public GoalBubbleModel(Vector3 position, BubbleTypes type, bool isExists)
    {
        this.position = position;
        this.type = type;
        this.isExists = isExists;
    }
}

