using System;
using UnityEngine;

public class AmmunitionShop : MonoBehaviour
{
    public Action OnReplenished;
    [SerializeField] private int price;
    [SerializeField] private MaxAmmunitionWeapon[] _maxAmmunitionWeapon;
    private Money _money;

    private void Start()
    {
        _money = FindObjectOfType<Money>();
    }

    public void BuyAmmunition()
    {
        if (!_money.SpendMoney(price)) return;

        ReplenishAmmunition();
    }

    public void ReplenishAmmunition()
    {
        var weaponsBought = Progress.LoadWeaponsBought();

        for (var i = 0; i < _maxAmmunitionWeapon.Length; i++)
        {
            weaponsBought.WeaponsAttachmentsBought[_maxAmmunitionWeapon[i].NameWeapon].AmmunitionSum =
                _maxAmmunitionWeapon[i].Count;
        }

        Progress.SaveWeaponsBought(weaponsBought);

        OnReplenished?.Invoke();
    }

    public Progress.WeaponsBought SetDefaultAmmmunition(Progress.WeaponsBought weaponBought)
    {
        var weapons = weaponBought;

        for (var i = 0; i < _maxAmmunitionWeapon.Length; i++)
        {
            weapons.WeaponsAttachmentsBought[_maxAmmunitionWeapon[i].NameWeapon].AmmunitionSum =
                _maxAmmunitionWeapon[i].Count;
        }

        return weapons;
    }

    [Serializable]
    private class MaxAmmunitionWeapon
    {
        public string NameWeapon;
        public int Count;
    }
}