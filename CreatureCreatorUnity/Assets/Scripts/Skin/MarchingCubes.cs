using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarchingCubes 
{

    public static MeshData GetMeshDataForCube(CubeData cube)
    {
        int TrianglesIndex = 0;
        TrianglesIndex = cube.Data[0].IsInSphere ? TrianglesIndex | 1 : TrianglesIndex;
        TrianglesIndex = cube.Data[1].IsInSphere ? TrianglesIndex | 2 : TrianglesIndex;
        TrianglesIndex = cube.Data[2].IsInSphere ? TrianglesIndex | 4 : TrianglesIndex;
        TrianglesIndex = cube.Data[3].IsInSphere ? TrianglesIndex | 8 : TrianglesIndex;
        TrianglesIndex = cube.Data[4].IsInSphere ? TrianglesIndex | 16 : TrianglesIndex;
        TrianglesIndex = cube.Data[5].IsInSphere ? TrianglesIndex | 32 : TrianglesIndex;
        TrianglesIndex = cube.Data[6].IsInSphere ? TrianglesIndex | 64 : TrianglesIndex;
        TrianglesIndex = cube.Data[7].IsInSphere ? TrianglesIndex | 128 : TrianglesIndex;
        MeshData output = new MeshData(MarchingCubesData.EdgeCentres.ToList(), MarchingCubesData.Triangles[TrianglesIndex]);
        return output;
    }

    static bool CaseFound(bool[] currentCase, CubeData cube)
    {
        var points = cube.Data;

        for (int i = 0; i < points.Length; ++i)
        {
            if (currentCase[i] != points[i].IsInSphere)
            {
                return false;
            }
        }

        return true;
    }
}
