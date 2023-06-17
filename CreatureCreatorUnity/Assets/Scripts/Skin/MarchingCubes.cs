using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes 
{

    public static MeshData GetMeshDataForCube(CubeData cube)
    {
        for (int i = 0; i < MarchingCubesData.Cases.Length; ++i)
        {
            var currentCase = MarchingCubesData.Cases[i];
            if (CaseFound(currentCase, cube))
            {
                return MarchingCubesData.CaseMeshData[i];
            }
        }

        Debug.LogError("[MarchingCubes.cs] No Pattern match for cube");
        return MarchingCubesData.CaseMeshData[0];
    }

    static bool CaseFound(bool[] currentCase, CubeData cube)
    {
        if (InSpherePoints(currentCase) != cube.InSpherePoints)
        {
            return false;
        }

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

    static int InSpherePoints(bool[] currentcase)
    {
        int counter = 0;
        for (int i = 0; i < currentcase.Length; i++)
        {
            counter = currentcase[i] ? counter++ : counter;
        }
        return counter;
    }
}
