/// <summary>
/// Basic Game Logic Event Type.
/// </summary>
public interface IGameLogicEventType { /**/ };

[System.Serializable]
public struct GameLogicAmmoEventType : IGameLogicEventType
{
    public int AmmoCount;
    public bool IsInfinity;

    public GameLogicAmmoEventType(int ammoCount, bool isInf = false)
    {
        AmmoCount = ammoCount;
        IsInfinity = isInf;
    }
};

[System.Serializable]
public struct GameLogicHealthEventType : IGameLogicEventType
{
    public int CurrentHealthPoint;
    public int InitialHealthPoint;

    public GameLogicHealthEventType(int currentHealthPoint, int initialHealthPoint)
    {
        CurrentHealthPoint = currentHealthPoint;
        InitialHealthPoint = initialHealthPoint;
    }
};

[System.Serializable]
public struct GameLogicTimeEventType : IGameLogicEventType
{
    public float ElapsedTime;
    public float RemainingTime;
    public float TimeScale;

    public GameLogicTimeEventType(float elapsedTime, float remainingTime, float initialTime = 300.0f)
    {
        ElapsedTime = elapsedTime;
        RemainingTime = initialTime;
        TimeScale = remainingTime;
    }
};