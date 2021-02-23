using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card[] Allcrd;

    public void DeselectAll()
    {
        foreach (var item in Allcrd)
        {
            item.Deselect();
        }
    }

    public void MyChoose()
    {
        foreach (var item in Allcrd)
        {
            if(!item.Select)
            {
                Vector2 Target = new Vector2(item.transform.position.x, item.transform.position.y - 50);
                StartCoroutine(item.Move(Target, 0.5f));
            }
        }
    }

    public IEnumerator Reset()
    {
        foreach (var item in Allcrd)
        {
            StartCoroutine(item.Move(item.OriginPos,0.5f));
            item.Select = false;
        }
        Debug.LogError("wait move up");
        yield return new WaitForSeconds(1);
    }
}
