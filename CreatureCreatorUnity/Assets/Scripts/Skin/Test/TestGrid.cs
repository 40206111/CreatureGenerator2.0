using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    [SerializeField] int XSize;
    [SerializeField] int YSize;
    [SerializeField] int ZSize;
    [SerializeField] float divisions = 1f;

    void Start()
    {
        MarchingCubesGrid.CreateGrid(transform.position, new Vector3(XSize, YSize, ZSize), divisions);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload Grid");
            MarchingCubesGrid.CreateGrid(transform.position, new Vector3(XSize, YSize, ZSize), divisions);
        }
    }

    protected virtual void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        for (int i = 0; i < MarchingCubesGrid.TheGrid.Count; ++i)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position + MarchingCubesGrid.TheGrid[i], 0.1f);
        }
    }
}
