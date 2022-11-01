using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    [SerializeField] private Transform spawnPoint;
    
    private Vector3 diceOffset;
    private string roomName= "bathroom";
    private string dicePrefabPath = "dicePrefab";
    
    private void Awake()
    {
        diceOffset = new Vector3(3,0,0);
          
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log($"Photon connected successfully {PhotonNetwork.CloudRegion}");

        if (!PhotonNetwork.InRoom) PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log($"OnJoinedRoom {PhotonNetwork.CurrentRoom}");
        Dictionary<int, Player> players = PhotonNetwork.CurrentRoom.Players;
        Vector3 spawnPosition = spawnPoint.position + diceOffset * (players.Count-1);
        PhotonNetwork.Instantiate(dicePrefabPath, spawnPosition, Quaternion.identity);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log($"OnJoinRoomFailed {returnCode} {message}");
       
    }
    
}
