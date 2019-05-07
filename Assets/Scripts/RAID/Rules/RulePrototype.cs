using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RulePrototype : MonoBehaviour, ILogicEvent
{
    /// <summary>
    /// Is already moving the nextscene?
    /// </summary>
    protected bool IsMovingNextScene = false;

    EventSet EventSet;

    public virtual void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    public abstract void OnInvoked(eEventMessage msg, params object[] obj);

    public virtual void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }
};
