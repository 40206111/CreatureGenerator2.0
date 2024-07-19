using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// a point within our grid
/// </summary>
public struct PointData
{
    public  Vector3 Position;
    public bool IsInSphere; //weather or not this point is within our mesh

    public PointData(Vector3 pos, bool isInSphere)
    {
        Position = pos;
        IsInSphere = isInSphere;
    }
}
