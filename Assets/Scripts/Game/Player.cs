using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int possibleMoves = 5;

    public static Player instance = null;

    void Start()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            //GameManager.instance.SetNewBallOnPlayfield();
        }
        #endregion
    }

    public int GetPossibleMoves()
    {
        return possibleMoves;
    }
}

