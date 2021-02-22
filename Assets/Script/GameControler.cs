using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;

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
    public bool MySelected,OpponentSelect;
    public PhotonView PhotonView;

    void Awake()
    {
        Instanst = this;
    }

    private void Start()
    {
        int i = 0;
        foreach (var playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            playerStats[i].Name.text = playerInfo.Value.NickName;
            i++;
        }
        StartCoroutine(Play());
    }

    public void DeSeLectAll()
    {
        mydeck.DeselectAll();
    }

    public IEnumerator Play()
    {
        while (playerStats.Where(w => w.hp > 0).ToList().Count != 0)
        {
            gameState = GameState.Select;
            yield return new WaitUntil(() => MySelected);
            yield return new WaitUntil(() => OpponentSelect);
            yield return StartCoroutine(OpponentCard.show());
            break;
        }
    }

    public void Check()
    {
        string opponentSelect = OpponentCard.Text.text;
        string myselect = MyCard.Text.text;
        if (myselect!= opponentSelect)
        {

            if(myselect == "Paper" &&opponentSelect == "Hammer")
            {
                
            }
            if (myselect == "Paper" && opponentSelect == "Scissor")
            {

            }

            if (myselect == "Hammer" && opponentSelect == "Paper")
            {

            }
            if (myselect == "Hammer" && opponentSelect == "Scissor")
            {

            }

            if (myselect == "Scissor" && opponentSelect == "Paper")
            {

            }
            if (myselect == "Scissor" && opponentSelect == "Hammer")
            {

            }
        }

        MyCard = null;
    }


    public void SentSelectToOpponent(string select)
    {
        photonView.RPC("SetOpponentCard", RpcTarget.Others, select);
    }


    [PunRPC]
    void SetOpponentCard(string select)
    {
        Debug.LogError("Opponent Select " + select);
        OpponentCard.Text.text = select;
        OpponentSelect = true;
    }
}
