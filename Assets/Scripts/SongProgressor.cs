using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongProgressor : MonoBehaviour
{
    [SerializeField]
    private Beat tickBeat;

    [SerializeField]
    private Song song;
    [SerializeField]
    private Track[] tracks;
    private BackingTrack backingTrack;
    private int i;
    private PlayingSong playingSong;
    void Awake()
    {
        backingTrack = GetComponentInChildren<BackingTrack>();
        playingSong = song.Create();
        Metronome.onTick += Metronome_onTick;
    }
    private void Start()
    {
        playingSong.Begin();
    }

    private void Metronome_onTick(Beat beat)
    {
        playingSong.Tick(beat);
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        Debug.Log("Progress!");
    //        backingTrack.SwitchTrack(tracks[i = (i + 1) % tracks.Length]);
    //    }
    //}
}
