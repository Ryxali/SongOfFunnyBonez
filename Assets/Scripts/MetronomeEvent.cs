using UnityEngine;

[CreateAssetMenu(menuName = "Bones/Metronome Event")]
public class MetronomeEvent : ScriptableObject
{
    public static event System.Action<MetronomeEvent> onTrigger = delegate { };

    // Something something trigger enqueue another verse

    public void Trigger() => onTrigger(this);
}