using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 Translate;
    public float speed;
    public bool IsShoot;
    void Start()
    {
        
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
}
