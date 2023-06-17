using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TheCubes : MonoBehaviour
{
    static readonly CubeData TestCube = new CubeData(new PointData[]
    {
        new PointData(CubeData.NormalisedCube[0], false),
        new PointData(CubeData.NormalisedCube[1], false),
        new PointData(CubeData.NormalisedCube[2], false),
        new PointData(CubeData.NormalisedCube[3], false),
        new PointData(CubeData.NormalisedCube[4], false),
        new PointData(CubeData.NormalisedCube[5], false),
        new PointData(CubeData.NormalisedCube[6], false),
        new PointData(CubeData.NormalisedCube[7], false)
    });

    [SerializeField, Range(0, 14)] int CurrentCube = 0;
    int LastCube = 0;

    Mesh TheMesh;

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        TheMesh.name = "Skin";
        UpdateMesh();
    }

    private void Update()
    {
        if (CurrentCube != LastCube)
        {
            UpdateCube();
            UpdateMesh();
        }
    }

    void UpdateCube()
    {
        LastCube = CurrentCube;
        var thisCase = MarchingCubesData.Cases[LastCube];

        for (int i = 0; i < TestCube.Data.Length; ++i)
        {
            TestCube.Data[i].IsInSphere = thisCase[i];
        }
    }

    void UpdateMesh()
    {
        var meshData = MarchingCubes.GetMeshDataForCube(TestCube);
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        if (meshData.Vertices.Count > 0)
        {
            TheMesh.vertices = new Vector3[meshData.Vertices.Count];
            TheMesh.vertices = meshData.Vertices.ToArray();
            TheMesh.triangles = new int[meshData.Triangles.Count];
            TheMesh.triangles = meshData.Triangles.ToArray();
        }
        TheMesh.RecalculateNormals();
        TheMesh.name = $"Skin{CurrentCube}";
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (CurrentCube != LastCube)
            {
                UpdateCube();
                UpdateMesh();
            }
        }
        for (int i = 0; i < TestCube.Data.Length; ++i)
        {
            var point = TestCube.Data[i];
            Gizmos.color = point.IsInSphere ? Color.green : Color.black;
            Gizmos.DrawSphere(point.Position, 0.1f);
        }
    }
}
