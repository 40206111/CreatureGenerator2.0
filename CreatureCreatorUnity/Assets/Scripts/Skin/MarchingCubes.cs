using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes 
{

    public static MeshData GetMeshDataForCube(CubeData cube)
    {
        return null;
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
