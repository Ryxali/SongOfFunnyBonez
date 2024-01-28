using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class KaraokeCanvas : MonoBehaviour
{
    public static KaraokeCanvas Instance { get; private set; }

    [SerializeField]
    private TMP_Text text;

    private string currentText;
    private void Awake()
    {
        Instance = this;
        text.text = string.Empty;
    }

    public void UpdateText(string content)
    {
        currentText = content;
    }

    private void LateUpdate()
    {
        if (!string.IsNullOrEmpty(currentText))
        {
            text.text = Format();
        }
    }

    private string Format()
    {
        return Regex.Replace(currentText, @"%(_*)([^%]*)%", ev =>
        {
            return "<color=#000000FF>" + PlayerInputCollector.Instance.Current.PadRight(ev.Groups[1].Value.Length, '_') + ev.Groups[2].Value + "</color>";
        });
    }
}
