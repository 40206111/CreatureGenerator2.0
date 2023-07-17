using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData
{
    public List<Vector3> Vertices;
    public List<int> Triangles;

    public MeshData()
    {
        Vertices = new List<Vector3>();
        Triangles = new List<int>();
    }

    public MeshData(List<Vector3> vertices, List<int> triangles)
    {
        Vertices = vertices;
        Triangles = triangles;
    }
}
