using UnityEngine;

[CreateAssetMenu(menuName = "Bones/Verse")]
public class Verse : ScriptableObject
{
    
    [SerializeField][Tooltip("Can be left blank")]
    private Track track;
    [SerializeField]
    private AudioClip song;
    [SerializeField]
    private Bar[] bars;
    [SerializeField]
    private NextVerse[] nextVerses;

    public VerseInstance Create() => new VerseInstance(track, bars, nextVerses, song);
    
}
