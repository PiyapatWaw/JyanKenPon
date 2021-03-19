using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    // Start is called before the first frame update
    public AnimationCurve curve;
    public float maxt,t;
    public Vector3 startpos;
    public Vector3 beforepos;
    void Start()
    {
        maxt = Vector3.Distance(transform.position,Target.transform.position) / speed;
        t = 0;
        startpos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(IsShoot)
        {
            t += Time.deltaTime;
            var pos = transform.position;
            pos.z = Mathf.Lerp(startpos.z , Target.transform.position.z, t / maxt);
            pos.x = Mathf.Lerp(startpos.x, Target.transform.position.x, t / maxt);
            pos.y = curve.Evaluate(t/maxt)*200f;
            transform.position = pos;

            Vector3 dir = pos - beforepos;
            Vector3 EA = transform.eulerAngles;
            EA.x = Mathf.Atan2(dir.y,dir.z)*Mathf.Rad2Deg;
            transform.eulerAngles = EA;

            beforepos = pos;
            //
        }
        
    }


    public Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
