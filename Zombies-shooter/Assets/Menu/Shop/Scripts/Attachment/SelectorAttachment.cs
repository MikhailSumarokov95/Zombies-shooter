using System;
using UnityEngine;
using UnityEngine.UI;
using InfimaGames.LowPolyShooterPack;
using System.Collections.Generic;

public abstract class SelectorAttachment : MonoBehaviour
{
    public Action<int> OnSetAttachment;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private Button selectedButton;
    protected List<int> _weaponAttachmentBought;
    protected int _weaponAttachmentSelected;
    protected int _currentAttachment;
    protected Shop _shop;
    protected WeaponAttachmentManager _weaponAttachmentManager;
    protected int _countAttachment;

    public void AwakeSelectorAttachment(List<int> weaponAttachmentBought, int weaponAttachmentSelected, WeaponAttachmentManager weaponAttachmentManager, Shop shop)
    {
        _shop = shop;
        _weaponAttachmentManager = weaponAttachmentManager;
        _weaponAttachmentBought = weaponAttachmentBought;
        _weaponAttachmentSelected = weaponAttachmentSelected;

        _currentAttachment = _weaponAttachmentSelected;

        AwakeAttachment();
        ScrollThrough(0);
    }

    public void ScrollThrough(int direction)
    {
        var nextAttachmen = Math.Sign(direction) + _currentAttachment;
        nextAttachmen = MathPlus.SawChart(nextAttachmen, 0, _countAttachment - 1);
        _currentAttachment = nextAttachmen;

        SetActiveAttachment(nextAttachmen);

        buyButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);
        selectedButton.gameObject.SetActive(false);

        if (_weaponAttachmentSelected == nextAttachmen)
        {
            selectedButton.gameObject.SetActive(true);
            return;
        }

        foreach (var attachment in _weaponAttachmentBought)
        {
            if (attachment == nextAttachmen)
            {
                selectButton.gameObject.SetActive(true);
                return;
            }
        }

        buyButton.gameObject.SetActive(true);
    }

    public abstract void SetActiveAttachment(int value);
    public abstract void AwakeAttachment();
    public abstract void BuyAttachment();
    public abstract void SelectAttachment();
}