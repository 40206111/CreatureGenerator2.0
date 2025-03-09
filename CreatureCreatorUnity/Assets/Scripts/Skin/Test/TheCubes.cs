using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component is used to test that the marching cube combinations are working as expected
/// </summary>

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TheCubes : MonoBehaviour
{
    protected CubeData TestCube = new CubeData(new PointData[]
    {
        new PointData(new Vector3(0, 0, 1), false), //0
        new PointData(new Vector3(1, 0, 1), false), //1
        new PointData(new Vector3(1, 0, 0), false), //2
        new PointData(new Vector3(0, 0, 0), false), //3
        new PointData(new Vector3(0, 1, 1), false), //4
        new PointData(new Vector3(1, 1, 1), false), //5
        new PointData(new Vector3(1, 1, 0), false), //6
        new PointData(new Vector3(0, 1, 0), false)  //7
    });

    [SerializeField, Range(0, 14)] protected int CurrentCube = 0;
    protected int LastCube = 0;

    Mesh TheMesh;

    protected MarchingCubes CubeMaker = new MarchingCubes();

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

    protected virtual void UpdateCube()
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
        var meshData = CubeMaker.GetMeshDataForCube(TestCube);
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


    protected virtual void OnDrawGizmos()
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
            Gizmos.color = point.IsInSphere && i == 5 ? Color.cyan : Gizmos.color;
            Gizmos.color = !point.IsInSphere && i == 5 ? Color.gray : Gizmos.color;
            Gizmos.DrawSphere(transform.position + point.Position, 0.1f);
        }

        var verts = TestCube.GetTriangleVerts();
        for (int i = 0; i < verts.Count; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + verts[i], 0.1f);
        }
    }
}
