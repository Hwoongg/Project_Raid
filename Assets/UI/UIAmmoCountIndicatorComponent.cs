using UnityEngine;
using UnityEngine.UI;

public class UIAmmoCountIndicatorComponent : MonoBehaviour
{
    [SerializeField] int CurrentAmmoCount;
    [SerializeField] int InitialAmmoCount;
    [SerializeField] bool IsInfinity = false;
    Text AmmoCountText;
    [SerializeField] Weapon WeaponComponent;

    void Start()
    {
        AmmoCountText = GetComponent<Text>();
        CustomDebug.LogCheckAssigned(AmmoCountText);
        CustomDebug.LogCheckAssigned(WeaponComponent);
        InitialAmmoCount = Mathf.Max(0, WeaponComponent.GetMaxBullet);
    }

    public void OnAmmoCountUpdated()
    {
        CurrentAmmoCount = Mathf.Max(0, WeaponComponent.GetCurrentBullet);
        
        if (Utils.IsValid(AmmoCountText) && Utils.IsValid(WeaponComponent))
        {
            AmmoCountText.text = $"{CurrentAmmoCount.ToString()}/{InitialAmmoCount.ToString()}";
        }
        else
        {
            CustomDebug.LogE("Ammo count text or Weapon component occurs error!", this);
        }
    }

    public void OnAmmoStatusInfinity(bool isInf = false)
    {
        IsInfinity = isInf;
    }
};
