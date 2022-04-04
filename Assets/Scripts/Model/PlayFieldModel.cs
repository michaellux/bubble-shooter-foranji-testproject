using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct PlayFieldModel
{
    public BubbleFieldModel bubbleField;

    public struct BubbleFieldModel
    {
        public BubbleRowModel[] bubbleRows;

        public BubbleFieldModel(BubbleRowModel[] bubbleRows)
        {
            this.bubbleRows = bubbleRows;
        }
    }

    public struct BubbleRowModel
    {
        public Vector3 position;
        public GoalBubbleModel[] goalBubbles;

        public BubbleRowModel(Vector3 position, GoalBubbleModel[] goalBubbles)
        {
            this.position = position;
            this.goalBubbles = goalBubbles;
        }
    }

    public PlayFieldModel(BubbleFieldModel bubbleField)
    {
        this.bubbleField = bubbleField;
    }
}
