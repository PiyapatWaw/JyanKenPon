using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet Bullet;
    public ParticleSystem Explosion;
    public Transform[] AllBulletOut;
    public float firerate,Magazine;
    public UIcontrol ui;
    [SerializeField]
    private int MaxBullet;
    private Coroutine ReloadCoroutine;

    [SerializeField]
    int BulletOutIndex;
    public bool IsFire;
    
    void Start()
    {
        Magazine = MaxBullet;
    }

    public void StartFire()
    {
        IsFire = true;
    }

    public void EndFire()
    {
        IsFire = false;
    }

    public IEnumerator Fire()
    {
        if (ReloadCoroutine != null)
            StopCoroutine(ReloadCoroutine);
        while (IsFire&& Magazine>0)
        {
            Bullet BC = Instantiate(Bullet);
            BC.transform.eulerAngles = AllBulletOut[BulletOutIndex].eulerAngles;
            BC.transform.position = AllBulletOut[BulletOutIndex].position;
            BC.SetGun(this, BattleTest.Instanst.Allgun.Where(w => w != this).FirstOrDefault(), AllBulletOut[BulletOutIndex].position);
            BC.Shoot();
            BulletOutIndex++;
            if (BulletOutIndex >= AllBulletOut.Length)
                BulletOutIndex = 0;
            Magazine--;
            ui.UpdateBulletBar(MaxBullet,Magazine);
            yield return new WaitForSeconds(firerate);
        }
        ReloadCoroutine = StartCoroutine(Reload());
    }

    public virtual void CreateBullet()
    {

    }

    public IEnumerator Reload()
    {
        while (Magazine < MaxBullet)
        {
            Magazine++;
            ui.UpdateBulletBar(MaxBullet, Magazine);
            yield return new WaitForSeconds(firerate);
        }
    }
}
