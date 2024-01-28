using UnityEngine;

public class DeactivateComponentEffect : MonoEffect
{
    [SerializeField]
    private MonoBehaviour component;

    protected override void OnEvent()
    {
        component.enabled = false;
    }
}

