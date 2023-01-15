using System;
using UnityEngine;

public class InventoryPlayer : MonoBehaviour
{
    [SerializeField] private WeaponInventory[] weaponInventory;
    [SerializeField] private Progress.Weapon startWeapon = Progress.Weapon.One;
    private Progress.Weapon[] _availableWeapons;
    private InventoryUI _inventoryUI;
    private Progress.Weapon _currentWeapon;
    public Progress.Weapon CurrentWeapon
    {
        get 
        {
            return _currentWeapon;
        }

        set
        {
            _currentWeapon = value;
            _inventoryUI.ChooseWeapon(_currentWeapon);
        }
    }

    private void Start()
    {
        _inventoryUI = FindObjectOfType<InventoryUI>();
        _availableWeapons = Progress.LoadWeapon();
        CurrentWeapon = startWeapon;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0.09f) ScrollWeapon(1);
        else if (Input.GetAxis("Mouse ScrollWheel") < - 0.09f) ScrollWeapon(-1);
    }

    private void ScrollWeapon(int direction)
    {
        int numberInAvailableOldWeapons = 0;
        for (var i = 0; i < _availableWeapons.Length; i++)
            if (_availableWeapons[i] == CurrentWeapon) numberInAvailableOldWeapons = i;

        var numberInAvailableNewWeapons =
            MathPlus.SawChart(numberInAvailableOldWeapons + (int)Math.Sign(direction), 0, _availableWeapons.Length - 1);

        var numberOldWeaponInInventory = 0;
        var numberNewWeaponInInventory = 0;

        for (var j = 0; j < weaponInventory.Length; j++)
        {
            if (weaponInventory[j].Weapon == _availableWeapons[numberInAvailableOldWeapons])
                numberOldWeaponInInventory = j;

            if (weaponInventory[j].Weapon == _availableWeapons[numberInAvailableNewWeapons])
                numberNewWeaponInInventory = j;
        }

        TakeWeaponFromInventory(numberOldWeaponInInventory, numberNewWeaponInInventory);
    }

    private void TakeWeaponFromInventory(int oldNumber, int newNumber)
    {
        CurrentWeapon = weaponInventory[newNumber].Weapon;
        weaponInventory[oldNumber].GameObj.SetActive(false);
        weaponInventory[newNumber].GameObj.SetActive(true);
    }
}
