using System.Collections.Generic;
using System.Linq;

public class VerseInstance
{
    public event System.Action<Verse> onEnqueueVerse = delegate { };
    private readonly Track track;
    private readonly Bar[] bars;
    private readonly Dictionary<MetronomeEvent, Verse> nextVerse;
    private readonly Verse autoNextVerse;

    public VerseInstance(Track track, Bar[] bars, NextVerse[] nextVerses)
    {
        this.track = track;
        this.bars = bars;
        if(nextVerses.Any(v => !v.beatEvent))
        {
            autoNextVerse = nextVerses.First().verse;
        }
        else
        {
            nextVerse = nextVerses.ToDictionary(k => k.beatEvent, v => v.verse);
        }
    }


    private int barIndex;
    private int beatIndex;

    public void Tick(out bool moveNext)
    {
        var bar = bars[barIndex];
        var beat = new[] { bar.beat0, bar.beat1, bar.beat2, bar.beat3 }[beatIndex];

        if(autoNextVerse && barIndex == 0 && beatIndex == 0)
        {
            onEnqueueVerse(autoNextVerse);
        }

        if (beat.triggerEvent)
            beat.triggerEvent.Trigger();
        beatIndex++;
        moveNext = false;
        if(beatIndex > 3)
        {
            beatIndex = 0;
            barIndex++;
            if(barIndex >= bars.Length)
            {
                moveNext = true;
            }
        }
    }

    public void Prepare()
    {
        if(track)
            BackingTrack.Instance.SwitchTrack(track);
    }

    public void Enter()
    {
        KaraokeCanvas.Instance.UpdateText(string.Join(" ", bars.Select(b => $"{b.beat0.text} {b.beat1.text} {b.beat2.text} {b.beat3.text}")));
        MetronomeEvent.onTrigger += MetronomeEvent_onTrigger;
    }

    private void MetronomeEvent_onTrigger(MetronomeEvent evt)
    {
        if(nextVerse.TryGetValue(evt, out var verse))
        {
            onEnqueueVerse(verse);
        }
    }

    public void Exit()
    {
        MetronomeEvent.onTrigger -= MetronomeEvent_onTrigger;
    }
}
