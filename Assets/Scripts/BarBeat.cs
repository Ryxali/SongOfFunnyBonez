using UnityEngine;

[System.Serializable]
public struct BarBeat
{
    public string Text => userInput ? $"%{text}%" : text;
    [Tooltip("The text to display with this beat")]
    [SerializeField]
    private string text;
    public bool userInput;
    public MetronomeEvent triggerEvent;

    private string Format()
    {
        var str = PlayerInputCollector.Instance.Current;
        return str.PadRight(text.Length, '_');
    }
}
