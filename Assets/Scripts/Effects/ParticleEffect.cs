using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoEffect
{
    [SerializeField]
    private new ParticleSystem particleSystem;
    protected override void OnEvent()
    {
        if (particleSystem)
        {
            particleSystem.Play();
        }
    }
}
