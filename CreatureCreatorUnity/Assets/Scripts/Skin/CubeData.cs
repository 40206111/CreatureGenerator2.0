using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeData 
{
    public static Vector3[] NormalisedCube = new Vector3[8]
    {
        new Vector3(0, 0, 0),
        new Vector3(1, 0, 0),
        new Vector3(0, 0, 1),
        new Vector3(1, 0, 1),
        new Vector3(0, 1, 0),
        new Vector3(1, 1, 0),
        new Vector3(0, 1, 1),
        new Vector3(1, 1, 1)
    };

    public PointData[] Data = new PointData[8];
    
    public int InSpherePoints
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < Data.Length; i++)
            {
                counter = Data[i].IsInSphere ? counter++ : counter;
            }
            return counter;
        }
    }

    public CubeData(PointData[] pointData)
    {
        Data = pointData;
    }
}
