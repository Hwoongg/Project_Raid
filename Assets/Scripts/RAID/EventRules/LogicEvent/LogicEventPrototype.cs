using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Logic Event Prototype that is callable!
/// Listeners are automatically added or removed.
/// </summary>
/// <typeparam name="Ty"></typeparam>
public abstract class LogicEventPrototype<Ty> : ScriptableObject
    //TODO: Addd the event type that support multiple events.
    where Ty : IGameLogicEventType
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    public virtual void Raise(Ty item)
    {
        for (int i = 0; i < Listeners.Count; ++i)
        {
            Listeners[i].OnEventRaised(item);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="listener"></param>
    public void AttachListener(ILogicEventListener<Ty> listener)
    {
        if (false == Listeners.Contains(listener))
        {
            Listeners.Add(listener);
        }
    }
    public void DetachListener(ILogicEventListener<Ty> listener)
    {
        if (true == Listeners.Contains(listener))
        {
            Listeners.Remove(listener);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    readonly List<ILogicEventListener<Ty>> Listeners = new List<ILogicEventListener<Ty>>();    
};
