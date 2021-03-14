using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontrol : MonoBehaviour
{
    public static UIcontrol Instanst;
    public Gun MyGun;
    public Image BulletBar;
    Coroutine FireCoroutine;

    private void Awake()
    {
        //Instanst = this;
    }

    public void UpdateBulletBar(float max,float current)
    {
        BulletBar.fillAmount = current / max;
    }

    public void OnTouch()
    {
        MyGun.IsFire = true;
        FireCoroutine = StartCoroutine(MyGun.Fire());
    }

    public void OnUnTouch()
    {
        MyGun.IsFire = false;
    }
}
