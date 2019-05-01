using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneRule : RulePrototype
{
    /// <summary>
    /// PressAnyKeys CanvasRenderer for controlling the alpha.
    /// </summary>
    [SerializeField] CanvasRenderer PressAnyKeys;
    [SerializeField] CanvasRenderer PressAnyKeysTransparent;
    [SerializeField] Image ScreenFader;
    /// <summary>
    /// UILogicEvent that call the Flickering PressAnyKeys.
    /// </summary>
    [SerializeField] UILogicEvent UILogicEvent;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float AlphaDescreaseIntensity = 0.03f;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float AlphaIncreaseIntensity = 0.03f;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] float Interval = 0.05f;

    [SerializeField] Animator[] PlayFlyAwayAnims;

    void Start()
    {
        bool isReady = CustomDebug.LogCheckAssigned(UILogicEvent, this);
        isReady = CustomDebug.LogCheckAssigned(PressAnyKeys, this);
        isReady = CustomDebug.LogCheckAssigned(PressAnyKeysTransparent, this);
        CustomDebug.LogCheckAssigned(PlayFlyAwayAnims[0], this);
        CustomDebug.LogCheckAssigned(PlayFlyAwayAnims[1], this);
        CustomDebug.LogCheckAssigned(ScreenFader, this);

        if (true == isReady)
        {
            UILogicEvent.Raise();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && false == IsMovingNextScene)
        {
            IsMovingNextScene = true;
            PlayFlyAwayAnims[0].SetTrigger("FlyAway");
            PlayFlyAwayAnims[1].SetTrigger("FlyAway");
            StartCoroutine(_WaitForAnim());
        }
    }
    
    public void StartFlickeringPressAnyKey()
    {
        if (false == IsMovingNextScene)
        {
            StartCoroutine(_PerformFlickeringPressAnyKey());
        }
    }

    IEnumerator _WaitForAnim()
    {
        float alpha = ScreenFader.color.a;
        bool IsFaded = false;
        while (alpha <= 1.0f)
        {
            alpha += AlphaIncreaseIntensity * 0.4f;
            ScreenFader.color = new Color(0.0f, 0.0f, 0.0f, alpha);
            yield return Yielder.GetCoroutine(0.045f);
        }

        SceneManager.LoadScene("SampleScene");
    }

    IEnumerator _PerformFlickeringPressAnyKey()
    {
        float alpha = PressAnyKeys.GetAlpha();
        while (false == IsMovingNextScene)
        {
            while (alpha >= 0.0f && false == IsMovingNextScene)
            {
                alpha -= AlphaDescreaseIntensity;
                PressAnyKeys.SetAlpha(alpha);
                PressAnyKeysTransparent.SetAlpha(alpha);
                yield return Yielder.GetCoroutine(Interval);
            }

            while (alpha <= 1.0f && false == IsMovingNextScene)
            {
                alpha += AlphaIncreaseIntensity;
                PressAnyKeys.SetAlpha(alpha);
                PressAnyKeysTransparent.SetAlpha(alpha);
                yield return Yielder.GetCoroutine(Interval);
            }
        }
    }
};
