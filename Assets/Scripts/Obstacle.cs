using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
abstract public class Obstacle : MonoBehaviour
{
    public Plane Plane
    {
        get;
        private set;
    }

    public void createPlane(Vector3 inNormal, Vector3 inPoint)
    {
        Plane = new Plane(new Vector3(inNormal.x, inNormal.y), inPoint);
    }
}
