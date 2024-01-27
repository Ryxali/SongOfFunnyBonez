using UnityEngine;

[CreateAssetMenu(menuName = "Bones/Song")]
public class Song : ScriptableObject
{
    [SerializeField]
    private Beat[] beats;
    [SerializeField]
    private Verse startVerse;

    public PlayingSong Create()
    {
        return new PlayingSong(startVerse, beats);
    }
}
