using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YeetCameraEffect : MonoEffect
{
    [SerializeField]
    private float y;


    private bool isPanning = false;
    private Vector3 pan;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float maxVelocity;

    private Vector3 target;
    protected override void OnEvent()
    {
        isPanning = true;
        target = transform.position + Vector3.up * y;
    }

    private void LateUpdate()
    {
        if (isPanning)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target, ref pan, 1f, Mathf.Min(Vector3.Distance(transform.position, target), maxVelocity));
        }
    }
}
