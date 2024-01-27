using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackingTrack : MonoBehaviour
{
    [SerializeField]
    private Pairing[] tracks;

    [System.Serializable]
    private struct Pairing
    {
        public Track track;
        public AudioSource audioSource;
    }

    [SerializeField]
    private Track defaultTrack;

    private AudioSource fadingTrack;
    private AudioSource activeTrack;

    private void Awake()
    {
        const float Bps = 70f / 300f;
        activeTrack = tracks.First(t => t.track == defaultTrack).audioSource;
        activeTrack.PlayScheduled(AudioSettings.dspTime - AudioSettings.dspTime % Bps + Bps);
    }

    public void SwitchTrack(Track track)
    {
        var nextTrack = tracks.First(t => t.track == track).audioSource;
        if(nextTrack == activeTrack)
        {
            Debug.LogWarning("No switch track to itself");
            return;
        }
        var barEnd = AudioSettings.dspTime;

        // Switch at the end of the current audio clip
        var remain = activeTrack.clip.length - activeTrack.time % activeTrack.clip.length;
        fadingTrack = activeTrack;
        activeTrack = nextTrack;
        var switchTime = AudioSettings.dspTime + remain;
        fadingTrack.SetScheduledEndTime(switchTime);
        activeTrack.PlayScheduled(switchTime);
        activeTrack.SetScheduledEndTime(double.MaxValue);
    }
}
