using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1SceneRule : RulePrototype
{
    [SerializeField] GameLogicTimeEvent TimeEvent;
    public GameLogicTimeEvent GetTimeEvent { get { CustomDebug.LogCheckAssigned(TimeEvent, this); return TimeEvent; } }

    Coroutine CoroutineTimeEvent;

    void Start()
    {
        CustomDebug.LogCheckAssigned(TimeEvent, this);
        CoroutineTimeEvent = StartCoroutine(_InvokeTimeEvent());
    }

    IEnumerator _InvokeTimeEvent()
    {
        while (true)
        {
            TimeEvent.Raise(new GameLogicTimeEventType(0.0f, 0.0f, 300));
            yield return Yielder.GetCoroutine(1.0f);
        }
    }
};
