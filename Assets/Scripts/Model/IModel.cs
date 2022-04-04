using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
interface IModel
{
    [SerializeField] abstract public Vector3 position { get; set; }
    [SerializeField] abstract public BubbleTypes type { get; set; }
    [SerializeField] abstract public bool isExists { get; set; }
    [SerializeField] abstract public int movesLeft { get; set; }

    [SerializeField] abstract public int positionInRow { get; set; }
    [SerializeField] abstract public int positionInColumn { get; set; }
}

