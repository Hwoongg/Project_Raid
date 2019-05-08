using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ConnectRule : MonoBehaviour
{
    /// <summary>
    /// Indicate the game version. just keep this value "1".
    /// </summary>
    public string GameVersion { get; set; } = "1";

    void Awake()
    {
        // This makes sure we can use PhotonNetwork.LoadLevel() on the master client
        // and all clients in the same room sync their level automatically.
        //
        // If AutomaticallySyncScene is true, Master client can call PhotonNetwork.Loadlevel().
        // and every connected players automatically will load the same level.
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        // If we are not connected, initiate the connection to the server.
        if (true == PhotonNetwork.IsConnected)
        {
            // We need to attempt joinging a random room.
            //if it fails we'll get notified in 'OnJoinRandomFailed()' and we'll create new one.
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // We first must connect to Photon online server.
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
};
