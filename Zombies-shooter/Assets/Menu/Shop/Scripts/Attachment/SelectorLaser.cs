public class SelectorLaser : SelectorAttachment
{
    public override void AwakeAttachment()
    {
        _countAttachment = _weaponAttachmentManager.LaserBehaviourCount;
    }

    public override void SetActiveAttachment(int value)
    {
        _weaponAttachmentManager.SetEquippedLaser(value);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        var weaponsBought = _shop.WeaponsBought;
        weaponsBought.WeaponsAttachmentsBought[_shop.CurrentWeaponName].LaserIndex.Add(_currentAttachment);
        _shop.WeaponsBought = weaponsBought;
        _weaponAttachmentBought.Add(_currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        var weaponsSelected = _shop.WeaponsSelected;
        weaponsSelected.WeaponsAttachmentsSelected[_shop.CurrentWeaponName].LaserIndex = _currentAttachment;
        _shop.WeaponsSelected = weaponsSelected;
        _weaponAttachmentSelected = _currentAttachment;
        ScrollThrough(0);
    }
}