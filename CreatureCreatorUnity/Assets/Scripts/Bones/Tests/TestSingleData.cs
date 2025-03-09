using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSingleData : MonoBehaviour
{
    public SkeletonPointData Data = new SkeletonPointData();

    [SerializeField]
    float Radius;

    [SerializeField]
    List<TestSingleData> Neighbours;

    [HideInInspector]
    public bool CauseUpdate;

    private void Awake()
    {
        Data.Radius = Radius;
        Data.Position = transform.position;
    }

    public void UpdateNeighbours()
    {
        Data.Neigbours.Clear();
        for (int i = 0; i < Neighbours.Count; ++i)
        {
            var nb = Neighbours[i];
            if (nb != null)
            {
                Data.Neigbours.Add(nb.Data);
                CauseUpdate = true;
            }
            else
            {
                Neighbours.RemoveAt(i);
            }
        }
    }

    public void MyUpdate()
    {
        CauseUpdate = Data.Radius != Radius;
        CauseUpdate |= Data.Position != transform.position;

        if (Data.Neigbours.Count != Neighbours.Count)
        {
            Data.Neigbours.Clear();
            foreach (var nb in Neighbours)
            {
                if (nb != null)
                {
                    Data.Neigbours.Add(nb.Data);
                    CauseUpdate = true;
                }
            }
        }

        if (CauseUpdate)
        {
            Data.Radius = Radius;
            Data.Position = transform.position;
        }
    }
}
