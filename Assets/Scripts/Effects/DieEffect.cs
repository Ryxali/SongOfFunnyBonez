using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEffect : MonoEffect
{
    [SerializeField]
    private AnimationCurve rotateCurve;

    [ContextMenu("Test")]
    protected override void OnEvent()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        var start = transform.localRotation;
        var target = Quaternion.Euler(0, 0, -87);

        var duration = rotateCurve.keys[rotateCurve.keys.Length - 1].time;
        var now = Time.time;
        var end = now + duration;
        while (Time.time < end)
        {
            var ev = rotateCurve.Evaluate((Time.time - now));
            if(ev < 0f)
            {
                transform.localRotation = Quaternion.Slerp(start, Quaternion.Euler(0, 0, 87), -ev);
            }
            else
            {
                transform.localRotation = Quaternion.Slerp(start, target, ev);
            }
            yield return null;
        }
        transform.localRotation = target;
    }
}
