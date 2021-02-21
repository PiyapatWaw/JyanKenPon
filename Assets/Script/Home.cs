using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class Home : MonoBehaviourPunCallbacks
{
    public StatusPanel StatusPanel;
    public TMP_InputField PlayerName;


    public void CreateRoom()
    {
        if(PlayerName.text=="")
        {
            StatusPanel.Show(PanelType.Error,"Need player name",true);
        }
        else
        {
            StatusPanel.Show(PanelType.Cretae, "Room name", true);
        }
    }

    IEnumerator Create()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom("Test 000",roomOptions,TypedLobby.Default);
        yield return null;
    } 

    public void JoinRoom()
    {
        if (PlayerName.text == "")
        {
            StatusPanel.Show(PanelType.Error, "Need player name", true);
        }
        else
        {
            StatusPanel.Show(PanelType.Join, "Join room", true);
        }
    }

    IEnumerator Join()
    {
        yield return null;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
