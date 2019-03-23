using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 카메라 워크 스크립트 입니다.
// 플레이어 머리 뒤의 상대좌표를 추적합니다.
// 추적 방식에 따른 모드와 그에 따른 추적 방식 함수가 정의되어 있습니다.
//

public class NewTPSCamera : MonoBehaviour
{

    public enum Mode
    {
        None,
        Normal,
        Jet,
        Free
    }
    [HideInInspector] public Mode mode;

    Transform FollowTarget; // 추적 타겟
    PlayerControll playerControll;

    private Transform CamTransform;


    private void Awake()
    {
        CamTransform = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;

        mode = Mode.Free;

    }


    void Start()
    {
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControll = FollowTarget.GetComponent<PlayerControll>();
    }
    
    

    private void LateUpdate()
    {
        switch(mode)
        {
            case Mode.None:
                break;

            case Mode.Normal:
                NormalStateWork();
                break;
                
            case Mode.Jet:
                JetStateWork();
                break;

            case Mode.Free:
                FreeStateWork();
                break;
        }
    }

    // 기본 모드 카메라 워크입니다.
    void NormalStateWork()
    {
        var newPos = FollowTarget.localToWorldMatrix * new Vector4(0.0f, 3.0f, -3.5f, 1);

        CamTransform.position = newPos;

        CamTransform.LookAt(FollowTarget);

        //CamTransform.forward = Vector3.Normalize(FollowTarget.position - CamTransform.position);
    }

    // 추적모드 카메라 워크입니다.
    void JetStateWork()
    {
        var newPos = FollowTarget.localToWorldMatrix * new Vector4(0.0f, 3.0f, -3.5f, 1);

        CamTransform.position = Vector3.Lerp(CamTransform.position, newPos, Time.fixedDeltaTime * 10.0f);
        
        CamTransform.LookAt(FollowTarget);

        // 오브젝트의 회전만큼 회전시킨다.
        // ...
        

    }

    // 자유시점 카메라 워크입니다.
    void FreeStateWork()
    {

        FreeRotation();
    }

    // 플레이어를 중심에 놓고 회전합니다.
    void FreeRotation()
    {
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        var newPos =
            Quaternion.AngleAxis(v * Time.deltaTime * 10.0f, Vector3.up)
            * Quaternion.AngleAxis(h * Time.deltaTime * 10.0f, Vector3.Cross(CamTransform.forward, Vector3.up))
             * CamTransform.position;

        CamTransform.position = newPos;

        CamTransform.LookAt(FollowTarget);


    }
}
