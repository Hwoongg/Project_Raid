using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 구체형 포탑에 사용되는 스크립트.
//

public class EyeTurret : MonoBehaviour
{
    // //////////////////////////////////////////////
    //
    // 눈알 회전에 관련된 변수들
    //
    // /////////////////////////////////////////////

    // 회전 속도
    [SerializeField] float RotSpeed = 10.0f;

    // 눈알 오브젝트
    [SerializeField] Transform EyeTransform;

    // 조준 대상
    Transform Target;
    

    // /////////////////////////////////////////////
    //
    // 포탄 발사에 관련된 변수들
    //
    // /////////////////////////////////////////////

    Muzzle[] FirePoints; // 발사 지점 오브젝트에 Muzzle 컴포넌트가 있어야 한다
    float ReloadTime;



    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        FirePoints = GetComponentsInChildren<Muzzle>();
    }
    
    
    void Update()
    {
        Quaternion LookDir = Quaternion.LookRotation( Target.position - EyeTransform.position);

        // 회전 전의 EyeTransform의 right 이기 때문에 문제가 발생한다. 사용 불가.
        //LookDir = Quaternion.AngleAxis(-90.0f, EyeTransform.right) * LookDir;

        // 대상을 바라보도록 전환
        EyeTransform.rotation = Quaternion.Slerp(EyeTransform.rotation, LookDir, RotSpeed * Time.deltaTime);
    }

    void Fire()
    {

    }
}
