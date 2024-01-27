using System.Collections;
using UnityEngine;

public class RevealEffect : MonoEffect
{
    [SerializeField]
    private AnimationCurve revealCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1,1));
    [SerializeField]
    private Mode revealMode;

    private Vector3 originalScale;

    private enum Mode
    {
        Rotate,
        Scale,
        Drop
    }

    private void Awake()
    {
        switch (revealMode)
        {
            case Mode.Rotate:
            transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case Mode.Scale:
                originalScale = transform.localScale;
                transform.localScale = Vector3.zero;
                break;
            case Mode.Drop:
                break;
        }

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
            switch (revealMode)
            {
                case Mode.Rotate:
                    transform.localRotation = Quaternion.Slerp(start, Quaternion.identity, ev);
                    break;
                case Mode.Scale:
                    transform.localScale = originalScale * ev;
                    break;
                case Mode.Drop:
                    break;
            }
            yield return null;
        }
        switch (revealMode)
        {
            case Mode.Rotate:
                transform.localRotation = Quaternion.identity;
                break;
            case Mode.Scale:
                transform.localScale = originalScale;
                break;
            case Mode.Drop:
                break;
        }
    }
}
