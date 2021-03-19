using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTest : MonoBehaviour
{
    public static BattleTest Instanst;
    public Gun[] Allgun;
    public Transform[] CamerPoint;
    public UIcontrol UI;

    private void Awake()
    {
        Instanst = this;
    }

}
