using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Photon.Pun;
using TMPro;


public enum GameState
{
    Initial,
    Select,
    Battle,
    TakeDamage
}
public class GameControler : MonoBehaviourPunCallbacks
{
    public static GameControler Instanst;
    public PlayerStat[] playerStats;
    public Deck mydeck;
    public GameState gameState;
    public Card OpponentCard,MyCard;
    public bool MySelected,OpponentSelect,TakeDamageComplete;
    public PhotonView PhotonView;
    public int playercount;
    public GameObject LoadingObj;
    public TextMeshProUGUI playercountText,WinnerName;
    public GameObject EndGroup;

    void Awake()
    {
        playercount = 0;
        Instanst = this;
    }

    private IEnumerator Start()
    {
        int i = 0;
        SentPlayerReady();
        foreach (var playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            playerStats[i].Name.text = playerInfo.Value.NickName;
            i++;
        }
        yield return new WaitUntil(()=> playercount==2);
        LoadingObj.SetActive(false);
        StartCoroutine(Play());
    }

    public void UpdatePlayerCount()
    {
        playercount++;
        playercountText.text = string.Format("waiting : {0} / {1}",playercount,2);
    }

    public void DeSeLectAll()
    {
        mydeck.DeselectAll();
    }

    public IEnumerator Play()
    {
        while (playerStats.Where(w => w.hp > 0).ToList().Count > 1)
        {
            TakeDamageComplete = false;
            Debug.LogError("StartRound");
            gameState = GameState.Select;
            Debug.LogError("WaitSelect");
            yield return new WaitUntil(() => MySelected);
            yield return new WaitUntil(() => OpponentSelect);
            gameState = GameState.Battle;
            Debug.LogError("Battle");
            yield return StartCoroutine(OpponentCard.show());
            gameState = GameState.TakeDamage;
            Debug.LogError("TakeDamage");
            if (PhotonNetwork.LocalPlayer.IsMasterClient)
                Check();
            yield return new WaitUntil(() => TakeDamageComplete);
            Debug.LogError("Reset");
            StartCoroutine(OpponentCard.hide());
            yield return StartCoroutine(mydeck.Reset());
            MySelected = false;
            OpponentSelect = false;
            Debug.LogError("EndRound");
        }
        Debug.LogError("-----------------------------------------");
        WinnerName.text = playerStats.Where(w => w.hp > 0).FirstOrDefault().Name.text;
        EndGroup.SetActive(true);
    }


    public void Check()
    {
        string opponentSelect = OpponentCard.Text.text;
        string myselect = MyCard.Text.text;
        string loser = "";
        if (myselect!= opponentSelect)
        {

            if(myselect == "Paper" &&opponentSelect == "Hammer")
            {
                loser = playerStats[1].Name.text;
            }
            if (myselect == "Paper" && opponentSelect == "Scissor")
            {
                loser = playerStats[0].Name.text;
            }

            if (myselect == "Hammer" && opponentSelect == "Paper")
            {
                loser = playerStats[0].Name.text;
            }
            if (myselect == "Hammer" && opponentSelect == "Scissor")
            {
                loser = playerStats[1].Name.text;
            }

            if (myselect == "Scissor" && opponentSelect == "Paper")
            {
                loser = playerStats[1].Name.text;
            }
            if (myselect == "Scissor" && opponentSelect == "Hammer")
            {
                loser = playerStats[0].Name.text;
            }
        }
        if (loser != "")
        { SentPlayerTakeDamage(loser); }
        else
        {
            SentDraw();
        }
        MyCard = null;
    }

    public void Home()
    {
        SceneManager.LoadScene("Home");
    }


    public void SentSelectToOpponent(string select)
    {
        photonView.RPC("SetOpponentCard", RpcTarget.OthersBuffered, select);
    }

    public void SentPlayerTakeDamage(string select)
    {
        photonView.RPC("PlayerTakeDamage", RpcTarget.AllBuffered, select);
    }

    public void SentPlayerReady()
    {
        photonView.RPC("PlayerReady", RpcTarget.AllBuffered);
    }

    public void SentDraw()
    {
        photonView.RPC("DrawResult", RpcTarget.AllBuffered);
    }


    [PunRPC]
    void SetOpponentCard(string select)
    {
        Debug.LogError("Opponent Select " + select);
        OpponentCard.Text.text = select;
        OpponentSelect = true;
    }
    [PunRPC]
    void PlayerTakeDamage(string name)
    {
        playerStats.Where(w => w.Name.text == name).FirstOrDefault().TakeDamage();
        TakeDamageComplete = true;
        Debug.LogError("PlayerTakeDamage " + name + "TakeDamageComplete");
    }

    [PunRPC]
    void PlayerReady()
    {
        Debug.LogError("PlayerReady Update");
        UpdatePlayerCount();
    }

    [PunRPC]
    void DrawResult()
    {
        Debug.LogError("Draw");
        TakeDamageComplete = true;
    }
}
