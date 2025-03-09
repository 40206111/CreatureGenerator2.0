using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class SkeletonGenerator : MonoBehaviour
{
    Skeleton MySkeleton;
    Mesh TheMesh;

    void MakeSkeleton()
    {
        MySkeleton = new Skeleton();

        var head = new SkeletonPointData()
        {
            Position = new Vector3(0, 0, 0),
            Radius = 5
        };
    }

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        TheMesh.name = "Skin";
    }

    void UpdateMesh()
    {
        Debug.Log("Updating Mesh");
        MySkeleton.MakeMyMesh();
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        var meshData = MySkeleton.SkeleMesh;
        if (meshData.Vertices.Count > 0)
        {
            TheMesh.vertices = new Vector3[meshData.Vertices.Count];
            TheMesh.vertices = meshData.Vertices.ToArray();
            TheMesh.triangles = new int[meshData.Triangles.Count];
            TheMesh.triangles = meshData.Triangles.ToArray();
        }
        TheMesh.RecalculateNormals();
        TheMesh.name = $"SkinTestSkeleton";
    }
}