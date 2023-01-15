using UnityEngine;
using System;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] private WeaponIcon[] weaponsIcon;
    [SerializeField] private Progress.Weapon[] startWeapon;
    private Money _money;

    private void Start()
    {
        SaveStartWeaponInStorage();
        DisableIconWeaponIsBuy();
        ActivateIconWeaponIsBuy();
        _money = GetComponent<Money>();
    }

    public void BuyWeapon(int weaponNumberIcon)
    {
        if (_money.SpendMoney(weaponsIcon[weaponNumberIcon].Price))
        {
            Progress.SaveWeapon(weaponsIcon[weaponNumberIcon].Weapon);
            weaponsIcon[weaponNumberIcon].IconIsBuy.SetActive(true);
            weaponsIcon[weaponNumberIcon].IconIsNotBuy.SetActive(false);
        }
    }

    private void ActivateIconWeaponIsBuy()
    {
        var weaponsIsBuy = Progress.LoadWeapon();
        foreach (var weapon in weaponsIcon)
        {
            foreach (var weaponIsBuy in weaponsIsBuy)
            {
                if (weapon.Weapon == weaponIsBuy)
                {
                    weapon.IconIsBuy.SetActive(true);
                    weapon.IconIsNotBuy.SetActive(false);
                }    
            }
        }
    }

    private void DisableIconWeaponIsBuy()
    {
        foreach (var weapon in weaponsIcon)
        {
            weapon.IconIsBuy.SetActive(false);
            weapon.IconIsNotBuy.SetActive(true);
        }
    }

    private void SaveStartWeaponInStorage()
    {
        foreach (var weapon in startWeapon)
            Progress.SaveWeapon(weapon);
    }

    [ContextMenu("ClearProgressAllWeapons")]
    private void ClearProgressAllWeapons()
    {
        foreach (Progress.Weapon weapon in Enum.GetValues(typeof(Progress.Weapon)))
            Progress.DeleteWeapon(weapon);
    }
}