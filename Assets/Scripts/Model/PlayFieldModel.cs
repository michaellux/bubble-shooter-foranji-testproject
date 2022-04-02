using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct PlayFieldModel
{
    public BubbleField bubbleField;

    public struct BubbleField
    {
        public BubbleRow[] bubbleRows;

        public BubbleField(BubbleRow[] bubbleRows)
        {
            this.bubbleRows = bubbleRows;
        }
    }

    public struct BubbleRow
    {
        public Vector3 position;
        public GoalBubble[] goalBubbles;

        public BubbleRow(Vector3 position, GoalBubble[] goalBubbles)
        {
            this.position = position;
            this.goalBubbles = goalBubbles;
        }
    }

    public PlayFieldModel(BubbleField bubbleField)
    {
        this.bubbleField = bubbleField;
    }
}
