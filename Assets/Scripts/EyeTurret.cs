using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTurret : MonoBehaviour
{
    [SerializeField] float RotSpeed = 10.0f;

    [SerializeField] Transform EyeTransform;
    Transform Target;

    private void Awake()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    

    // Update is called once per frame
    void Update()
    {
        Quaternion LookDir = Quaternion.LookRotation( Target.position - EyeTransform.position);

        // 회전 전의 EyeTransform의 right 이기 때문에 문제가 발생한다. 사용 불가.
        //LookDir = Quaternion.AngleAxis(-90.0f, EyeTransform.right) * LookDir;

        // 대상을 바라보도록 전환
        EyeTransform.rotation = Quaternion.Slerp(EyeTransform.rotation, LookDir, RotSpeed * Time.deltaTime);
    }
}
