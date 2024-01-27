using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackingTrack : MonoBehaviour
{
    public static BackingTrack Instance { get; private set; }
    public float BeatsPerMinute => beatsPerMinute;
    public float BeatsPerSecond => beatsPerMinute / 60f;
    [SerializeField]
    private float beatsPerMinute;
    [SerializeField]
    private Pairing[] tracks;

    [System.Serializable]
    private struct Pairing
    {
        public Track track;
        public AudioSource audioSource;
        public float[] beatTimes;
    }

    [SerializeField]
    private Track defaultTrack;

    private AudioSource fadingTrack;
    private AudioSource activeTrack;

    private void Awake()
    {
        Instance = this;
        float bps = BeatsPerSecond*4;
        //activeTrack = tracks.First(t => t.track == defaultTrack).audioSource;
        //activeTrack.PlayScheduled(AudioSettings.dspTime - (AudioSettings.dspTime % bps) + bps);
    }

    public int CurrentBeat()
    {
        var track = fadingTrack && fadingTrack.isPlaying ? fadingTrack : activeTrack;
        var beatTimes = tracks.First(t => t.audioSource = track).beatTimes;
        var samplePosition = track.timeSamples;
        var playbackTime = track.time % track.clip.length;
        
        for(int i = beatTimes.Length-1; i >= 0; i--)
        {
            if (beatTimes[i] <= playbackTime)
                return i % 4;
        }
        return -1;
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

        if (activeTrack)
        {
            // Switch at the end of the current audio clip
            var remain = activeTrack.clip.length - activeTrack.time % activeTrack.clip.length;
            fadingTrack = activeTrack;
            activeTrack = nextTrack;
            var switchTime = AudioSettings.dspTime + remain;
            fadingTrack.SetScheduledEndTime(switchTime);
            activeTrack.PlayScheduled(switchTime);
            activeTrack.SetScheduledEndTime(double.MaxValue);
        }
        else
        {
            float bps = BeatsPerSecond*4;
            activeTrack = nextTrack;
            activeTrack.PlayScheduled(AudioSettings.dspTime - (AudioSettings.dspTime % bps) + bps);
        }
    }
}
