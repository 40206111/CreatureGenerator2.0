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
}
