public class SelectorScope : SelectorAttachment
{
    public override void AwakeAttachment()
    {
        _countAttachment = _weaponAttachmentManager.ScopeBehaviourCount;
    }

    public override void SetActiveAttachment(int value)
    {
        _weaponAttachmentManager.SetEquippedScope(value);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        var weaponsBought = _shop.WeaponsBought;
        weaponsBought.WeaponsAttachmentsBought[_shop.CurrentWeaponName].ScopeIndex.Add(_currentAttachment);
        _shop.WeaponsBought = weaponsBought;
        _weaponAttachmentBought.Add(_currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        var weaponsSelected = _shop.WeaponsSelected;
        weaponsSelected.WeaponsAttachmentsSelected[_shop.CurrentWeaponName].ScopeIndex = _currentAttachment;
        _shop.WeaponsSelected = weaponsSelected;
        _weaponAttachmentSelected = _currentAttachment;
        ScrollThrough(0);
    }
}