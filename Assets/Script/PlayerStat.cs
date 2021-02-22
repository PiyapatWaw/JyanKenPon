using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStat : MonoBehaviour
{
    int maxHp = 3;
    public int hp;
    public Image HpBar;
    public TextMeshProUGUI Name;

    private void Awake()
    {
        hp = maxHp;
    }


    public void TakeDamage()
    {
        hp -= 1;
        HpBar.fillAmount = hp/maxHp;
    }
}
