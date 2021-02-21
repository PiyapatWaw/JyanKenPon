using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum GameState
{
    Initial,
    Select,
    Battle,
    TakeDamage
}
public class GameControler : MonoBehaviour
{
    public static GameControler Instanst;
    public PlayerStat[] playerStats;
    public Deck mydeck;
    public GameState gameState;
    public Card OpponentCard,MyCard;
    public bool Selected;

    void Awake()
    {
        Instanst = this;
    }

    private void Start()
    {
        StartCoroutine(Play());
    }

    public void DeSeLectAll()
    {
        mydeck.DeselectAll();
    }

    public IEnumerator Play()
    {
        while (playerStats.Where(w=> w.hp<=0).ToList().Count == 0)
        {
            gameState = GameState.Select;
            yield return new WaitUntil(() => Selected);
            //set select to server
            //wait server sent opponent select;
            yield return StartCoroutine(OpponentCard.show());
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
}
