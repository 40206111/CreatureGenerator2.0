using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// place on gameobject to test a single point with the marching cubes stuff
/// </summary>
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(TestSingleData))]
public class TestSingle : MonoBehaviour
{
    [SerializeField]
    float GridDivisions;

    TestSingleData Data;

    Skeleton SkeleBones = new Skeleton(new SkeletonPointData());

    Mesh TheMesh;

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        Data = GetComponent<TestSingleData>();
        TheMesh.name = "Skin";
        UpdateMesh();
    }

    void Update()
    {
        if (Data == null)
        {
            Awake();
        }
        Data.MyUpdate();

        if(Data.CauseUpdate)
        {
            SkeleBones.SkelePoints[0] = Data.Data;
            UpdateMesh();
        }

        if(GridDivisions != SkeleBones.TestGetDivisions())
        {
            SkeleBones.TestUpdateGridDivisions(GridDivisions);
            UpdateMesh();
        }
    }

    void UpdateMesh()
    {
        Debug.Log("Updating Mesh");
        SkeleBones.MakeMyMesh();
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        var meshData = SkeleBones.SkeleMesh;
        if (meshData.Vertices.Count > 0)
        {
            TheMesh.vertices = new Vector3[meshData.Vertices.Count];
            TheMesh.vertices = meshData.Vertices.ToArray();
            TheMesh.triangles = new int[meshData.Triangles.Count];
            TheMesh.triangles = meshData.Triangles.ToArray();
        }
        TheMesh.RecalculateNormals();
        TheMesh.name = $"SkinTestSingle";
    }

    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Update();
        }
    }
}
