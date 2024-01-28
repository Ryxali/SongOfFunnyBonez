using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEffect : MonoEffect
{
    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private AnimationCurve curve;

    private void Awake()
    {
        transform.localRotation = Quaternion.Euler(origin);
    }

    protected override void OnEvent()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        var start = transform.localRotation;
        var duration = curve.keys[curve.keys.Length - 1].time;
        var now = Time.time;
        var end = now + duration;
        while (Time.time < end)
        {
            var ev = curve.Evaluate((Time.time - now) / duration);
            transform.localRotation = Quaternion.Slerp(start, Quaternion.identity, ev);
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
    }
}
