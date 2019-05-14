[System.Flags]
public enum eEventType
{
    FOR_SYSTEM = 1 << 0,
    FOR_UI = 1 << 1,
    FOR_ENVIRONMENT = 1 << 2,
    FOR_PLAYER = 1 << 3,
    FOR_ENEMY = 1 << 4,

    FOR_ALL = FOR_SYSTEM | FOR_UI | FOR_ENVIRONMENT | FOR_PLAYER | FOR_ENEMY,
    COUNT = 5
};

public enum eEventMessage : ulong
{
    NONE = 0,

    SPLASH_FULLY_APPEARED,
    FADER_FULLY_APPEARED,

    ON_ANYKEY_PRESSED,
    

    ON_HEALTH_POINT_CHANGED,
    ON_AMMUNITION_COUNT_CHANGED,
    ON_AMMUNITION_INFINITY_SKILL_ACTIVATED,
    ON_REMAINING_TIME_CHANGED,

    ON_MENU_OPENED,
    ON_MENU_CLOSED,
    ON_LOGIN_BUTTON_CLICKED,
    ON_CONNECTED,
};

public interface ILogicEvent
{
    void OnInvoked(eEventMessage msg, params object[] obj);
};

