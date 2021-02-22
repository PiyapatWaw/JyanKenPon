using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class PlayerCard : MonoBehaviour
{
    public Player Mydata;
    public TextMeshProUGUI Name;
    public bool Host;

    public void SetData(Player data)
    {
        Mydata = data;
        Name.text = data.NickName;
        Host = data.IsMasterClient;
    }
}
