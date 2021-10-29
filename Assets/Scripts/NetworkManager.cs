using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager instance;
    public GameObject button;

    private void Awake()
    {
        instance = this;
        PhotonNetwork.AutomaticallySyncScene = true;
        button.SetActive(false);
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected) Connect();
    }

    public override void OnConnectedToMaster()
    {
        button.SetActive(true);
    }

    public void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Play()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room");
        // TODO: Check if rooms are available
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Succesfully joined a room");
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(1);
        }
    }

    void Update()
    {

    }
}
