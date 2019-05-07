using UnityEngine;

[CreateAssetMenu(fileName = "UI Logic Event", menuName = "Logic Event/UI")]
public class UILogicEvent : LogicEventPrototype<UILogicEventType>
{
    public void Raise()
    {
        base.Raise(new UILogicEventType());
    }
};
