using UnityEngine;

[CreateAssetMenu(fileName = "Game Logic Event", menuName = "Logic Event/Game/Time")]
public class GameLogicTimeEvent : GameLogicEventPrototype
{
    public void Raise(float elapsedTime, float remainingTime, float initialTime)
    {
        base.Raise(new GameLogicTimeEventType(
            Mathf.Max(0.0f, elapsedTime),
            Mathf.Max(0.0f, remainingTime),
            Mathf.Max(0.0f, initialTime)));
    }
};
