using System.Linq;

public class PlayingSong
{
    private readonly VerseInstance start;
    private readonly Beat[] beats;

    private VerseInstance active;
    private VerseInstance next;

    private bool hadFirstBeat;

    public PlayingSong(Verse start, Beat[] beats)
    {
        this.start = start.Create();
        this.beats = beats;
    }

    public void Begin()
    {
        start.Enter();
        active = start;
        active.onEnqueueVerse += Active_onEnqueueVerse;
    }

    public void Tick(Beat beat)
    {
        if (!hadFirstBeat)
        {
            hadFirstBeat |= beat == beats[0];
            if (!hadFirstBeat)
                return;
        }
        active.Tick(out var moveNext);

        if (moveNext)
        {
            active.onEnqueueVerse -= Active_onEnqueueVerse;
            active.Exit();
            active = next;
            next = null;
            active.onEnqueueVerse += Active_onEnqueueVerse;
            active.Enter();
        }
    }

    private void Active_onEnqueueVerse(Verse verse)
    {
        next = verse.Create();
        next.Prepare();
    }
}
