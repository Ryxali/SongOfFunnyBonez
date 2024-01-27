using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongProgressor : MonoBehaviour
{
    [SerializeField]
    private Beat tickBeat;
    [SerializeField]
    private Track[] tracks;
    private BackingTrack backingTrack;
    private int i;
    void Start()
    {
        backingTrack = GetComponentInChildren<BackingTrack>();
        Metronome.onTick += Metronome_onTick;
    }

    private void Metronome_onTick(Beat obj)
    {
        Debug.Log("Tick");
        if(obj == tickBeat)
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Progress!");
            backingTrack.SwitchTrack(tracks[i = (i + 1) % tracks.Length]);
        }
    }
}
