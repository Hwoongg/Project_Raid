using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneRule : RulePrototype
{
    ///// <summary>
    ///// PressAnyKeys CanvasRenderer for controlling the alpha.
    ///// </summary>
    
    //[SerializeField] Image ScreenFader;
    
    ///// <summary>
    ///// 
    ///// </summary>
    
    ///// <summary>
    ///// 
    ///// </summary>
    //[SerializeField] float Interval = 0.05f;

    //[SerializeField] Animator[] PlayFlyAwayAnims;

    //void Start()
    //{
    //    CustomDebug.LogCheckAssigned(PlayFlyAwayAnims[0], this);
    //    CustomDebug.LogCheckAssigned(PlayFlyAwayAnims[1], this);
    //    CustomDebug.LogCheckAssigned(ScreenFader, this);
    //}

    //void Update()
    //{
    //    if (Input.anyKeyDown && false == IsMovingNextScene)
    //    {
    //        IsMovingNextScene = true;
    //        PlayFlyAwayAnims[0].SetTrigger("FlyAway");
    //        PlayFlyAwayAnims[1].SetTrigger("FlyAway");
    //        StartCoroutine(_WaitForAnim());
    //    }
    //}

    //IEnumerator _WaitForAnim()
    //{
    //    float alpha = ScreenFader.color.a;
    //    bool IsFaded = false;
    //    while (alpha <= 1.0f)
    //    {
    //        alpha += AlphaIncrease * 0.4f;
    //        ScreenFader.color = new Color(0.0f, 0.0f, 0.0f, alpha);
    //        yield return Yielder.GetCoroutine(0.045f);
    //    }

    //    SceneManager.LoadScene("SampleScene");
    //}

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {

    }
};
