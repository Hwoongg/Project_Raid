﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Stage1MenuRule : RulePrototype
{
    /// <summary>
    /// Exit button that bring us back to the lobby scene.
    /// </summary>
    [SerializeField] Button Exit;
    public static bool IsExitButtonClicked { get; set; } = false;
    /// <summary>
    /// Exit button that close the menu overlapping the stage1.
    /// </summary>
    [SerializeField] Button CloseMenu;
    public static bool IsCloseMenuClicked { get; set; } = false;

    void Start()
    {
        IsExitButtonClicked = false;
        IsCloseMenuClicked = false;
        // Error check.
        Dbg.LogCheckAssigned(Exit, this);
        Dbg.LogCheckAssigned(CloseMenu, this);

        if (Utils.IsValid(Exit))
        {
            // Add listener to onClick.
            Exit.onClick.AddListener(OnLeaveRoom);
        }

        if (Utils.IsValid(CloseMenu))
        {
            CloseMenu.onClick.AddListener(OnCloseMenuClicked);
        }

        System.GC.Collect();
    }

    void Update()
    {
        // If 'Escape' key is pressed down
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // regard The menu is closed.
            OnCloseMenuClicked();
        }
    }

    public override void OnLeaveRoom()
    {
        if (false == IsExitButtonClicked)
        {
            IsExitButtonClicked = true;
            base.OnLeaveRoom();
        }
    }

    void OnCloseMenuClicked()
    {
        if (false == IsCloseMenuClicked)
        {
            IsCloseMenuClicked = true;
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_MENU_CLOSED);
        }
    }
    

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {

    }
}
