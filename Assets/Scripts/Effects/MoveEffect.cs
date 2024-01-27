using UnityEngine;

public class MoveEffect : MonoEffect
{
    [SerializeField]
    private Transform tiltTransform;
    [SerializeField]
    private float distancePerStep;

    private bool angleDir;
    [SerializeField]
    private float minAngle;
    [SerializeField]
    private float maxAngle;

    private int steps;

    private Vector3 origin;
    private void Awake()
    {
        origin = transform.position;
    }
    protected override void OnEvent()
    {
        steps++;
        transform.position = origin + Vector3.right *  steps * distancePerStep;
        var angle = Random.Range(minAngle, maxAngle);
        if (angleDir) angle = -angle;
        tiltTransform.rotation = Quaternion.Euler(0, 0, angle);
        angleDir = !angleDir;
    }
}
