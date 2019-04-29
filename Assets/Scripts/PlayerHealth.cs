﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{

    [SerializeField] Image damageImage;
    [SerializeField] float flashSpeed = 0.5f;
    [SerializeField] Color flashColor = new Color(1f, 0f, 0f, 0.2f);
    [SerializeField] GameObject objBarrier;

    
    void Update()
    {
        // 플레이어용 화면 피격 이펙트 연출
        if (damaged)
        {
            damageImage.color = flashColor;
            objBarrier.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            objBarrier.SetActive(true);

        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            objBarrier.transform.localScale = Vector3.Lerp(objBarrier.transform.localScale, new Vector3(3,3,3), Time.deltaTime * 20.0f);
        }
        damaged = false;

        if(damageImage.color.a < 0.13f)
        {
            objBarrier.SetActive(false);
        }
    }


}
