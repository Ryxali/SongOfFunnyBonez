using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Metronome : MonoBehaviour
{
    public static event System.Action<Beat> onTick = delegate { };
    [SerializeField]
    private Beat[] beats;

    [SerializeField]
    private int bpm = 70;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true)
        {
            for(int i = 0; i < beats.Length; i++)
            {
                onTick(beats[i]);

                yield return new WaitForSeconds(80f/60f);
            }
        }
    }
}
