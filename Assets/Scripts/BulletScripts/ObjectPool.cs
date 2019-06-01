using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 객체 풀 클래스. 총알에만 해당하여 진행하도록 사용중
//

public class ObjectPool : MonoBehaviour
{

    Bullet[] bullets;

    int Counter;

    private void Awake()
    {
        Counter = 0;
        bullets = GetComponentsInChildren<Bullet>();
    }
    
    

    public void Spawn()
    {
        // 사용 가능한지 체크
        // ...

        // 생성
        // ...

        // 카운터 상승
        // ...
    }
}
