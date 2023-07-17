using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//place on gameobject to test a single point with the marching cubes stuff
public class TestSingle : MonoBehaviour
{
    [SerializeField]
    float Radius;
    [SerializeField]
    float GridDivisions;

    SkeletonPointData SkelePoint = new SkeletonPointData();

    List<Vector3> GridPoints = new List<Vector3>(); //temp

    void Update()
    {
        if(SkelePoint.Position != transform.position)
        {
            SkelePoint.Position = transform.position;
            UpdateMesh();
        }

        if (SkelePoint.Radius != Radius)
        {
            SkelePoint.Radius = Radius;
            UpdateMesh();
        }

        if(GridDivisions != MarchingCubes.GridDivisions)
        {
            MarchingCubes.GridDivisions = GridDivisions;
            UpdateMesh();
        }
    }

    void UpdateMesh()
    {
        GridPoints = MarchingCubes.GenerateGridFromPoint(SkelePoint);
    }

    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Update();
        }

        for (int i = 0; i < GridPoints.Count; ++i)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(GridPoints[i], 0.1f);
        }

        //X
        Gizmos.color = Color.red;
        Vector3 endPosX = SkelePoint.Position;
        endPosX.x -= Radius;
        Gizmos.DrawLine(SkelePoint.Position, endPosX);
        //Y
        Gizmos.color = Color.green;
        Vector3 endPosY = SkelePoint.Position;
        endPosY.y -= Radius;
        Gizmos.DrawLine(SkelePoint.Position, endPosY);
        //Z
        Gizmos.color = Color.blue;
        Vector3 endPosZ = SkelePoint.Position;
        endPosZ.z -= Radius;
        Gizmos.DrawLine(SkelePoint.Position, endPosZ);

        Gizmos.color = Color.grey;
        float diameter = Radius * 2f;
        Gizmos.DrawWireCube(SkelePoint.Position, new Vector3(diameter, diameter, diameter));

        Gizmos.DrawWireSphere(SkelePoint.Position, Radius);
    }
}
