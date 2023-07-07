using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData 
{
    public PointData[] Data = new PointData[8]; 

    public CubeData(PointData[] pointData)
    {
        Data = pointData;
    }

    private List<Vector3> TriangleVerts = new List<Vector3>();

    public List<Vector3> GetTriangleVerts()
    {
        TriangleVerts.Clear();
        var a = Data[0];
        var b = Data[3];
        CheckIfABShouldBeInList(a, b); //1
        a = Data[0];
        b = Data[4];
        CheckIfABShouldBeInList(a, b); //2
        a = Data[0];
        b = Data[1];
        CheckIfABShouldBeInList(a, b); //3
        a = Data[1];
        b = Data[5];
        CheckIfABShouldBeInList(a, b); //4
        a = Data[1];
        b = Data[2];
        CheckIfABShouldBeInList(a, b); //5
        a = Data[2];
        b = Data[3];
        CheckIfABShouldBeInList(a, b); //6
        a = Data[2];
        b = Data[6];
        CheckIfABShouldBeInList(a, b); //7
        a = Data[3];
        b = Data[7];
        CheckIfABShouldBeInList(a, b); //8
        a = Data[7];
        b = Data[4];
        CheckIfABShouldBeInList(a, b); //9
        a = Data[7];
        b = Data[6];
        CheckIfABShouldBeInList(a, b); //10
        a = Data[5];
        b = Data[4];
        CheckIfABShouldBeInList(a, b); //11
        a = Data[5];
        b = Data[6];
        CheckIfABShouldBeInList(a, b); //12
        return TriangleVerts;
    }

    void CheckIfABShouldBeInList(PointData a, PointData b)
    {
        if (a.IsInSphere && !b.IsInSphere || b.IsInSphere && !a.IsInSphere)
        {
            var dirVector = b.Position - a.Position;
            TriangleVerts.Add(a.Position + (0.5f * dirVector));
        }
    }
}
