using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 쏘는 부분 재정의 하여 뒤로 밀리도록 해야함.
// 애니메이터 전환 기능.
// UI 전환 기능.
// Enable 시 저격 UI 호출, Disable시 복귀.
// 이동 기능 제어 기능. PlayerController 자체를 비활성화 하면 시점 회전도 잠김에 유의.
//

public class SniperRifle : SwitchableWeapon
{
    // 움직임 제어를 위한 컨트롤러 참조
    NewController playerController;

    // 반동 움직임 관련 변수들
    Transform playerTransform;
    [SerializeField]
    Vector3 ReboundPower = new Vector3(0, 0, -1);
    float Deceleration = 5.0f;
    Vector3 NowRebounding = Vector3.zero;



    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("Sniping On");
        timer = 1.0f;

        playerController.mode = NewController.Mode.SNIPING;
    }

    protected override void Awake()
    {
        base.Awake();
        playerController = FindObjectOfType<NewController>();
        playerTransform = GameObject.Find("Player").transform;
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
        timer += Time.deltaTime;

        
        if (Input.GetMouseButton(0) && (Time.timeScale != 0))
        {
            
            if (timer >= timeBetweenBullets)
            {
                Fire();
            }
        }
        else
        {
            animator.SetBool("onFire", false);
        }

        if (timer >= timeBetweenBullets * effectDisplayTime)
        {
            DisableEffects();
        }



        // 뒤로 밀림 효과
        NowRebounding = NowRebounding - (NowRebounding * Deceleration * Time.deltaTime);
        playerTransform.Translate(NowRebounding);

        if (Input.GetKeyDown(KeyCode.Q) && WeaponSwitchTimer > WeaponSwitchDelay)
        {
            // 애니메이터 전환.
            animator.SetBool("isSniping", false);

            // 무기 교체
            SwitchWeapons();
            
        }

        WeaponSwitchTimer += Time.deltaTime;
    }

    // 뒤로 밀림 가속 기능 추가 재정의.
    // 애니메이터 조작 관련 개선 필요. Weapon의 Fire와 대조할 것.
    protected override void Fire()
    {
        base.Fire();

        animator.SetBool("onFire", true);

        // 뒤로 밀림 가속
        NowRebounding += ReboundPower;
        
    }
}
