using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneRule : RulePrototype
{
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] Animator[] PlayFlyAwayAnims = new Animator[2];
    /// <summary>
    /// 
    /// </summary>
    bool IsAnyKeyDown = false;

    void Start()
    {
        Dbg.LogCheckAssigned(PlayFlyAwayAnims[0], this);
        Dbg.LogCheckAssigned(PlayFlyAwayAnims[1], this);
    }

    void Update()
    {
        if (Input.anyKeyDown && false == IsAnyKeyDown)
        {
            IsAnyKeyDown = true;
            PlayFlyAwayAnims[0].SetTrigger("FlyAway");
            PlayFlyAwayAnims[1].SetTrigger("FlyAway");
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_ANYKEY_PRESSED);
        }
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.SPLASH_FULLY_APPEARED:
                SceneManager.LoadScene("SampleScene");
                break;
        }
    }
};
