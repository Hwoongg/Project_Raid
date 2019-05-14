using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIScreenFillBlackGradually : UISplashImageAppearGradually
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    IEnumerator _ScreenFill()
    {
        float alpha = Image.GetAlpha();
        while (alpha <= 1.0f && false == IsMovingNextScene)
        {
            alpha += AlphaIncrease;
            Image.SetAlpha(alpha);
            Dbg.Log($"{alpha.ToString()}");
            yield return Yielder.GetCoroutine(0.03f);
        }
        IsMovingNextScene = true;
        LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.SPLASH_FULLY_APPEARED);
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            // 아무키나 눌리면 다음씬으로 이동하도록 플래그 설정.
            // Set the flag that prevent another update since any key has been pressed.
            case eEventMessage.ON_ANYKEY_PRESSED:
                StartCoroutine(_ScreenFill());
                break;
        }
    }
};
