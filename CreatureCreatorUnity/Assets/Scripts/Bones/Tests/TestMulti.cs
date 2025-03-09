using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// used to test multi point skeletons
/// </summary>
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class TestMulti : MonoBehaviour
{
    [SerializeField]
    TestSingleData DataPrefab;

    [SerializeField]
    int NumberOfPoints;

    [SerializeField]
    float GridDivisions;

    [SerializeField]
    bool ForceUpdate;

    List<TestSingleData> TestPoints = new List<TestSingleData>();

    Skeleton SkeleBones = new Skeleton(new SkeletonPointData());

    Mesh TheMesh;

    private void Awake()
    {
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        TheMesh.name = "Skin";
        //Test points gets reset on play, this allows us to use it in edit and play mode
        TestPoints.AddRange( GetComponentsInChildren<TestSingleData>() );
        UpdateMesh();
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

        if (GridDivisions != SkeleBones.TestGetDivisions())
        {
            SkeleBones.TestUpdateGridDivisions(GridDivisions);
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
        SkeleBones.SkelePoints.Clear();

        for (int i = 0; i < TestPoints.Count; ++i)
        {
            var testPoint = TestPoints[i];
            SkeleBones.SkelePoints.Add(testPoint.Data);
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
            point.transform.position = new Vector3(TestPoints.Count * 4, 0, 0);
            point.name = $"Skelepoint {TestPoints.Count}";
            TestPoints.Add(point);
        }
    }

    void UpdateMesh()
    {
        Debug.Log("Updating Mesh");
        GenerateSkeleton();
        SkeleBones.MakeMyMesh();
        TheMesh = GetComponent<MeshFilter>().mesh = new Mesh();
        var meshData = SkeleBones.SkeleMesh;
        if (meshData.Vertices.Count > 0)
        {
            TheMesh.vertices = new Vector3[meshData.Vertices.Count];
            TheMesh.vertices = meshData.Vertices.ToArray();
            TheMesh.triangles = new int[meshData.Triangles.Count];
            TheMesh.triangles = meshData.Triangles.ToArray();
        }
        TheMesh.RecalculateNormals();
        TheMesh.name = $"SkinTestMulti";
    }

    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Update();
        }
    }
}
