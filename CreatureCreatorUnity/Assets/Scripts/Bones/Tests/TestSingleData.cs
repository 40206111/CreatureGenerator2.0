using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSingleData : MonoBehaviour
{
    public float Radius;

    [HideInInspector]
    public bool CauseUpdate;

    private float lastRad;

    private Vector3 lastPos;

    private void Awake()
    {
        lastRad = Radius;
        lastPos = transform.position;
    }

    public void MyUpdate()
    {
        CauseUpdate = lastRad != Radius;
        CauseUpdate |= lastPos != transform.position;

        lastRad = Radius;
        lastPos = transform.position;
    }
}
