using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Prototype of Every LogicEventListener. Get 3 Types
/// </summary>
/// <typeparam name="Ty">Type for specifing what is this event.</typeparam>
/// <typeparam name="EventData">Logic Event that produces data.</typeparam>
/// <typeparam name="UEventResponse">Unity Event that is actually Invoked.</typeparam>
public abstract class LogicEventListenerPrototype<Ty, EventData, UEventResponse> 
    : MonoBehaviour,
    ILogicEventListener<Ty>
    where Ty : IGameLogicEventType
    where EventData : LogicEventPrototype<Ty>
    where UEventResponse : UnityEvent<Ty>
{
    /// <summary>
    /// Whenever Event has been raised this is called.
    /// </summary>
    /// <param name="item">Parameter that is going to transfer to the UnityEvent.</param>
    public void OnEventRaised(Ty item)
    {
        if (Utils.IsValid(Response))
        {
            Response.Invoke(item);
        }
    }
    /// <summary>
    /// Attach Listener.
    /// </summary>
    protected void OnEnable()
    {
        if (Utils.IsValid(LogicEvents))
        {
            int len = LogicEvents.Length;
            for (int i = 0; i < len; ++i)
            {
                LogicEvents[i].AttachListener(this);
            }
        }
    }
    /// <summary>
    /// Detach listener.
    /// </summary>
    protected void OnDisable()
    {
        if (Utils.IsValid(LogicEvents))
        {
            int len = LogicEvents.Length;
            for (int i = 0; i < len; ++i)
            {
                LogicEvents[i].DetachListener(this);
            }
        }
    }

    /// <summary>
    /// Event that produces data to Unity Event(UEventResponse).
    /// </summary>
    [SerializeField] protected EventData[] LogicEvents;
    public EventData[] logicEvent { get { return LogicEvents; } set { LogicEvents = value; } }
    /// <summary>
    /// Actual Event caller that is produced data from LogicEvent(EventData).
    /// </summary>
    [SerializeField] protected UEventResponse Response;
    public UEventResponse response { get { return Response; } set { Response = value; } }
};
