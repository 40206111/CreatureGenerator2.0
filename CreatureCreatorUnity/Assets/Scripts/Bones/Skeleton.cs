using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is a collection of all the points that make a creature
/// It can be used to generate the skeletons skin
/// </summary>
public class Skeleton
{
    public List<SkeletonPointData> SkelePoints;
    public MeshData SkeleMesh;
    MarchingCubes CubeMaker;

    public Skeleton(SkeletonPointData skelePoint)
    {
        SkelePoints = new List<SkeletonPointData>();
        SkelePoints.Add(skelePoint);

        SkeleMesh = new MeshData();
    }

    public void MakeMyMesh()
    {
        if (CubeMaker == null)
        {
            CubeMaker = new MarchingCubes();
        }
        SkeleMesh.Triangles.Clear();
        SkeleMesh.Vertices.Clear();
        SkeleMesh = CubeMaker.GenerateMeshFromSkeleton(this);
    }

    public void TestUpdateGridDivisions(float divisions)
    {
        if (CubeMaker == null)
        {
            CubeMaker = new MarchingCubes();
        }
        CubeMaker.GridDivisions = divisions;
    }

    public float TestGetDivisions()
    {
        if (CubeMaker == null)
        {
            return 0;
        }
        return CubeMaker.GridDivisions;
    }

}
