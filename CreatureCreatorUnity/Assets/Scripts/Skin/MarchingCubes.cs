using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;

public class MarchingCubes
{
    public float GridDivisions = 1f; //this should not stay here

    public MeshData GenerateMeshFromSkeleton(Skeleton data)
    {
        var meshData = new MeshData();
        for (int i = 0; i < data.SkelePoints.Count; ++i)
        {
            var newData = GenerateGridFromPoint(data.SkelePoints[i]);
            int curVertLen = Mathf.Max(meshData.Vertices.Count - 1, 0);
            if (curVertLen == 0)
            {
                meshData.Triangles.AddRange(newData.Triangles);
            }
            else
            {
                for (int j = 0; j < newData.Triangles.Count; ++j)
                {
                    meshData.Triangles.Add(curVertLen + newData.Triangles[j]);
                }
            }
            meshData.Vertices.AddRange(newData.Vertices);
        }
        //FixVertices(ref meshData.Vertices, ref meshData.Triangles);
        return meshData;
    }

    public MeshData GenerateGridFromPoint(SkeletonPointData point)
    {
        if (TimDebugsThings.Instance != null)
        {
            TimDebugsThings.Instance.ClearVisuals();
        }
        Vector3 pos = point.Position;
        float rad = point.Radius;
        Vector3 startPoint = new Vector3(pos.x - rad, pos.y - rad, pos.z - rad);
        float divisionsPerUnit = 1f / GridDivisions;
        Vector3 gridVec = new Vector3(GridDivisions, GridDivisions, GridDivisions);
        startPoint.x = Mathf.Floor(divisionsPerUnit * startPoint.x) / divisionsPerUnit;
        startPoint.y = Mathf.Floor(divisionsPerUnit * startPoint.y) / divisionsPerUnit;
        startPoint.z = Mathf.Floor(divisionsPerUnit * startPoint.z) / divisionsPerUnit;

        startPoint -= gridVec;

        Vector3 endPoint = new Vector3(pos.x + rad, pos.y + rad, pos.z + rad);
        endPoint.x = Mathf.Ceil(divisionsPerUnit * endPoint.x) / divisionsPerUnit;
        endPoint.y = Mathf.Ceil(divisionsPerUnit * endPoint.y) / divisionsPerUnit;
        endPoint.z = Mathf.Ceil(divisionsPerUnit * endPoint.z) / divisionsPerUnit;

        endPoint += gridVec;
        if (TimDebugsThings.Instance != null)
        {
            TimDebugsThings.Instance.VisualStartEndPoint(startPoint, endPoint);
        }

        int cubeSideLenX = (int)((endPoint.x - startPoint.x) / GridDivisions);
        int cubeSideLenY = (int)((endPoint.y - startPoint.y) / GridDivisions);
        int cubeSideLenZ = (int)((endPoint.z - startPoint.z) / GridDivisions);

        var meshData = new MeshData();

        for (int x = 0; x < cubeSideLenX; ++x)
        {
            for (int y = 0; y < cubeSideLenY; ++y)
            {
                for (int z = 0; z < cubeSideLenZ; ++z)
                {
                    Vector3 cubeOrigin = new Vector3(x, y, z) * GridDivisions;
                    cubeOrigin = startPoint + cubeOrigin;
                    float halfDiv = GridDivisions * 0.5f;
                    float multX = x >= cubeSideLenX * 0.5f ? 0 : 1;
                    float multY = y >= cubeSideLenY * 0.5f ? 0 : 1;
                    float multZ = z >= cubeSideLenZ * 0.5f ? 0 : 1;
                    Vector3 toCentreVector = new Vector3(GridDivisions * multX, GridDivisions * multY, GridDivisions * multZ);
                    Vector3 awayFromCentreVector = new Vector3(GridDivisions, GridDivisions, GridDivisions) - toCentreVector;
                    Vector3 CubeCenter = cubeOrigin + toCentreVector;
                    Vector3 CubeOuter = cubeOrigin + awayFromCentreVector;
                    float distIn = (CubeCenter - point.Position).magnitude;
                    float distOut = (CubeOuter - point.Position).magnitude;
                    //if (TimDebugsThings.Instance != null)
                    //    TimDebugsThings.Instance?.AddVisualCubeCenters(CubeCenter);
                    if (distOut < rad * 0.9f)
                    {
                        //z = Mathf.FloorToInt(z + (cubeSideLenZ * 0.75f));
                        continue;
                    }
                    if (distIn > rad * 1.1f)
                    {
                        continue;
                    }
                    //Task.Run(() => MakeGridCube(startPoint, point, data));
                    var newData = MakeGridCube(cubeOrigin, point);
                    int curVertLen = meshData.Vertices.Count;
                    AddTriangles(ref meshData, newData.Triangles, newData.Vertices);
                }
            }
        }

        return meshData;
    }

    MeshData MakeGridCube(Vector3 startingPoint, SkeletonPointData point)
    {
        float x = startingPoint.x;
        float y = startingPoint.y;
        float z = startingPoint.z;
        PointData[] data = new PointData[]
        {
        new PointData(new Vector3(x, y, z + GridDivisions), false), //0
        new PointData(new Vector3(x + GridDivisions, y, z + GridDivisions), false), //1
        new PointData(new Vector3(x + GridDivisions, y, z), false), //2
        new PointData(startingPoint, false), //3
        new PointData(new Vector3(x, y + GridDivisions, z + GridDivisions), false), //4
        new PointData(new Vector3(x + GridDivisions, y + GridDivisions, z + GridDivisions), false), //5
        new PointData(new Vector3(x + GridDivisions, y + GridDivisions, z), false), //6
        new PointData(new Vector3(x, y + GridDivisions, z), false)  //7
        };

        for (int i = 0; i < data.Length; ++i)
        {
            data[i].IsInSphere = (data[i].Position - point.Position).magnitude + GridDivisions * 0.1f <= point.Radius;
        }

        CubeData cube = new CubeData(data);
        if (TimDebugsThings.Instance != null)
        {
            TimDebugsThings.Instance.VisualUpdateCube(cube);
        }
        return GetMeshDataForCube(cube);
    }

    public MeshData GetMeshDataForCube(CubeData cube)
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
        var vertices = GetVerticesModifiedByPosition(cube);
        //FixVertices(ref vertices, ref triangles);
        MeshData output = new MeshData(vertices, triangles);
        return output;
    }

    List<Vector3> GetVerticesModifiedByPosition(CubeData cube)
    {
        List<Vector3> output = new List<Vector3>();
        for (int i = 0; i < MarchingCubesData.EdgeCentres.Length; ++i)
        {
            var vertex = MarchingCubesData.EdgeCentres[i];
            vertex *= GridDivisions;
            vertex += cube.Data[3].Position;
            output.Add(vertex);
        }
        return output;
    }

    public void AddTriangles(ref MeshData data, List<int> triangles, List<Vector3> vertices)
    {
        for (int i = 0; i < triangles.Count; ++i)
        {
            var vector = vertices[triangles[i]];
            if (!data.Vertices.Contains(vector))
            {
                data.Vertices.Add(vector);
            }
            data.Triangles.Add(data.Vertices.IndexOf(vector));
        }
    }

    public void FixVertices(ref List<Vector3> vertices, ref List<int> triangles)
    {
        Dictionary<Vector3, int> outputDic = new Dictionary<Vector3, int>();

        for (int i = 0; i < triangles.Count; ++i)
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
}
