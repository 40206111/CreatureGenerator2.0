using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SkelePointData
{
    Vector3 pos;
    float rad;
};

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TestGPUSkinGen : MonoBehaviour
{
    [SerializeField]
    ComputeShader _computeShader;

    ComputeBuffer _pointsBuffer;
    static readonly int _pointsId = Shader.PropertyToID("_SkelePoints");

    readonly int maxSkeletonPoints = 100;

    private void Start()
    {
        _pointsBuffer = new ComputeBuffer(maxSkeletonPoints, 4 * sizeof(float));
        _computeShader.SetBuffer(0, _pointsId, _pointsBuffer);
        _computeShader.Dispatch(0, 8, 8, 1);
    }
}
