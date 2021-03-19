using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Translate;
    public float speed;
    public bool IsShoot;
    public ParticleSystem Explosion;
    public BoxCollider BoxCollider;
    public Gun myGun,Target;
    public Vector3 bulletout;
    void Start()
    {
        
    }

    public void SetGun(Gun gun,Gun Enemy,Vector3 point)
    {
        myGun = gun;
        Target = Enemy;
        bulletout = point;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsShoot)
        transform.Translate(Translate * speed * Time.deltaTime);
    }

    public virtual void Shoot()
    {
        Translate = Vector3.forward;
        IsShoot = true;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Gun>()!=null && other.GetComponent<Gun>()!=myGun)
        {
            IsShoot = false;
            Instantiate(Explosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
