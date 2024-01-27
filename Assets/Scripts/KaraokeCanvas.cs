using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KaraokeCanvas : MonoBehaviour
{
    public static KaraokeCanvas Instance { get; private set; }

    [SerializeField]
    private TMP_Text text;

    private void Awake()
    {
        Instance = this;
        text.text = string.Empty;
    }

    public void UpdateText(string content)
    {
        text.text = content;
    }
}
