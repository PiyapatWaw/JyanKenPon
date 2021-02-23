using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

namespace JyanKenPon
{
    public class Room : MonoBehaviourPunCallbacks
    {
        public PlayerCard[] playerCards;
        public GameObject GoBTN;
        public Player Mydata;
        public TextMeshProUGUI RoomName;
        public GameObject LoadingObj;
        bool loading;
        private IEnumerator Start()
        {
            loading = false;
            yield return new WaitUntil(()=>PhotonNetwork.IsConnected);
            Mydata = PhotonNetwork.LocalPlayer;
            SetUpPlayer();
            RoomName.text = PhotonNetwork.CurrentRoom.Name;
            GoBTN.SetActive(Mydata.IsMasterClient);
        }

        public void SetUpPlayer()
        {
            if (loading)
                return;
            foreach (var item in playerCards)
            {
                item.gameObject.SetActive(false);
            }
            int i = 0;
            foreach (var playerInfo in  PhotonNetwork.CurrentRoom.Players)
            {
                playerCards[i].SetData(playerInfo.Value);
                playerCards[i].gameObject.SetActive(true);
                i++;
            }
            /*for (int i = 0; i <  in ; i++)
            {
               
            } */    
        }

        public void Go()
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            LoadingObj.SetActive(true);
            loading = true;
            PhotonNetwork.LoadLevel("Play");
        }

        public void Leave()
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("Home");
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("PlayerEnter"+newPlayer.NickName);
            SetUpPlayer();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.Log("PlayerLeft");
            SetUpPlayer();
        }



    }
}
