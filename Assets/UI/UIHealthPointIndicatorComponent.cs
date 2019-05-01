using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthPointIndicatorComponent : MonoBehaviour
{
    [SerializeField] float InitialHealthPoint;
    [SerializeField] Health HealthComponent;
    Text HealthPointText;

    void Start()
    {
        HealthPointText = GetComponent<Text>();
        CustomDebug.LogCheckAssigned(HealthPointText);
        CustomDebug.LogCheckAssigned(HealthComponent);
    }

    public void OnHealthPointUpdate()
    {
        if (Utils.IsValid(HealthComponent) && Utils.IsValid(HealthPointText))
        {
            float percent = HealthComponent.GetCurrentHealth / HealthComponent.GetStartingHealth;
            HealthPointText.text = $"{percent.ToString()}%";
        }
    }
};
