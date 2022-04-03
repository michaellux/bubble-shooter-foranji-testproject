using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public  class BubbleData : ScriptableObject
{
    [SerializeField] private Vector3 position;
    [SerializeField] private BubbleTypes type;
    [SerializeField] private bool isExists;

    public Vector3 GetPosition()
    {
        return position;
    }
    public BubbleTypes GetBubbleType()
    {
        return type;
    }
    public bool GetIsExists()
    {
        return isExists;
    }

    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

    public void SetBubbleType(BubbleTypes type)
    {
        this.type = type;
    }

    public void SetIsExists(bool isExists)
    {
        this.isExists = isExists;
    }
}
