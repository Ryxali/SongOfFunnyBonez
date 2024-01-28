using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartFadeImage : MonoBehaviour
{
    [SerializeField]
    private AnimationCurve fadeCurve;
    [SerializeField]
    private CanvasGroup image;
    [SerializeField]
    private Beat triggerBeat;
    [SerializeField]
    private int waitNBars;
    private int nBeats;
    private bool isRunning;

    private void OnEnable()
    {
        Metronome.onTick += Metronome_onTick;
    }

    private void OnDisable()
    {
        Metronome.onTick -= Metronome_onTick;
    }

    private void Metronome_onTick(Beat beat)
    {
        if(triggerBeat == beat && !isRunning)
        {
            if (nBeats < waitNBars)
                nBeats++;
            else
            {
                isRunning = true;
                StartCoroutine(FadeIn());
            }
        }
    }

    // Start is called before the first frame update
    IEnumerator FadeIn()
    {
        var t = Time.time;
        var duration = fadeCurve.keys[fadeCurve.keys.Length - 1].time;
        var end = t + duration;
        while(Time.time < end)
        {
            image.alpha = fadeCurve.Evaluate(Time.time - t);
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
