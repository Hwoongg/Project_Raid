using UnityEngine;

[CreateAssetMenu(fileName = "Game Logic Event", menuName = "Logic Event/Game/Health")]
public class GameLogicHealthEvent : GameLogicEventPrototype
{
    public void Raise(int currentHealthPoint, int initialHealthPoint)
    {
        base.Raise(new GameLogicHealthEventType(
            Mathf.Max(0, currentHealthPoint),
            Mathf.Max(0, initialHealthPoint)));
    }
};

