using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Bullet Bullet;
    public ParticleSystem Explosion;
    public Transform[] AllBulletOut;
    public float firerate;
    int BulletOutIndex;
    Coroutine FireBehavior;
    bool IsFire;
    
    void Start()
    {
        
    }

    public void StartFire()
    {
        IsFire = true;
    }

    public void EndFire()
    {
        IsFire = false;
    }

    IEnumerator Fire()
    {
        while (IsFire)
        {
            Bullet BC = Instantiate(Bullet);
            BC.transform.eulerAngles = AllBulletOut[BulletOutIndex].eulerAngles;
            BC.Shoot();
            BulletOutIndex = BulletOutIndex == AllBulletOut.Length-1? BulletOutIndex+=1 : 0;
            yield return new WaitForSeconds(firerate);
        }
    }
}
