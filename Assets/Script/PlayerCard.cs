using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public bool Host;

    public void SetData(string name,bool IsHost)
    {
        Name.text = name;
        Host = IsHost;
    }
}
