using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct PlayField
{
    [SerializeField]
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
        public GoalBubble[] goalBubbles;

        public BubbleRow(GoalBubble[] goalBubbles)
        {
            this.goalBubbles = goalBubbles;
        }
    }

    public enum GoalBubbleType
    {
        RED, GREEN, YELLOW, BLUE
    };

    public struct GoalBubble
    {
        public Vector3 position;
        public GoalBubbleType type;
        public bool isExists;

        public GoalBubble(Vector3 position, GoalBubbleType type, bool isExists)
        {
            this.position = position;
            this.type = type;
            this.isExists = isExists;
        }
    }

    public PlayField(BubbleField bubbleField)
    {
        this.bubbleField = bubbleField;
    }


}
