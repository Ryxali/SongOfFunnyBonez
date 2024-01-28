using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpriteEffect : MonoEffect
{
    [SerializeField]
    private new SpriteRenderer renderer;
    [SerializeField]
    private Sprite sprite;

    protected override void OnEvent()
    {
        if (renderer)
            renderer.sprite = sprite;
    }
}
