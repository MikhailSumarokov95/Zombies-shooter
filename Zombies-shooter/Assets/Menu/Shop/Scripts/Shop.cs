using UnityEngine;
using InfimaGames.LowPolyShooterPack;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private Transform weaponsParentTr;
    [SerializeField] private SelectorAttachment selectorScope;
    [SerializeField] private SelectorAttachment selectorMuzzle;
    [SerializeField] private SelectorAttachment selectorLaser;
    [SerializeField] private SelectorAttachment selectorGrip;
    [SerializeField] private Button buyWeaponButton;
    [SerializeField] private TMP_Text buyWeaponText;
    [SerializeField] private Button selectWeaponButton;
    [SerializeField] private Button selectedWeaponButton;

    private Weapon[] _weapons;
    private int _currentWeaponNumber;
    private int _cost;
    private Money _money;

    private string _currentWeaponName;
    public string CurrentWeaponName { get { return _currentWeaponName; } set { _currentWeaponName = value; } }

    private Progress.WeaponsBought _weaponsBought;
    public Progress.WeaponsBought WeaponsBought
    { 
        get 
        { 
            return _weaponsBought;
        } 

        set 
        { 
            _weaponsBought = value;
            Save();
        } 
    }

    private Progress.WeaponsSelected _weaponsSelected;
    public Progress.WeaponsSelected WeaponsSelected 
    { 
        get 
        {
            return _weaponsSelected;
        } 

        set
        { 
            _weaponsSelected = value;
            Save();
        } 
    }


    private void Awake()
    {
        _money = FindObjectOfType<Money>();
    }

    private void OnEnable()
    {
        StartInitWeapons();
        _weaponsBought = Progress.LoadWeaponsBought();
        _weaponsSelected = Progress.LoadWeaponsSelected();
        InitButtons();
    }

    public void ScrollThroughWeapons(int direction)
    {
        var numberNext = _currentWeaponNumber + Math.Sign(direction);
        numberNext = MathPlus.SawChart(numberNext, 0, _weapons.Length - 1);
        _weapons[_currentWeaponNumber].gameObject.SetActive(false);
        _weapons[numberNext].gameObject.SetActive(true);
        _currentWeaponNumber = numberNext;
        _currentWeaponName = _weapons[_currentWeaponNumber].WeaponName;
        _cost = _weapons[_currentWeaponNumber].Cost;
        InitButtons();
    }

    public void BuyWeapon()
    {
        if (!_money.SpendMoney(_cost)) return;

        var weaponsBought = WeaponsBought;
        weaponsBought.WeaponsAttachmentsBought[_currentWeaponName].IsBoughtWeapon = true;

        weaponsBought.WeaponsAttachmentsBought[_currentWeaponName].AmmunitionSum =
            _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>().GetEquippedMagazine().GetAmmunitionTotal();

        WeaponsBought = weaponsBought;

        InitButtons();
    }

    public void SelectWeapon()
    {
        var weaponsSelected = WeaponsSelected;
        weaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].IsSelectedWeapon = true;
        WeaponsSelected = weaponsSelected;
        InitButtons();
    }

    public void Save()
    {
        Progress.SaveWeaponsBought(WeaponsBought);
        Progress.SaveWeaponsSelected(WeaponsSelected);        
    }

    private void InitButtons()
    {
        buyWeaponButton.gameObject.SetActive(false);
        selectedWeaponButton.gameObject.SetActive(false);
        selectWeaponButton.gameObject.SetActive(false);

        if (WeaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].IsSelectedWeapon)
        {
            selectedWeaponButton.gameObject.SetActive(true);
            SetActiveAttachment(true);
        }

        else if (WeaponsBought.WeaponsAttachmentsBought[_currentWeaponName].IsBoughtWeapon)
        {
            selectWeaponButton.gameObject.SetActive(true);
            SetActiveAttachment(true);
        }

        else
        {
            buyWeaponButton.gameObject.SetActive(true);
            buyWeaponText.text = _cost.ToString();
            SetActiveAttachment(false);
        }
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

    private void SetActiveAttachment(bool value)
    {
        if (!value)
        {
            selectorScope.gameObject.SetActive(false);

            selectorMuzzle.gameObject.SetActive(false);

            selectorLaser.gameObject.SetActive(false);

            selectorGrip.gameObject.SetActive(false);
        }

        else
        {
            selectorScope.gameObject.SetActive(true);
            selectorScope.AwakeSelectorAttachment(WeaponsBought.WeaponsAttachmentsBought[_currentWeaponName].ScopeIndex,
                WeaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].ScopeIndex,
                _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>(),
                this);

            selectorMuzzle.gameObject.SetActive(true);
            selectorMuzzle.AwakeSelectorAttachment(WeaponsBought.WeaponsAttachmentsBought[_currentWeaponName].MuzzleIndex,
                WeaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].MuzzleIndex,
                _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>(),
                this);

            selectorLaser.gameObject.SetActive(true);
            selectorLaser.AwakeSelectorAttachment(WeaponsBought.WeaponsAttachmentsBought[_currentWeaponName].LaserIndex,
                WeaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].LaserIndex,
                _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>(),
                this);

            selectorGrip.gameObject.SetActive(true);
            selectorGrip.AwakeSelectorAttachment(WeaponsBought.WeaponsAttachmentsBought[_currentWeaponName].GripIndex,
                WeaponsSelected.WeaponsAttachmentsSelected[_currentWeaponName].GripIndex,
                _weapons[_currentWeaponNumber].GetComponent<WeaponAttachmentManager>(),
                this);
        }
    }

    public void SetDefaultSetting()
    {
        var weapons = weaponsParentTr.GetComponentsInChildren<Weapon>(true);

        //выбраное
        var weaponSelected = new Progress.WeaponsSelected();
        weaponSelected.WeaponsAttachmentsSelected = new Dictionary<string, Progress.WeaponAttachmentSelected>();
        foreach (var weapon in weapons)
        {
            var weaponAttachmentManager = weapon.GetComponent<WeaponAttachmentManager>();
            var weaponAttachmentSelected = new Progress.WeaponAttachmentSelected()
            {
                IsSelectedWeapon = false,
                GripIndex = weaponAttachmentManager.GripIndex,
                LaserIndex = weaponAttachmentManager.LaserIndex,
                MuzzleIndex = weaponAttachmentManager.MuzzleIndex,
                ScopeIndex = weaponAttachmentManager.ScopeIndex
            };

            weaponSelected.WeaponsAttachmentsSelected.Add(weapon.WeaponName, weaponAttachmentSelected);
        }

        weaponSelected.WeaponsAttachmentsSelected["Assault Rifle 01"].IsSelectedWeapon = true; // назначение стандартной пушки
        weaponSelected.WeaponsAttachmentsSelected["Handgun 01"].IsSelectedWeapon = true; // назначение стандартной пушки

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
            
        }

        weaponBought.WeaponsAttachmentsBought["Assault Rifle 01"].IsBoughtWeapon = true; // назначение стандартной пушки
        weaponBought.WeaponsAttachmentsBought["Handgun 01"].IsBoughtWeapon = true; // назначение стандартной пушки

        Progress.SaveWeaponsBought(weaponBought);
        //Progress.SaveWeaponsBought(FindObjectOfType<AmmunitionShop>().SetDefaultAmmmunition(weaponBought));
    }
}