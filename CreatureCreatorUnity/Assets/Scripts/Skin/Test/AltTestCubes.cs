using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// this was used to test the marching cube's data worked right, I don't remember why it's helpful
/// </summary>
[RequireComponent(typeof(MeshFilter))]
public class AltTestCubes : TheCubes
{
    [SerializeField, Range(0, 255)] protected int AltCurrentCube = 0;

    protected override void UpdateCube()
    {
        LastCube = AltCurrentCube;
        TestCube.Data[0].IsInSphere = (AltCurrentCube & 1) == 1;
        TestCube.Data[1].IsInSphere = (AltCurrentCube & 2) == 2;
        TestCube.Data[2].IsInSphere = (AltCurrentCube & 4) == 4;
        TestCube.Data[3].IsInSphere = (AltCurrentCube & 8) == 8;
        TestCube.Data[4].IsInSphere = (AltCurrentCube & 16) == 16;
        TestCube.Data[5].IsInSphere = (AltCurrentCube & 32) == 32;
        TestCube.Data[6].IsInSphere = (AltCurrentCube & 64) == 64;
        TestCube.Data[7].IsInSphere = (AltCurrentCube & 128) == 128;
    }

    protected override void OnDrawGizmos()
    {
        CurrentCube = AltCurrentCube;
        base.OnDrawGizmos();
    }
}
