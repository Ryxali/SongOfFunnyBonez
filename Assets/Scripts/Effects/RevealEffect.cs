using System.Collections;
using UnityEngine;

public class RevealEffect : MonoEffect
{
    [SerializeField]
    private AnimationCurve revealCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1,1));
    
    private void Awake()
    {
        transform.localEulerAngles = new Vector3(90, 0, 0);
    }

    protected override void OnEvent()
    {
        StartCoroutine(Reveal());
    }

    private IEnumerator Reveal()
    {
        var start = transform.localRotation;
        var duration = revealCurve.keys[revealCurve.keys.Length - 1].time;
        var now = Time.time;
        var end = now + duration;
        while(Time.time < end)
        {
            var ev = revealCurve.Evaluate((Time.time - now) / duration);
            transform.localRotation = Quaternion.Slerp(start, Quaternion.identity, ev);
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
    }
}
