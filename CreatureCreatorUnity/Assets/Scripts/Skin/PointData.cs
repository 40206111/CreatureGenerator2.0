using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PointData
{
    public  Vector3 Position;
    public bool IsInSphere;

    public PointData(Vector3 pos, bool isInSphere)
    {
        Position = pos;
        IsInSphere = isInSphere;
    }
}
