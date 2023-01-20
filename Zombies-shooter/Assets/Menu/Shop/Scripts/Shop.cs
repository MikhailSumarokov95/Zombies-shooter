using UnityEngine;
using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class Shop : MonoBehaviour 
{
    [SerializeField] private Transform weaponsParentTr;
    [SerializeField] private SelectorAttachment selectorScope;
    [SerializeField] private Button buyWeapon;
    [SerializeField] private Button selectedWeapon;
    [SerializeField] private Button selectWeapon;
    
    private Weapon[] _weapons;
    private int _currentWeaponNumber;

    private string _currentWeaponName;
    public string CurrentWeaponName { get { return _currentWeaponName; } set { _currentWeaponName = value; } }

    private Progress.WeaponsBought _weaponsBought;
    public Progress.WeaponsBought WeaponsBought { get { return _weaponsBought; } set { _weaponsBought = value; } }

    private Progress.WeaponsSelected _weaponsSelected;
    public Progress.WeaponsSelected WeaponsSelected { get { return _weaponsSelected; } set { _weaponsSelected = value; } }

    [ContextMenu("Save")]
    public void SetDefaultSetting()
    {
        print("Default");

        var weapons = weaponsParentTr.GetComponentsInChildren<Weapon>(true);

        //выбраное
        var weaponSelected = new Progress.WeaponsSelected();
        weaponSelected.WeaponsAttachmentsSelected = new Dictionary<string, Progress.WeaponAttachmentSelected>();
        foreach (var weapon in weapons)
        {
            var weaponAttachmentManager = weapon.GetComponent<WeaponAttachmentManager>();
            var weaponAttachmentSelected = new Progress.WeaponAttachmentSelected()
            { IsSelectedWeapon = false,
                GripIndex = weaponAttachmentManager.GripIndex,
                LaserIndex = weaponAttachmentManager.LaserIndex,
                MuzzleIndex = weaponAttachmentManager.MuzzleIndex,
                ScopeIndex = weaponAttachmentManager.ScopeIndex };

            weaponSelected.WeaponsAttachmentsSelected.Add(weapon.WeaponName, weaponAttachmentSelected);

            weaponSelected.WeaponsAttachmentsSelected[weapons[0].WeaponName].IsSelectedWeapon = true; // назначение стандартной пушки
        }
        Progress.SaveWeaponsSelected(weaponSelected);

        //купленое
        var weaponBought = new Progress.WeaponsBought();
        weaponBought.WeaponsAttachmentsBought = new Dictionary<string, Progress.WeaponAttachmentsBought>();
        foreach (var weapon in weapons)
        {
            var weaponAttachmentManager = weapon.GetComponent<WeaponAttachmentManager>();
            var weaponAttachmentBought = new Progress.WeaponAttachmentsBought()
            {
                IsBoughtWeapon = false,
                GripIndex = new List<int> { weaponAttachmentManager.GripIndex },
                LaserIndex = new List<int> { weaponAttachmentManager.LaserIndex },
                MuzzleIndex = new List<int> { weaponAttachmentManager.MuzzleIndex },
                ScopeIndex = new List<int> { weaponAttachmentManager.ScopeIndex }
            };

            weaponBought.WeaponsAttachmentsBought.Add(weapon.WeaponName, weaponAttachmentBought);

            weaponBought.WeaponsAttachmentsBought[weapons[0].WeaponName].IsBoughtWeapon = true; // назначение стандартной пушки
        }
        Progress.SaveWeaponsBought(weaponBought);
    }

    private void Start()
    {
        if (!Progress.CheckSave()) SetDefaultSetting();
        StartInitWeapons();
        _weaponsBought = Progress.LoadWeaponsBought();
        _weaponsSelected = Progress.LoadWeaponsSelected();
        InitButtons();
    }

    private void StartInitWeapons()
    {
        _weapons = weaponsParentTr.GetComponentsInChildren<Weapon>(true);

        foreach (var weapon in _weapons)
            weapon.gameObject.SetActive(false);

        _weapons[0].gameObject.SetActive(true);
        _currentWeaponNumber = 0;

        _currentWeaponName = _weapons[_currentWeaponNumber].WeaponName;
    }

    public void InitButtons()
    {
        buyWeapon.gameObject.SetActive(false);
        selectedWeapon.gameObject.SetActive(false);
        selectWeapon.gameObject.SetActive(false);

        if (_weaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].IsSelectedWeapon)
        {
            selectedWeapon.gameObject.SetActive(true);
            SetActiveAttachment(true);
        }

        else if (_weaponsBought.WeaponsAttachmentsBought[_currentWeaponName].IsBoughtWeapon)
        {
            selectWeapon.gameObject.SetActive(true);
            SetActiveAttachment(true);
        }

        else
        {
            buyWeapon.gameObject.SetActive(true);
            SetActiveAttachment(false);
        }
    }

    private void SetActiveAttachment(bool value)
    {
        if (!value)
        {
            selectorScope.gameObject.SetActive(false);
            //и все остальные
        }

        else
        {
            selectorScope.gameObject.SetActive(true);
            selectorScope.AwakeSelectorAttachment(_weaponsBought.WeaponsAttachmentsBought[_currentWeaponName].ScopeIndex,
                _weaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].ScopeIndex,
                _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>(),
                this);
            //и все остальные
        }
    }

    public void ScrollThroughWeapons(int direction)
    {
        var numberNext = _currentWeaponNumber + Math.Sign(direction);
        numberNext = MathPlus.SawChart(numberNext, 0 , _weapons.Length - 1);
        _weapons[_currentWeaponNumber].gameObject.SetActive(false);
        _weapons[numberNext].gameObject.SetActive(true);
        _currentWeaponNumber = numberNext;
        _currentWeaponName = _weapons[_currentWeaponNumber].WeaponName;
        InitButtons();
    }

    public void BuyWeapon()
    {
        _weaponsBought.WeaponsAttachmentsBought[_currentWeaponName].IsBoughtWeapon = true;
        InitButtons();
    }
    
    public void SelectWeapon()
    {
        _weaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].IsSelectedWeapon = true;
        InitButtons();
    }
}
