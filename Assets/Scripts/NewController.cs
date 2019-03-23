using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 플레이어의 회전, 이동을 담당하는 컴포넌트 스크립트 입니다.
// 컨트롤러 모드에 따라 카메라 워크 컴포넌트의 모드도 제어됩니다. 
//

public class NewController : MonoBehaviour
{
    enum Mode
    {
        Normal,
        Jet
    }
    Mode mode;

    [SerializeField]
    Transform CameraTransform;
    NewTPSCamera TPSCam;

    Transform PlayerTransform;
    Vector3 MoveVector; // 이동량 벡터
    Vector3 MoveDirection;

    [SerializeField] float MaxSpeed = 1.0f;
    [SerializeField] float Acceleration = 0.1f; // 가속도
    [SerializeField] float Deceleration = 0.5f; // 감속도

    [SerializeField]
    float XaxisSpeed = 30.0f, YaxisSpeed = 30.0f;
    float X, Y;

    [SerializeField]
    float MinAngle = -45f, MaxAngle = 45f;

    private void Awake()
    {
        mode = Mode.Normal;
        MoveVector = Vector3.zero;
    }

    void Start()
    {
        PlayerTransform = GetComponent<Transform>();
        TPSCam = CameraTransform.GetComponent<NewTPSCamera>();
    }
    
    void Update()
    {
        NormalStateControll();
    }

    // 일반 상태 회전 + 이동 기능입니다.
    void NormalStateControll()
    {
        // 카메라 모드 제어
        TPSCam.mode = NewTPSCamera.Mode.Jet;

        // 일반 상태 회전
        NormalStateRotation();

        // 일반 상태 이동
        NormalStateMovement();

    }

    void NormalStateRotation()
    {
        //
        // 입력부
        //
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        X += v * XaxisSpeed * Time.deltaTime;
        Y -= h * YaxisSpeed * Time.deltaTime;

        Y = Mathf.Clamp(Y, MinAngle, MaxAngle);


        // 
        // 처리부
        //
        var rot = Quaternion.Euler(Y, X, 0);

        PlayerTransform.rotation = Quaternion.Euler(Y, X + 180f, 0);
    }

    
    void NormalStateMovement()
    {
        // ////////////////////////////////////////////////////
        //
        // 카메라 기준 방향벡터를 이용한 이동변환을 시행합니다.
        // 전역-모델 좌표계 전환에 유의하여 사용해야 합니다.
        // 
        // ////////////////////////////////////////////////////


        //
        // 입력부
        //
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // 월드 기준 벡터들입니다. 이동 시 모델좌표계로 사상시켜야 함.
        Vector3 worldY = Input.GetKey(KeyCode.Space) ? new Vector3(0, 1, 0) : new Vector3(0, 0, 0);
        Vector3 cameraX = CameraTransform.right * x;
        Vector3 cameraZ = CameraTransform.forward * z;

        Vector3 Movement = cameraX + cameraZ + worldY;

        Movement.Normalize();

        Movement = PlayerTransform.worldToLocalMatrix * Movement;

        MoveVector += Movement * Acceleration * Time.deltaTime;

        MoveVector = MoveVector - (MoveVector * Deceleration * Time.deltaTime);

        PlayerTransform.Translate(MoveVector);
    }

    void JetStateControll()
    {
        //
        // 입력부
        // 오브젝트 기준 회전입니다.
        // 마우스 좌우 : Z축 회전
        // 마우스 상하 : X축 회전
        // 키보드 좌우 : Y축 회전
        // Z축 회전 시 카메라도 같이 회전이 이루어짐.
        // 이동 자체는 오브젝트의 정면으로만 진행합니다.
        //

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
    }
}
