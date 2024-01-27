using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCameraEffect : MonoEffect
{
    [SerializeField]
    private Transform cameraTarget;
    [SerializeField]
    private Transform panTarget;

    private bool isPanning = false;
    private Vector3 pan;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float maxVelocity;
    protected override void OnEvent()
    {
        isPanning = true;
    }

    private void LateUpdate()
    {
        if (isPanning)
        {
            cameraTarget.position = Vector3.SmoothDamp(cameraTarget.position, panTarget.position + offset, ref pan, 1f, Mathf.Min(Vector3.Distance(cameraTarget.position, panTarget.position), maxVelocity));
        }
    }
}
