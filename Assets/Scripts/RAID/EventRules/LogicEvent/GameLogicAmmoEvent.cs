using UnityEngine;
[CreateAssetMenu(fileName = "Game Logic Event", menuName = "Logic Event/Game/Ammo")]
public class GameLogicAmmoEvent : GameLogicEventPrototype
{
    public void Raise(int ammoCount, bool isInf)
    {
        base.Raise(new GameLogicAmmoEventType(
            Mathf.Max(0, ammoCount),
            isInf));
    }
};