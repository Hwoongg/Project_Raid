using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISplashImageAppearGradually : MonoBehaviour, ILogicEvent
{
    protected CanvasRenderer Image;
    [SerializeField, Header("Increasing amount of splash alpha value.")]
    protected float AlphaIncrease = 0.01f;
    
    protected bool IsMovingNextScene = false;
    protected EventSet EventSet;

    protected virtual void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_UI | eEventType.FOR_SYSTEM, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    protected virtual void Start()
    {
        Image = GetComponent<CanvasRenderer>();
        Image.SetAlpha(0.0f);
        StartCoroutine(_SplashImageAppear());
    }

    protected virtual void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    IEnumerator _SplashImageAppear()
    {
        // 한번만 실행되도록 보장.
        // Make secure this logic executes once.
        if (false == IsMovingNextScene)
        {
            float alpha = Image.GetAlpha();
            while (alpha <= 1.0f && false == IsMovingNextScene)
            {
                alpha += AlphaIncrease * Time.deltaTime;
                Image.SetAlpha(alpha);
                yield return Yielder.GetCoroutine(0.05f);
            }

            IsMovingNextScene = true;
            // 씬 규칙으로 메시지 전달.
            // Send the message to the scene rule.
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.SPLASH_FULLY_APPEARED);
        }
    }    

    public virtual void OnInvoked(eEventMessage msg, params object[] obj)
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
