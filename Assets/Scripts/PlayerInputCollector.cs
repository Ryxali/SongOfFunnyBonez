using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class PlayerInputCollector : MonoBehaviour
{
    public static PlayerInputCollector Instance { get; private set; }

    public string Current { get; private set; }

    private bool isRecording = true;

    private void Awake()
    {
        Instance = this;
    }

    public void Clear()
    {
        Current = string.Empty;
        isRecording = true;
    }

    private void Update()
    {
        if (!isRecording)
            return;
        var inp = Input.inputString;
        
        foreach(var c in inp)
        {
            if(c == '\b')
            {
                Current = Current.Substring(0,Current.Length - 1);
            }
            else if ((c == '\n') || (c == '\r'))
            {
                isRecording = false;
            }
            else
            {
                Current += c;
            }
        }
    }
}
