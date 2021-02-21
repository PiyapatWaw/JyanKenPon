using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    int maxHp;
    public int hp;
    public Image HpBar;
    public TextMeshProUGUI Name;

    void Start()
    {
        maxHp = 3;
        hp = maxHp;
        //recive
        Name.text = "";
    }


    public void TakeDamage()
    {
        hp -= 1;
        HpBar.fillAmount = hp/maxHp;
    }
}
