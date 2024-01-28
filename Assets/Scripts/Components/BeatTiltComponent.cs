using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatTiltComponent : MonoBehaviour
{
    [SerializeField]
    private BeatTilt[] tilts;

    [System.Serializable]
    private struct BeatTilt
    {
        public Beat beat;
        public float minAngle;
        public float maxAngle;
    }

    private void OnEnable()
    {
        Metronome.onTick += Metronome_onTick;
    }

    private void OnDisable()
    {
        transform.localRotation = Quaternion.identity;
        Metronome.onTick -= Metronome_onTick;
    }

    private void Metronome_onTick(Beat beat)
    {
        for(int i = 0; i < tilts.Length; i++)
        {
            var tilt = tilts[i];
            if(tilt.beat == beat)
            {
                transform.localRotation = Quaternion.Euler(0, 0, Random.Range(tilt.minAngle, tilt.maxAngle));
            }
        }
    }
}
