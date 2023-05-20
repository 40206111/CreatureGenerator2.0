using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubesData
{
    static public MeshData[] CaseMeshData = new MeshData[15]
    {
        //0
       new MeshData(new List<Vector3>(), new List<int>()),
       //1
       new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(0.5f, 0, 0),
           new Vector3(0, 0, 0.5f),},
       new List<int> {2, 1, 0}
       ),
       //2
       new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(1, 0.5f, 0),
           new Vector3(1, 0, 0.5f),
           new Vector3(0, 0, 0.5f),},
       new List<int>{3, 2, 0, 2, 1, 0}
       ),
       //3
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(0.5f, 0, 0),
           new Vector3(0, 0, 0.5f),
           new Vector3(0.5f, 1f, 0),
           new Vector3(1f, 0.5f, 0),
           new Vector3(1, 1, 0.5f),},
       new List<int> {2, 1, 0, 3, 4, 5}
       ),
       //4
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 1f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0.5f, 0f),
           new Vector3(0f, 0f, 0.5f),
           new Vector3(0.5f, 0f, 0f),},
       new List<int> {0, 1, 2, 0, 2, 3, 2, 4, 3}
       ),
       //5
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(0f, 0.5f, 1f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0.5f, 0f),},
       new List<int> {0, 1, 2, 0, 2, 3}
       ),
       //6
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 1f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0.5f, 0f),
           new Vector3(0f, 0f, 0.5f),
           new Vector3(0.5f, 0f, 0f),
           new Vector3(0f, 1f, 0.5f),
           new Vector3(0.5f, 1f, 0f),
           new Vector3(0f, 0.5f, 0f),},
       new List<int> {0, 1, 2, 0, 2, 3, 2, 4, 3, 7, 6, 5}
       ),
       //7
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(0.5f, 0, 0),
           new Vector3(0, 0, 0.5f),
           new Vector3(0.5f, 1f, 0),
           new Vector3(1f, 0.5f, 0),
           new Vector3(1, 1, 0.5f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0f, 0.5f),
           new Vector3(0.5f, 0f, 1f),
           new Vector3(0f, 1f, 0.5f),
           new Vector3(0.5f, 1, 1f),
           new Vector3(0f, 0.5f, 1f),},
       new List<int> {2, 1, 0, 3, 4, 5, 6, 7, 8, 9, 10, 11}
       ),
       //8
        new MeshData(new List<Vector3>{
           new Vector3(0f, 1f, 0.5f),
           new Vector3(0.5f, 1f, 1f),
           new Vector3(0f, 0.5f, 0f),
           new Vector3(0.5f, 0f, 0f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0, 0.5f),},
       new List<int> {0, 1, 2, 2, 1, 3, 1, 4, 3, 3, 4, 5}
       ),
       //9
        new MeshData(new List<Vector3>{
           new Vector3(0f, 1f, 0.5f),
           new Vector3(0.5f, 0f, 0f),
           new Vector3(0f, 0f, 0.5f),
           new Vector3(0.5f, 1f, 1f),
           new Vector3(1f, 0.5f, 1f),
           new Vector3(1f, 0.5f, 0f),},
       new List<int> {0, 1, 2, 0, 3, 4, 0, 4, 1, 4, 5, 1}
       ),
       //10
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(0.5f, 0, 0),
           new Vector3(0, 0, 0.5f),
           new Vector3(0.5f, 1, 1f),
           new Vector3(1f, 1f, 0.5f),
           new Vector3(1f, 0.5f, 1f),},
       new List<int> {2, 1, 0, 3, 4, 5}
       ),
       //11
       new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 0),
           new Vector3(1, 0.5f, 0),
           new Vector3(1, 0, 0.5f),
           new Vector3(0, 0, 0.5f),
           new Vector3(0.5f, 1, 1f),
           new Vector3(1f, 1f, 0.5f),
           new Vector3(1f, 0.5f, 1f),},
       new List<int>{3, 2, 0, 2, 1, 0, 4, 5, 6}
       ),
       //12
        new MeshData(new List<Vector3>{
           new Vector3(0, 1f, 0.5f),
           new Vector3(0.5f, 1f, 0f),
           new Vector3(0, 0.5f, 0f),
           new Vector3(1f, 0.5f, 0f),
           new Vector3(0.5f, 0f, 0),
           new Vector3(1f, 0f, 0.5f),
           new Vector3(0.5f, 1, 1f),
           new Vector3(1f, 1f, 0.5f),
           new Vector3(1f, 0.5f, 1f)},
       new List<int> {2, 1, 0, 3, 4, 5, 6, 7, 8}
       ),
       //13
        new MeshData(new List<Vector3>{
           new Vector3(0f, 1f, 0.5f),
           new Vector3(0.5f, 1f, 0f),
           new Vector3(0.5f, 0, 0f),
           new Vector3(0, 0, 0.5f),
           new Vector3(0.5f, 1f, 1f),
           new Vector3(1f, 1f, 0.5f),
           new Vector3(1f, 0f, 0.5f),
           new Vector3(0.5f, 0f, 1f),},
       new List<int> {2, 1, 0, 3, 2, 0, 4, 5, 6, 4, 6, 7}
       ),
       //14
        new MeshData(new List<Vector3>{
           new Vector3(0, 0.5f, 1f),
           new Vector3(0.5f, 0f, 0f),
           new Vector3(0f, 0.5f, 0f),
           new Vector3(0.5f, 1f, 1f),
           new Vector3(1f, 1f, 0.5f),
           new Vector3(1f, 0f, 0.5f),
        },
       new List<int> {0, 1, 2, 3, 4, 0, 0, 4, 1, 4, 5, 1}
       ),
    };

    static public bool[][] Cases = new bool[15][]
    {
        //0
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
        //1
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
        //2
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
        //3
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
        //4
        new bool[8]
        {
            false, //A
            true, //B
            true, //C
            true, //D
            false, //E
            false, //F
            false, //G
            false, //H
        },
        //5
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
        //6
        new bool[8]
        {
            false, //A
            true, //B
            true, //C
            true, //D
            true, //E
            false, //F
            false, //G
            false, //H
        },
        //7
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
        //8
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
        //9
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
        //10
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
        //11
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
        //12
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
        //13
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
        //14
        new bool[8]
        {
            true, //A
            false, //B
            true, //C
            true, //D
            false, //E
            false, //F
            false, //G
            true, //H
        },
 };
}
