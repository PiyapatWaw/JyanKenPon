using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Home : MonoBehaviourPunCallbacks
{
    public static Home Instanst;
    public StatusPanel StatusPanel;
    public TMP_InputField PlayerName;
    public string gameVersion;
    bool loading = false;

    private void Awake()
    {
        Instanst = this;
        gameVersion = Application.version;
        loading = false;
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void BeforeLoadScene()
    {
        if (loading == false)
        {
            loading = true;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.LoadLevel("Room");
        }
    }

    public void CreateRoom()
    {
        if(PlayerName.text=="")
        {
            StatusPanel.Show(PanelType.Error,"Need player name",true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerName.text;
            StatusPanel.Show(PanelType.Cretae, "Room name", true);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectTomaster");
    }

    public void Create(string RoomName)
    {
        StartCoroutine(StepCreate(RoomName));
    }

    IEnumerator StepCreate(string RoomName)
    {
        yield return new WaitUntil(()=>PhotonNetwork.IsConnected);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(RoomName,roomOptions);
        StatusPanel.SetStatus("Creating");
        
    }

    public override void OnCreatedRoom()
    {
        StatusPanel.SetStatus("Create Success");
        BeforeLoadScene();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        StatusPanel.SetStatus("Creating Fail "+message);
    }

    public void JoinRoom()
    {
        if (PlayerName.text == "")
        {
            StatusPanel.Show(PanelType.Error, "Need player name", true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerName.text;
            StatusPanel.Show(PanelType.Join, "Join room", true);
        }
    }

    public void Join(string RoomName)
    {
        StartCoroutine(StepJoin(RoomName));
    }

    IEnumerator StepJoin(string RoomName)
    {
        yield return new WaitUntil(() => PhotonNetwork.IsConnected);
        PhotonNetwork.JoinRoom(RoomName);
        
    }

    public override void OnJoinedRoom()
    {
        StatusPanel.SetStatus("Join Success");
        BeforeLoadScene();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        StatusPanel.SetStatus("Join fail "+message);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
