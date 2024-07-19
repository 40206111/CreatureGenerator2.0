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
        var skelePoint = SkeleBones.SkelePoints[0];
        if(skelePoint.Position != transform.position)
        {
            skelePoint.Position = transform.position;
            UpdateMesh();
        }

        if (skelePoint.Radius != Data.Radius)
        {
            skelePoint.Radius = Data.Radius;
            UpdateMesh();
        }

        if(GridDivisions != SkeleBones.TestGetDivisions())
        {
            SkeleBones.TestUpdateGridDivisions(GridDivisions);
            UpdateMesh();
        }
        SkeleBones.SkelePoints[0] = skelePoint;
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


        Gizmos.color = Color.red;
        Vector3 endPosX = transform.position;
        endPosX.x -= Data.Radius;
        Gizmos.DrawLine(transform.position, endPosX);
        //Y
        Gizmos.color = Color.green;
        Vector3 endPosY = transform.position;
        endPosY.y -= Data.Radius;
        Gizmos.DrawLine(transform.position, endPosY);
        //Z
        Gizmos.color = Color.blue;
        Vector3 endPosZ = transform.position;
        endPosZ.z -= Data.Radius;
        Gizmos.DrawLine(transform.position, endPosZ);

        Gizmos.color = Color.grey;
        float diameter = Data.Radius * 2f;
        Gizmos.DrawWireCube(transform.position, new Vector3(diameter, diameter, diameter));

        Gizmos.DrawWireSphere(transform.position, Data.Radius);
    }
}
