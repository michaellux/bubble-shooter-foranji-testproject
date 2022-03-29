using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : Obstacle
{

    void Start()
    {
        createPlane(gameObject.transform.right, gameObject.transform.position);
    }

    
    void Update()
    {
        
    }
}
