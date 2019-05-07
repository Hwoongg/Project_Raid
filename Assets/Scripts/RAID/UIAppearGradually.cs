using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIAppearGradually : MonoBehaviour, ILogicEvent
{
    CanvasRenderer SplashLogo;
    [SerializeField, Header("Increasing amount of splash alpha value.")]
    float AlphaIncrease = 0.01f;
    
    bool IsMovingNextScene = false;
    EventSet EventSet;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        SplashLogo = GetComponent<CanvasRenderer>();
        SplashLogo.SetAlpha(0.0f);
    }
    
    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Update()
    {
        // 한번만 실행되도록 보장.
        // Make secure this logic executes once.
        if (false == IsMovingNextScene)
        {
            float alpha = SplashLogo.GetAlpha();
            float counter = 0.0f;
            while (alpha <= 1.0f && false == IsMovingNextScene)
            {
                alpha += AlphaIncrease * Time.deltaTime;
                SplashLogo.SetAlpha(alpha);
                while (counter <= 0.5f)
                {
                    counter += Time.time;
                    CustomDebug.Log($"{counter.ToString()}");
                }
            }

            IsMovingNextScene = true;
            // 씬 규칙으로 메시지 전달.
            // Send the message to the scene rule.
            LogicEventListener.Invoke(eEventType.FOR_UI, eEventMessage.SPLASH_FULLY_APPEARED);
        }
    }

    void ILogicEvent.OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            // 아무키나 눌리면 다음씬으로 이동하도록 플래그 설정.
            // Set the flag that prevent another update since any key has been pressed.
            case eEventMessage.ON_ANYKEY_PRESSED:
                IsMovingNextScene = true;
                break;
        }
    }
};
