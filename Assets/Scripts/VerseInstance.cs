using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class VerseInstance
{
    public event System.Action<Verse> onEnqueueVerse = delegate { };
    private readonly Track track;
    private readonly Bar[] bars;
    private readonly Dictionary<MetronomeEvent, Verse> nextVerse;
    private readonly Verse autoNextVerse;
    private readonly AudioClip songClip;
    private readonly StringBuilder stringBuilder;

    public VerseInstance(Track track, Bar[] bars, NextVerse[] nextVerses, AudioClip songClip)
    {
        stringBuilder = new StringBuilder();
        this.track = track;
        this.bars = bars;
        if (nextVerses.Any(v => !v.beatEvent))
        {
            autoNextVerse = nextVerses.First().verse;
        }
        nextVerse = nextVerses.Where(v => v.beatEvent).ToDictionary(k => k.beatEvent, v => v.verse);
        this.songClip = songClip;
    }


    private int barIndex;
    private int beatIndex;
    private bool triggeredAnyEvent;

    public void Tick(out bool moveNext)
    {
        var bar = bars[barIndex];
        var beat = bar[beatIndex];

        if (barIndex == 0 && beatIndex == 0)
        {
            if(bars.Any(b => b.beat0.userInput || b.beat1.userInput || b.beat2.userInput || b.beat3.userInput))
            {
                PlayerInputCollector.Instance.Clear();
            }
            //KaraokeCanvas.Instance.UpdateText(string.Join(" ", bars.Select(b => $"{b.beat0.Text} {b.beat1.Text} {b.beat2.Text} {b.beat3.Text}")));
        }

        stringBuilder.Clear();

        bool appendedTransparent = false;
        for (int i = 0; i < bars.Length; i++)
        {
            var b = bars[i];

            for (int j = 0; j < 4; j++)
            {
                var bBeat = b[j];

                if (j <= beatIndex && i <= barIndex)
                    stringBuilder.Append(bBeat.Text);
                else
                    stringBuilder.Append(FormatHidden(bBeat.Text));

                if (beatIndex < 4 || barIndex < bars.Length)
                {
                    stringBuilder.Append(' ');
                }

                if (j == beatIndex && i == barIndex)
                {
                    stringBuilder.Append("<color=#00000066>");
                    appendedTransparent = true;
                }

            }
        }
        if (appendedTransparent)
            stringBuilder.Append("</color>");
        KaraokeCanvas.Instance.UpdateText(stringBuilder.ToString());



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
                if(autoNextVerse && !triggeredAnyEvent)
                    onEnqueueVerse(autoNextVerse);
                moveNext = true;
            }
        }
    }
    private string FormatHidden(string str)
    {
        return Regex.Replace(str, ">([^>]+)<", "><color=#00000066>$1</color><");
    }

    public void Prepare()
    {
    }

    public void Enter()
    {
        triggeredAnyEvent = false;
        BackingTrack.Instance.SwitchTrack(track, songClip);
        //KaraokeCanvas.Instance.UpdateText(string.Join(" ", bars.Select(b => $"{b.beat0.text} {b.beat1.text} {b.beat2.text} {b.beat3.text}")));
        MetronomeEvent.onTrigger += MetronomeEvent_onTrigger;
    }

    private void MetronomeEvent_onTrigger(MetronomeEvent evt)
    {
        if(nextVerse.TryGetValue(evt, out var verse))
        {
            triggeredAnyEvent = true;
            onEnqueueVerse(verse);
        }
    }

    public void Exit()
    {
        MetronomeEvent.onTrigger -= MetronomeEvent_onTrigger;
    }
}
