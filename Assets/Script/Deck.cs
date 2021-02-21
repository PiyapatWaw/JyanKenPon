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
}
