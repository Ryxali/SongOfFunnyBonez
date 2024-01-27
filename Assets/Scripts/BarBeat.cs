using UnityEngine;

[System.Serializable]
public struct BarBeat
{
    [Tooltip("The text to display with this beat")]
    public string text;
    public MetronomeEvent triggerEvent;
}
