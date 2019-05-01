using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneRule : RulePrototype
{
    /// <summary>
    /// Logo CanvasRenderer for controlling the alpha.
    /// </summary>
    [SerializeField] CanvasRenderer SplashLogo;
    /// <summary>
    /// UILogicEvent that call the Alpha Appearing Logo.
    /// </summary>
    [SerializeField] UILogicEvent UILogicEvent;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float AlphaIncreaseIntensity = 0.01f;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float Interval = 0.05f;

    void Start()
    {
        bool isReady = CustomDebug.LogCheckAssigned(SplashLogo, this);
        SplashLogo.SetAlpha(0.0f);
        isReady = CustomDebug.LogCheckAssigned(UILogicEvent, this);
        if (true == isReady)
        {
            UILogicEvent.Raise();
        }
    }

    void Dispose()
    {
        Destroy(SplashLogo);
        SplashLogo = null;

        Destroy(this.UILogicEvent);
        UILogicEvent = null;
    }

    void Update()
    {
        // Move to the next scene if any key get down.
        if (Input.anyKeyDown && false == IsMovingNextScene)
        {
            SceneManager.LoadScene("Title");
            IsMovingNextScene = true;
        }
    }

    public void MoveNextScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void AppearSplashLogoGradually()
    {
        if (false == IsMovingNextScene)
        {
            StartCoroutine(_PerformAlphaSplashLogo());
        }
    }

    IEnumerator _PerformAlphaSplashLogo()
    {
        float alpha = SplashLogo.GetAlpha();
        while (alpha <= 1.0f && false == IsMovingNextScene)
        {
            alpha += AlphaIncreaseIntensity;
            SplashLogo.SetAlpha(alpha);
            yield return Yielder.GetCoroutine(Interval);
        }

        if (false == IsMovingNextScene)
        {
            SceneManager.LoadScene("Title");
            IsMovingNextScene = true;
        }
    }
};
