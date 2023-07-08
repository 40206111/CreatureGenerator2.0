using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubesGrid
{
    public static List<Vector3> TheGrid;

    public static void CreateGrid(Vector3 midPoint, Vector3 Size, float divisions)
    {
        TheGrid = new List<Vector3>();
        Vector3 startPoint = new Vector3(midPoint.x - Size.x * 0.5f, midPoint.y - Size.y * 0.5f, midPoint.z - Size.z * 0.5f);


        for (float x = startPoint.x; x < startPoint.x + Size.x; x += divisions)
        {
            for (float y = startPoint.y; y < startPoint.y + Size.y; y += divisions)
            {
                for (float z = startPoint.z; z < startPoint.z + Size.z; z += divisions)
                {
                    TheGrid.Add(new Vector3(x, y, z));
                }
            }
        }

    }


}
