using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCubes : MonoBehaviour
{
    static PointData[] TestCube = new PointData[8]
    {
        new PointData(new Vector3(0, 0, 0), false),
        new PointData(new Vector3(1, 0, 0), false),
        new PointData(new Vector3(0, 0, 1), false),
        new PointData(new Vector3(1, 0, 1), false),
        new PointData(new Vector3(0, 1, 0), false),
        new PointData(new Vector3(1, 1, 0), false),
        new PointData(new Vector3(0, 1, 1), false),
        new PointData(new Vector3(1, 1, 1), false)
    };

    static bool[][] Cases = new bool[15][]
    {
        new bool[8]
        {
            false, //A
            false, //B
            false, //C
            false, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            false, //C
            false, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            true, //B
            false, //C
            false, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            false, //C
            false, //D
            false, //E
            true, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            false, //A
            true, //B
            false, //C
            true, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            true, //B
            true, //C
            true, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            false, //A
            true, //B
            false, //C
            true, //D
            true, //E
            false, //F
            false, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            false, //C
            true, //D
            false, //E
            true, //F
            true, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            true, //C
            true, //D
            false, //E
            false, //F
            true, //G
            false, //H
        },
        new bool[8]
        {
            false, //A
            true, //B
            true, //C
            true, //D
            false, //E
            false, //F
            true, //G
            false, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            false, //C
            false, //D
            false, //E
            false, //F
            false, //G
            true, //H
        },
        new bool[8]
        {
            true, //A
            true, //B
            false, //C
            false, //D
            false, //E
            false, //F
            false, //G
            true, //H
        },
        new bool[8]
        {
            false, //A
            true, //B
            false, //C
            false, //D
            true, //E
            false, //F
            false, //G
            true, //H
        },
        new bool[8]
        {
            true, //A
            false, //B
            false, //C
            true, //D
            true, //E
            false, //F
            false, //G
            true, //H
        },
        new bool[8]
        {
            true, //A
            true, //B
            true, //C
            false, //D
            false, //E
            false, //F
            false, //G
            true, //H
        },
    };


    [SerializeField, Range(0, 14)] int CurrentCube = 0;
    int LastCube = 0;

    void UpdateCube()
    {
        LastCube = CurrentCube;
        var thisCase = Cases[LastCube];

        for (int i = 0; i < TestCube.Length; ++i)
        {
            TestCube[i].IsInSphere = thisCase[i];
        }
    }


    private void OnDrawGizmos()
    {
        if (CurrentCube != LastCube)
        {
            UpdateCube();
        }
        for (int i = 0; i < TestCube.Length; ++i)
        {
            var point = TestCube[i];
            Gizmos.color = point.IsInSphere ? Color.green : Color.black;
            Gizmos.DrawSphere(point.Position, 0.1f);
        }
    }
}
