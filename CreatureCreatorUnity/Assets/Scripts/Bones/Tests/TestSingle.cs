using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place on gameobject to test a single point with the marching cubes stuff
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TestSingle : MonoBehaviour
{
    [SerializeField]
    float Radius;
    [SerializeField]
    float GridDivisions;

    Skeleton SkeleBones = new Skeleton(new SkeletonPointData());

    Mesh TheMesh;

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        TheMesh.name = "Skin";
        UpdateMesh();
    }

    void Update()
    {
        var skelePoint = SkeleBones.SkelePoints[0];
        if(skelePoint.Position != transform.position)
        {
            skelePoint.Position = transform.position;
            UpdateMesh();
        }

        if (skelePoint.Radius != Radius)
        {
            skelePoint.Radius = Radius;
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
        endPosX.x -= Radius;
        Gizmos.DrawLine(transform.position, endPosX);
        //Y
        Gizmos.color = Color.green;
        Vector3 endPosY = transform.position;
        endPosY.y -= Radius;
        Gizmos.DrawLine(transform.position, endPosY);
        //Z
        Gizmos.color = Color.blue;
        Vector3 endPosZ = transform.position;
        endPosZ.z -= Radius;
        Gizmos.DrawLine(transform.position, endPosZ);

        Gizmos.color = Color.grey;
        float diameter = Radius * 2f;
        Gizmos.DrawWireCube(transform.position, new Vector3(diameter, diameter, diameter));

        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
