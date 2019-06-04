﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 플레이어 단순 추적 미사일 스크립트.
//

public class Missile : Bullet
{
    [SerializeField] GameObject objExplotionEfx;
    [SerializeField] float RotSpeed = 10.0f;

    Transform Target;

    float timer;

    
    public override void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        Dbg.LogCheckAssigned(Target, this);
        timer = 0;
    }
    
    public override void Update()
    {
        
        timer += Time.deltaTime;

        if (Utils.IsNull(Target))
        {
            return;
        }

        if (timer > 2.0f)
        {
            Quaternion LookDir = Quaternion.LookRotation(Target.position - transform.position);

            // 대상을 바라보도록 전환
            transform.rotation = Quaternion.Slerp(transform.rotation, LookDir, RotSpeed * Time.deltaTime);
        }

        // 전진 이동
        transform.Translate(transform.forward * Speed * Time.deltaTime, Space.World);

    }

    public override void Explotion()
    {
        GameObject.Instantiate(objExplotionEfx, gameObject.transform.position, Quaternion.identity);
        base.Explotion();
    }

    public override void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>().TakeDamage(5);
        Explotion();
    }
}
