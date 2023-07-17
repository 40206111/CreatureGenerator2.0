using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarchingCubes 
{
    public static float GridDivisions = 1f; //this should not stay here

    public static MeshData GenerateMeshFromSkeleton(SkeletonPointData data /*this will be full skeleton later*/)
    {


        return null;
    }

    public static  List<Vector3> GenerateGridFromPoint(SkeletonPointData point)
    {
        var pos = point.Position;
        var rad = point.Radius;
        Vector3 startPoint = new Vector3(pos.x - rad, pos.y - rad, pos.z - rad);
        float divisionsPerUnit = 1f / GridDivisions;
        startPoint.x = Mathf.Floor(divisionsPerUnit * startPoint.x) / divisionsPerUnit;
        startPoint.y = Mathf.Floor(divisionsPerUnit * startPoint.y) / divisionsPerUnit;
        startPoint.z = Mathf.Floor(divisionsPerUnit * startPoint.z) / divisionsPerUnit;


        Vector3 endPoint = new Vector3(pos.x + rad, pos.y + rad, pos.z + rad);
        endPoint.x = Mathf.Ceil(divisionsPerUnit * endPoint.x) / divisionsPerUnit;
        endPoint.y = Mathf.Ceil(divisionsPerUnit * endPoint.y) / divisionsPerUnit;
        endPoint.z = Mathf.Ceil(divisionsPerUnit * endPoint.z) / divisionsPerUnit;

        int cubeSideLenX = (int)((endPoint.x - startPoint.x) / GridDivisions);
        int cubeSideLenY = (int)((endPoint.y - startPoint.y) / GridDivisions);
        int cubeSideLenZ = (int)((endPoint.z - startPoint.z) / GridDivisions);
        var output = new List<Vector3>();

        for (int x = 0; x <= cubeSideLenX; ++x)
        {
            for (int y = 0; y <= cubeSideLenY; ++y)
            {
                for (int z = 0; z <= cubeSideLenZ; ++z)
                {
                    var neVec = new Vector3(startPoint.x + GridDivisions * x, startPoint.y + GridDivisions * y, startPoint.z + GridDivisions * z);
                    output.Add(neVec);
                }
            }
        }

        //output.Add(startPoint);
        //output.Add(endPoint);
        return output;
    }

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
        var triangles = new List<int>(MarchingCubesData.Triangles[TrianglesIndex]);
        var vertices = new List<Vector3>();
        FixVertices(ref vertices, ref triangles);
        MeshData output = new MeshData(vertices, triangles);
        return output;
    }

    static void FixVertices(ref List<Vector3> vertices, ref List<int> triangles)
    {
        Dictionary<Vector3, int> outputDic = new Dictionary<Vector3, int>();

        for(int i = 0; i < triangles.Count; ++i)
        {
            var vector = MarchingCubesData.EdgeCentres[triangles[i]];
            if (outputDic.ContainsKey(vector))
            {
                triangles[i] = outputDic[vector];
            }
            else
            {
                vertices.Add(vector);
                outputDic.Add(vector, vertices.Count - 1);
                triangles[i] = outputDic[vector];
            }
        }
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
