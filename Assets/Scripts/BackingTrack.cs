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
    [SerializeField]
    private AudioSource[] songSources;
    private int songSourcesIter;

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

    public void SwitchTrack(Track track, AudioClip song)
    {
        
        var nextTrack = track ? tracks.First(t => t.track == track).audioSource : activeTrack;
        var barEnd = AudioSettings.dspTime;

        if (activeTrack)
        {
            // Switch at the end of the current audio clip
            var remain = activeTrack.clip.length - activeTrack.time % activeTrack.clip.length;
            var switchTime = AudioSettings.dspTime + remain;
            if (song)
            {
                var songSource = songSources[songSourcesIter];
                songSource.clip = song;
                songSource.PlayScheduled(switchTime);
                songSourcesIter = (songSourcesIter + 1) % songSources.Length;
            }
            if (activeTrack != nextTrack)
            {
                fadingTrack = activeTrack;
                activeTrack = nextTrack;
                fadingTrack.SetScheduledEndTime(switchTime);
                activeTrack.PlayScheduled(switchTime);
                activeTrack.SetScheduledEndTime(double.MaxValue);
            }
        }
        else
        {
            float bps = BeatsPerSecond*4;
            activeTrack = nextTrack;
            var switchTime = AudioSettings.dspTime - (AudioSettings.dspTime % bps) + bps;
            activeTrack.PlayScheduled(switchTime);
            if (song)
            {
                var songSource = songSources[songSourcesIter];
                songSource.clip = song;
                songSource.PlayScheduled(switchTime);
                songSourcesIter = (songSourcesIter + 1) % songSources.Length;
            }
        }
    }
}
