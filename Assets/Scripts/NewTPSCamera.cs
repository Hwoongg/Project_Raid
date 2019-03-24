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
        NONE,
        NORMAL,
        JETFOLLOW,
        FREE
    }
    [HideInInspector] public Mode mode;

    Transform FollowTarget; // 추적 타겟
    Vector3 LookCorrection; // 시점 보정값. 타겟보다 조금 더 위를 바라보도록

    private Transform CamTransform;

    public Transform Anchor;


    private void Awake()
    {
        CamTransform = GetComponent<Transform>();

        Cursor.lockState = CursorLockMode.Locked;

        mode = Mode.FREE;

        LookCorrection.Set(0, 2.0f, 0);

    }


    void Start()
    {
        FollowTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        Anchor.LookAt(FollowTarget.position + LookCorrection);
    }
    
    

    private void LateUpdate()
    {
        switch(mode)
        {
            case Mode.NONE:
                break;

            case Mode.NORMAL:
                NormalStateWork();
                break;
                
            case Mode.JETFOLLOW:
                JetStateWork();
                break;

            case Mode.FREE:
                FreeStateWork();
                break;
        }
    }

    // 기본 모드 카메라 워크입니다.
    void NormalStateWork()
    {
        var newPos = FollowTarget.localToWorldMatrix * new Vector4(0.0f, 3.0f, -3.5f, 1);

        CamTransform.position = newPos;

        CamTransform.LookAt(FollowTarget.position + LookCorrection);

        //CamTransform.forward = Vector3.Normalize(FollowTarget.position - CamTransform.position);
    }

    // 추적모드 카메라 워크입니다.
    void JetStateWork()
    {
        //var newPos = FollowTarget.localToWorldMatrix * new Vector4(0, 1, -6.0f, 1);

        //CamTransform.position = Vector3.Lerp(CamTransform.position, newPos, Time.fixedDeltaTime * 5.0f);

        //CamTransform.LookAt(FollowTarget.transform.position + LookCorrection);

        //// 추적 오브젝트가 얼마나 돌았는지 측정하여 같이 돈다.
        //TargetTrackRotation();

        CamTransform.position = Vector3.Lerp(CamTransform.position, Anchor.position, Time.fixedDeltaTime * 5.0f);

        CamTransform.rotation = Anchor.rotation;


        
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

    void TargetTrackRotation()
    {
        // 각도를 잰다.

        Vector3 TargetHorizontal = Vector3.Cross(Vector3.up, FollowTarget.forward);
        float RotAngle = Vector3.SignedAngle(TargetHorizontal, FollowTarget.right, CamTransform.forward);
        
        // 나도 돈다
        Quaternion rot = Quaternion.AngleAxis(RotAngle, CamTransform.forward);
        CamTransform.rotation = rot * CamTransform.rotation;
        

    }
}
