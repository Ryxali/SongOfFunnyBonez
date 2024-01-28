using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComponentEffect : MonoEffect
{
    [SerializeField]
    private MonoBehaviour component;

    protected override void OnEvent()
    {
        component.enabled = true;
    }
}

