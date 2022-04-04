using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class BubbleData : ScriptableObject
{
    [SerializeField] private Vector3 position;
    [SerializeField] private BubbleTypes type;
    [SerializeField] private bool isExists;
    [SerializeField] private int movesLeft;
    [SerializeField] private int positionInRow;
    [SerializeField] private int positionInColumn;

    public BubbleModel bubbleModel;

    public void SetBubbleModel(BubbleModel bubbleModel)
    {
        this.bubbleModel = bubbleModel;

        SetPosition(this.bubbleModel.position);
        SetBubbleType(this.bubbleModel.type);
        SetIsExists(this.bubbleModel.isExists);
        SetMovesLeft(this.bubbleModel.movesLeft);
        SetPositionInRow(this.bubbleModel.positionInRow);
        SetPositionInColumn(this.bubbleModel.positionInColumn);
    }

    public BubbleModel GetBubbleModel()
    {
        return this.bubbleModel;
    }

    public Vector3 GetPosition()
    {
        return this.bubbleModel.position;
    }
    public BubbleTypes GetBubbleType()
    {
        return this.bubbleModel.type;
    }
    public bool GetIsExists()
    {
        return this.bubbleModel.isExists;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
        this.bubbleModel.position = position;
    }

    public void SetBubbleType(BubbleTypes type)
    {
        this.type = type;
        this.bubbleModel.type = type;
    }

    public void SetIsExists(bool isExists)
    {
        this.isExists = isExists;
        this.bubbleModel.isExists = isExists;
    }

    public void SetMovesLeft(int movesLeft)
    {
        this.movesLeft = movesLeft;
        this.bubbleModel.movesLeft = movesLeft;
    }

    public void SetPositionInRow(int positionInRow)
    {
        this.positionInRow = positionInRow;
        this.bubbleModel.positionInRow = positionInRow;
    }

    public void SetPositionInColumn(int positionInColumn)
    {
        this.positionInColumn = positionInColumn;
        this.bubbleModel.positionInColumn = positionInColumn;
    }
}
