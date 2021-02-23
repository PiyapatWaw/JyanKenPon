using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public string Name;
    public TextMeshProUGUI Text;
    public CanvasGroup CanvasGroup;
    public bool Select;
    public Vector2 OriginPos;
    private void Start()
    {
        Text.text = Name;
        Select = false;
        OriginPos = transform.position;
    }

    public void Selected()
    {
        Debug.LogError("My Select " + Name);
        GameControler.Instanst.mydeck.MyChoose();
        GameControler.Instanst.MySelected = true;
        GameControler.Instanst.MyCard = this;
        GameControler.Instanst.SentSelectToOpponent(Name);
    }

    public void Click()
    {
        if (GameControler.Instanst.gameState != GameState.Select)
            return;
        
        if (Select)
        {
            Vector2 Target = new Vector2(GameControler.Instanst.OpponentCard.transform.position.x, 400);
            StartCoroutine(Move(Target, 1, Selected));
        }
        else
        {
            GameControler.Instanst.DeSeLectAll();
            Vector2 Target = new Vector2(transform.position.x, transform.position.y + 50);
            StartCoroutine(Move(Target, 0.5f));
            Select = true;
        }
    }

    public void Deselect()
    {
        if(Select)
        {
            StartCoroutine(Move(OriginPos, 0.5f));
            Select = false;
        }
    }

    public IEnumerator Move(Vector2 Target,float maxT,System.Action Callback = null)
    {
        float t = 0;
        Vector2 Origin = transform.position;
        while (t/maxT<=1)
        {
            t += Time.deltaTime;
            transform.position = Vector2.Lerp(Origin,Target,t/maxT);
            yield return null;
        }
        Callback?.Invoke();
    }

    public void Show()
    {
        StartCoroutine(show());
    }

    public IEnumerator show()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            CanvasGroup.alpha = t;
            yield return null;
        }
    }

    public IEnumerator hide()
    {
        float t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime;
            CanvasGroup.alpha = t;
            yield return null;
        }
    }
}
