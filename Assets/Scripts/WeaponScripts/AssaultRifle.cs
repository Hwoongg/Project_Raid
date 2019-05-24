using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 애니메이터 전환 기능 추가.
// Q 입력시 무기 교체, E 입력시 스킬 발동.
//

public class AssaultRifle : SwitchableWeapon
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Rifle On");
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Q) && WeaponSwitchTimer > WeaponSwitchDelay)
        {
            // 애니메이터 전환.
            animator.SetBool("isSniping", true);

            // 무기 교체
            SwitchWeapons();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            // Skill_RapidFire
            // ...
        }
    }

    void Skill_RapidFire()
    {

    }
}
