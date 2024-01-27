using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Metronome : MonoBehaviour
{
    public static event System.Action<Beat> onTick = delegate { };
    [SerializeField]
    private Beat[] beats;

    private BackingTrack backingTrack;
    private double next = 0f;
    private int beatI;

    private void Awake()
    {
        backingTrack = GetComponentInChildren<BackingTrack>();
        var bps = backingTrack.BeatsPerSecond;
        next = AudioSettings.dspTime - (AudioSettings.dspTime % bps) + bps;
    }

    private void Update()
    {
        var current = backingTrack.CurrentBeat();
        if (current >= 0 && beatI != current)
        {
            beatI = current;
            onTick(beats[current]);
        }
        //var dspTime = AudioSettings.dspTime;
        
        //if(next <= dspTime)
        //{
        //    var bps = backingTrack.BeatsPerSecond;
        //    next = AudioSettings.dspTime - AudioSettings.dspTime % bps + bps;
        //    Debug.Log(beats[beatI]);
        //    onTick(beats[beatI]);
        //    beatI = (beatI + 1) % beats.Length;
        //}

    }
}
