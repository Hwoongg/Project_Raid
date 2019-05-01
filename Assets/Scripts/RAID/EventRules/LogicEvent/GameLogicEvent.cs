using UnityEngine;

[CreateAssetMenu(fileName = "Game Logic Event", menuName = "Logic Event/Game")]
public class GameLogicEventPrototype : LogicEventPrototype<IGameLogicEventType>
{
    public override void Raise(IGameLogicEventType item)
    {
        base.Raise(item);
    }
};
