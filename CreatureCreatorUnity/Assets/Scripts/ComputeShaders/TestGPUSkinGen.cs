using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct SkelePointData
{
    public Vector3 pos;
    public float rad;
};

struct GpuCubeData
{
    public Vector4 cubeVertex1;
    public Vector4 cubeVertex2;
    public Vector4 cubeVertex3;
    public Vector4 cubeVertex4;
    public Vector4 cubeVertex5;
    public Vector4 cubeVertex6;
    public Vector4 cubeVertex7;
    public Vector4 cubeVertex8;
};

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TestGPUSkinGen : MonoBehaviour
{
    [SerializeField]
    ComputeShader _computeShader;

    [SerializeField]
    TestSingleData DataPrefab;

    [SerializeField]
    int NumberOfPoints;

    [SerializeField]
    float GridDivisions;

    [SerializeField]
    bool ForceUpdate;

    ComputeBuffer _pointsBuffer;
    ComputeBuffer _mcBuffer;
    static readonly int _pointsId = Shader.PropertyToID("_SkelePoints");
    static readonly int _gridDivId = Shader.PropertyToID("_GridDivisions");
    static readonly int _mcId = Shader.PropertyToID("_MarchingCubes");

    readonly int maxSkeletonPoints = 100;
    readonly int maxMarchingCubes = 100000;


    List<TestSingleData> TestPoints = new List<TestSingleData>();

    List<SkelePointData> _skelePoints = new List<SkelePointData>();
    List<GpuCubeData> _marchingCubes =  new List<GpuCubeData>();

    Mesh TheMesh;

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        TheMesh.name = "Skin";
        //Test points gets reset on play, this allows us to use it in edit and play mode
        TestPoints.AddRange(GetComponentsInChildren<TestSingleData>());

        _pointsBuffer = new ComputeBuffer(maxSkeletonPoints, 4 * sizeof(float));
        _mcBuffer = new ComputeBuffer(maxMarchingCubes, (4 * sizeof(float)) * 8);
        UpdateMesh();
    }

    private void OnDisable()
    {
        _pointsBuffer.Release();
        _mcBuffer.Release();
        _pointsBuffer = null;
        _mcBuffer = null;
    }

    private void OnDestroy()
    {
        _pointsBuffer.Release();
        _mcBuffer.Release();
        _pointsBuffer = null;
        _mcBuffer = null;
    }

    void Update()
    {
        if (TheMesh == null)
        {
            Awake();
        }

        bool updateMesh = false;
        if (TestPoints.Count != NumberOfPoints)
        {
            UpdatePointCount();
            updateMesh = true;
        }

        if (ForceUpdate)
        {
            ForceUpdate = false;
            updateMesh = true;
        }

        for (int i = 0; i < TestPoints.Count; ++i)
        {
            TestPoints[i].MyUpdate();
            if (TestPoints[i].CauseUpdate)
            {
                updateMesh = true;
            }
        }

        if (updateMesh)
        {
            UpdateMesh();
        }
    }

    void GenerateSkeleton()
    {
        _skelePoints.Clear();

        for (int i = 0; i < TestPoints.Count; ++i)
        {
            var testPoint = TestPoints[i];
            var skelePoint = new SkelePointData()
            {
                pos = testPoint.Data.Position,
                rad = testPoint.Data.Radius
            };
            _skelePoints.Add( skelePoint );
        }
    }

    void UpdatePointCount()
    {
        if (NumberOfPoints <= 0)
        {
            for (int i = 0; i < TestPoints.Count; ++i)
            {
                if (TestPoints[i] != null)
                {
                    GameObject.DestroyImmediate(TestPoints[i].gameObject);
                }
            }
            TestPoints.Clear();
            return;
        }

        bool fixNeighbours = TestPoints.Count > NumberOfPoints;
        while (TestPoints.Count > NumberOfPoints)
        {
            int lastPoint = TestPoints.Count - 1;
            if (TestPoints[lastPoint] != null)
            {
                GameObject.DestroyImmediate(TestPoints[lastPoint].gameObject);
            }
            TestPoints.RemoveAt(lastPoint);
        }
        if (fixNeighbours)
        {
            for (int i = 0; i < TestPoints.Count; ++i)
            {
                if (TestPoints[i] != null)
                {
                    TestPoints[i].UpdateNeighbours();
                }
            }
        }

        while (TestPoints.Count < NumberOfPoints)
        {
            var point = Instantiate(DataPrefab, transform);
            point.transform.position = new Vector3(TestPoints.Count, 0, 0);
            point.name = $"Skelepoint {TestPoints.Count}";
            TestPoints.Add(point);
        }
    }

    void UpdateMesh()
    {
        Debug.Log("Updating Mesh");
        GenerateSkeleton();

        if ( _pointsBuffer == null )
        {
            _pointsBuffer = new ComputeBuffer(maxSkeletonPoints, 4 * sizeof(float));
        }
        if (_mcBuffer == null)
        {
            _mcBuffer = new ComputeBuffer(maxMarchingCubes, (4 * sizeof(float)) * 8);
        }
        _pointsBuffer.SetData(_skelePoints);
        _computeShader.SetBuffer(0, _pointsId, _pointsBuffer);
        _computeShader.SetBuffer(0, _mcId, _mcBuffer);
        _computeShader.SetFloat( _gridDivId, GridDivisions );
        _computeShader.Dispatch(0, 8, 8, 1);
        var marchingCubes = new GpuCubeData[maxMarchingCubes];
        _mcBuffer.GetData(marchingCubes);
        _marchingCubes.Clear();
        _marchingCubes.AddRange(marchingCubes);
        //_marchingCubes.AddRange(marchingCubes);
        //SkeleBones.MakeMyMesh();
        //TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        //var meshData = SkeleBones.SkeleMesh;
        //if (meshData.Vertices.Count > 0)
        //{
        //    TheMesh.vertices = new Vector3[meshData.Vertices.Count];
        //    TheMesh.vertices = meshData.Vertices.ToArray();
        //    TheMesh.triangles = new int[meshData.Triangles.Count];
        //    TheMesh.triangles = meshData.Triangles.ToArray();
        //}
        //TheMesh.RecalculateNormals();
        //TheMesh.name = $"SkinTestMulti";
    }

    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Update();
        }
    }

}
