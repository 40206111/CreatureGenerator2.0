using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This represents a single metaball within a skeleton
/// I'm thinking this could probably be changed to not necisarily be a ball
/// for better shaping
/// </summary>
public class SkeletonPointData
{
    public Vector3 Position;
    public float Radius;
    //public List<SkeletonPointData> Neigbours; expect to use this for full body gen 

    public SkeletonPointData() { }
    public SkeletonPointData(Vector3 pos, float rad)
    {
        Position = pos;
        Radius = rad;
    }
}
