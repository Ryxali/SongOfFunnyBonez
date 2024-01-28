using UnityEngine;

[CreateAssetMenu(menuName = "Bones/Metronome Input Event")]
public class MetronomeInputEvent : MetronomeEvent
{
    [SerializeField]
    private Pairing[] acceptedInput;
    [System.Serializable]
    private struct Pairing
    {
        public string inputString;
        public MetronomeEvent trigger;
    }

    public override void Trigger()
    {
        base.Trigger();
        var inputString = PlayerInputCollector.Instance.Current;
        foreach(var pair in acceptedInput)
        {
            if(pair.trigger && string.Equals(pair.inputString, inputString, System.StringComparison.OrdinalIgnoreCase))
            {
                pair.trigger.Trigger();
            }
        }
    }
}