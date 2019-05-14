using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Rule : RulePrototype
{
    public static bool IsMenuOpened { get; set; } = false;
    public static bool IsSceneLoaded { get; set; } = false;

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("Title");
    }

    void Start()
    {
        if (Application.isEditor)
        {
            var loadedLevel = SceneManager.GetSceneByName("Stage1");
            if (true == loadedLevel.isLoaded)
            {
                SceneManager.SetActiveScene(loadedLevel);
                return;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && false == IsMenuOpened)
        {
            IsMenuOpened = true;
            SceneManager.LoadSceneAsync("Stage1Menu", LoadSceneMode.Additive);
            LogicEventListener.Invoke(eEventType.FOR_SYSTEM, eEventMessage.ON_MENU_OPENED);
        }
    }

    //IEnumerator LoadMenu()
    //{
    //    enabled = false;
    //    yield return SceneManager.LoadSceneAsync("Stage1Menu", LoadSceneMode.Additive);
    //    SceneManager.SetActiveScene(SceneManager.GetSceneByName("Stage1Menu"));
    //    enabled = true;
    //}

    void LoadStage()
    {
        if (false == PhotonNetwork.IsMasterClient)
        {
            Dbg.LogE($"Trying to load a level but we are not the master client.");
        }
        PhotonNetwork.LoadLevel("Stage1");
    }

    public override void OnLeaveRoom()
    {
        // OnLeaveRoom for Stage1 is called on Stage1Menu in Stage1Menu scene.
    }

    /// <summary>
    /// Called when a remote player entered the room. This player is already added to the playerlist.
    /// </summary>
    /// <remarks>
    /// If your game starts with a certain number of players, this callback can be useful to check the
    /// Room.playerCount and find out if you can start.
    /// </remarks>
    /// <param name="newPlayer"></param>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Dbg.Log($"Stage1Rule: OnPlayerEnteredRoom() called.");
        Dbg.Log($"{newPlayer.NickName} has joined the game.");
        if (true == PhotonNetwork.IsMasterClient)
        {
            LoadStage();
        }
    }

    /// <summary>
    /// Called when a remote player left the room or became inactive. Check otherPlayer.IsInactive.
    /// </summary>
    /// <remarks>
    /// If another player leaves the room or if the server detects a lost connection, this callback will
    /// be used to notify your game logic.
    /// 
    /// Depending on the room's setup, players may become inactive, which means they may return and retake their spot
    /// in the room. In such cases, the player stays in the Room.Players dictionary.
    /// 
    /// If the player is not just inactive, it gets removed from the Room.Players dictionary, before the callback is called.
    /// </remarks>
    /// <param name="otherPlayer"></param>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Dbg.Log($"Stage1Rule: OnPlayerLeftRoom() called.");
        Dbg.Log($"{otherPlayer.NickName} has lefted the game.");
        if (true == PhotonNetwork.IsMasterClient)
        {
            LoadStage();
        }
    }

    public override void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_MENU_CLOSED:
                IsMenuOpened = false;
                SceneManager.UnloadSceneAsync("Stage1Menu", UnloadSceneOptions.None);
                break;
        }
    }
};
