using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandOnceEffect : MonoEffect
{
    [SerializeField]
    private AnimationCurve curve;

    [ContextMenu("Test")]
    protected override void OnEvent()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        var start = transform.localScale;
        var duration = curve.keys[curve.keys.Length - 1].time;

        var now = Time.time;
        var end = now + duration;

        while (Time.time < end)
        {
            var ev = curve.Evaluate((Time.time - now) / duration);
            transform.localScale = start * ev;
            yield return null;
        }

        transform.localScale = start;
    }
}
