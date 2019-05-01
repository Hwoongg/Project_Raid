using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRemainingTimeIndicatorBarComponent : MonoBehaviour
{
    [SerializeField] TimeRule TimeRule;
    Image RemainingTimeBar;

    void Start()
    {
        CustomDebug.LogCheckAssigned(TimeRule);
        RemainingTimeBar = GetComponent<Image>();
        RemainingTimeBar.fillMethod = Image.FillMethod.Horizontal;
        CustomDebug.LogCheckAssigned(RemainingTimeBar);
    }

    public void OnRemainingTimeUpdate()
    {
        if (Utils.IsValid(TimeRule) && Utils.IsValid(RemainingTimeBar))
        {
            RemainingTimeBar.SetAllDirty();
            RemainingTimeBar.fillAmount = TimeRule.remainingTime / TimeRule.initialRemainingTime;
        }
    }
};
