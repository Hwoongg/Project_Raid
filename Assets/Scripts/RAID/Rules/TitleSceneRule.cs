using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Photon.Pun;
using MEC;

public class TitleSceneRule : RulePrototype
{
    [SerializeField] UIUsername UserName;
    [SerializeField] UIPassword Password;
    /// <summary>
    /// 
    /// </summary>
    [SerializeField] Animator[] FlyAwayAnims = new Animator[2];
    /// <summary>
    /// 
    /// </summary>
    bool IsAnyKeyDown = false;

    void Start()
    {
        Dbg.LogCheckAssigned(UserName, this);
        Dbg.LogCheckAssigned(Password, this);
        Dbg.LogCheckAssigned(FlyAwayAnims[0], this);
        Dbg.LogCheckAssigned(FlyAwayAnims[1], this);
    }

    //void Update()
    //{
    //    //if (Input.anyKeyDown && false == IsAnyKeyDown)
    //    //{
    //    //    IsAnyKeyDown = true;
    //    //    FlyAwayAnims[0].SetTrigger("FlyAway");
    //    //    FlyAwayAnims[1].SetTrigger("FlyAway");
    //    //    LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_ANYKEY_PRESSED);
    //    //}
    //}

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_LOGIN_BUTTON_CLICKED:
                OnLogin(true);
                break;

            case eEventMessage.FADER_FULLY_APPEARED:
                PhotonNetwork.LoadLevel("Stage1");
                break;
        }
    }

    void OnLogin(bool isValid)
    {
        FlyAwayAnims[0].SetTrigger("FlyAway");
        FlyAwayAnims[1].SetTrigger("FlyAway");
        LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_ANYKEY_PRESSED);

        //if (isValid)
        //{
        //
        //}
        //else
        //{
        //
        //}
    }
};
