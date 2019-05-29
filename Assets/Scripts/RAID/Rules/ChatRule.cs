using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChatRule : MonoBehaviour, IChatClientListener, ILogicEvent
{
    [SerializeField] ChatClient ChatClient;
    internal AppSettings AppSettings;
    public string UserName;
    EventSet EventSet;
    Text MessageText;
    InputField MessageInputField;
    [SerializeField] string[] ChannelsToAutoJoin;
    int HistoryLength = 10;

    void OnEnable()
    {
        EventSet = new EventSet(eEventType.FOR_ALL, this);
        LogicEventListener.RegisterEvent(EventSet);
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        AppSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        bool isAppIDpresent = !string.IsNullOrEmpty(AppSettings.AppIdChat);

        if (!isAppIDpresent)
        {
            Dbg.LogE("Chat ID is missing.");
        }

        ChatClient = new ChatClient(this)
        {
            UseBackgroundWorkerForSending = true
        };
        ChatClient.Connect(AppSettings.AppIdChat, "1.0", new Photon.Chat.AuthenticationValues(UserName));

        Dbg.Log($"Connecting as {UserName}");
    }

    void OnDisable()
    {
        LogicEventListener.UnregisterEvent(EventSet);
    }

    void Update()
    {
        if (Utils.IsValid(ChatClient))
        {
            ChatClient.Service();
        }
    }

    public void OnInvoked(eEventMessage msg, params object[] obj)
    {
        switch (msg)
        {
            case eEventMessage.ON_CHAT_UI_LOADED:
                var stage = SceneManager.GetSceneAt(1);
                SceneManager.MoveGameObjectToScene(this.gameObject, stage);
                Dbg.Log($"Chat Rule is successfully moved to the stage1");
                break;
        }
    }

    public void DebugReturn(DebugLevel level, string message)
    {

    }

    public void OnChatStateChange(ChatState state)
    {

    }

    public void OnConnected()
    {
        ChatClient.Subscribe(
            ChannelsToAutoJoin[0],
            0,
            HistoryLength,
            new ChannelCreationOptions { PublishSubscribers = true });
    }

    public void SendChatMessage(string msg)
    {
        ChatClient.PublishMessage(ChannelsToAutoJoin[0], msg);
        MessageInputField.text = "";
        MessageInputField.ActivateInputField();
        MessageInputField.Select();
    }

    public void OnDisconnected()
    {

    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        ChatChannel channel = default;
        ChatClient.TryGetChannel(channelName, out channel);
        MessageText.text = channel.ToStringMessages();
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {

    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        ChatClient.PublishMessage(channels[0], "has Joined the game.");
    }

    public void OnUnsubscribed(string[] channels)
    {

    }

    public void OnUserSubscribed(string channel, string user)
    {

    }

    public void OnUserUnsubscribed(string channel, string user)
    {

    }
}
