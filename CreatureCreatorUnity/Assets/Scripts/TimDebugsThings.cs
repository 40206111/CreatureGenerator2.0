using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimDebugsThings : MonoBehaviour
{
    private static TimDebugsThings _instance;
    public static TimDebugsThings Instance { get { return _instance; } }
    private void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    Transform Sphere;
    [SerializeField]
    Material InMat;
    [SerializeField]
    Material OutMat;
    [SerializeField]
    bool VisualiseSpecificCube = false;
    bool lastVSC = false;
    [SerializeField]
    bool IncreaseSCI = false;
    [SerializeField]
    bool DecreaseSCI = false;
    int lastSCI = -1;


    [SerializeField, Range(0, 9)]
    int CI1000 = 0;
    [SerializeField, Range(0, 9)]
    int CI0100 = 0;
    [SerializeField, Range(0, 9)]
    int CI0010 = 0;
    [SerializeField, Range(0, 9)]
    int CI0001 = 0;
    private int SpecificCubeIndex
    {
        get
        {
            return
                CI0001 +
                CI0010 * 10 +
                CI0100 * 100 +
                CI1000 * 1000;
        }
        set
        {
            CI0001 = value % 10;
            CI0010 = value / 10 % 10;
            CI0100 = value / 100 % 10;
            CI1000 = value / 1000 % 10;
        }
    }

    List<CubeData> Cubes;
    List<MeshRenderer> Spheres = new List<MeshRenderer>();
    Transform SpawnedStart;
    Transform SpawnedEnd;
    List<Transform> CubeCenters = new List<Transform>();

    private void Update()
    {
        if (IncreaseSCI)
        {
            IncreaseSCI = false;
            SpecificCubeIndex++;
        }
        if (DecreaseSCI)
        {
            DecreaseSCI = false;
            SpecificCubeIndex--;
        }
        if (VisualiseSpecificCube)
        {
            if (lastSCI != SpecificCubeIndex || lastVSC == false)
            {
                VisualSpecificCube();
            }
        }
        if (lastVSC == true && VisualiseSpecificCube == false)
        {
            VisualAllCubes();
        }

        lastSCI = SpecificCubeIndex;
        lastVSC = VisualiseSpecificCube;
    }

    void VisualSpecificCube()
    {
        ClearInsAndOuts();
        if (SpecificCubeIndex >= Cubes.Count)
        {
            return;
        }
        foreach (var p in Cubes[SpecificCubeIndex].Data)
        {
            Spheres.Add(Instantiate(Sphere, p.Position, Quaternion.identity, this.transform).GetComponent<MeshRenderer>());
            Spheres[Spheres.Count - 1].material = p.IsInSphere ? InMat : OutMat;
        }
    }

    void VisualAllCubes()
    {
        ClearInsAndOuts();
        foreach (CubeData c in Cubes)
        {
            foreach (var p in c.Data)
            {
                Spheres.Add(Instantiate(Sphere, p.Position, Quaternion.identity, this.transform).GetComponent<MeshRenderer>());
                Spheres[Spheres.Count - 1].material = p.IsInSphere ? InMat : OutMat;
            }
        }
    }

    private void ClearInsAndOuts()
    {
        for (int i = Spheres.Count - 1; i >= 0; --i)
        {
            Destroy(Spheres[i].gameObject);
        }
        Spheres = new List<MeshRenderer>();
    }

    public void ClearVisuals()
    {
        ClearInsAndOuts();
        if (SpawnedStart != null)
        {
            Destroy(SpawnedStart.gameObject);
        }
        if (SpawnedEnd != null)
        {
            Destroy(SpawnedEnd.gameObject);
        }
        for (int i = 0; i < CubeCenters.Count; ++i)
        {
            Destroy(CubeCenters[i].gameObject);
        }
        CubeCenters.Clear();
        Cubes = new List<CubeData>();
    }
    public void VisualUpdateCube(CubeData cube)
    {
        foreach (var p in cube.Data)
        {
            Spheres.Add(Instantiate(Sphere, p.Position, Quaternion.identity, this.transform).GetComponent<MeshRenderer>());
            Spheres[Spheres.Count - 1].material = p.IsInSphere ? InMat : OutMat;
        }
        Cubes.Add(cube);
    }

    public void VisualStartEndPoint(Vector3 start, Vector3 end)
    {
        SpawnedStart = Instantiate(Sphere, start, Quaternion.identity, this.transform);
        SpawnedStart.localScale = Vector3.one * 0.3f;
        SpawnedEnd = Instantiate(Sphere, end, Quaternion.identity, this.transform);
        SpawnedEnd.localScale = Vector3.one * 0.3f;
    }


    public void AddVisualCubeCenters(Vector3 point)
    {
        CubeCenters.Add(Instantiate(Sphere, point, Quaternion.identity, this.transform));
        CubeCenters[CubeCenters.Count - 1].localScale = Vector3.one * 0.3f;
    }
}
