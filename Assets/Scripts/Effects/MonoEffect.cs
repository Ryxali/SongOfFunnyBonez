using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoEffect : MonoBehaviour
{
    [SerializeField]
    private MetronomeEvent @event;

    protected virtual void OnEnable()
    {
        MetronomeEvent.onTrigger += MetronomeEvent_onTrigger;
    }

    protected virtual void OnDisable()
    {
        MetronomeEvent.onTrigger -= MetronomeEvent_onTrigger;
    }

    protected abstract void OnEvent();

    private void MetronomeEvent_onTrigger(MetronomeEvent evt)
    {
        if(@event == evt)
        {
            OnEvent();
        }
    }
}
