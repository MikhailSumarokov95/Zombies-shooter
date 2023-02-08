public class SelectorMuzzle : SelectorAttachment
{
    protected void Start()
    {
        _attachmenAbsenteeNumber = 0;
    }

    public override void AwakeAttachment()
    {
        _countAttachment = _weaponAttachmentManager.MuzzleBehaviourCount;
    }

    public override void SetActiveAttachment(int value)
    {
        _weaponAttachmentManager.SetEquippedMuzzle(value);
    }

    public override void BuyAttachment()
    {
        if (!_money.SpendMoney(cast)) return;

        var weaponsBought = _shop.WeaponsBought;
        weaponsBought.WeaponsAttachmentsBought[_shop.CurrentWeaponName].MuzzleIndex.Add(_currentAttachment);
        _shop.WeaponsBought = weaponsBought;
        _weaponAttachmentBought.Add(_currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        var weaponsSelected = _shop.WeaponsSelected;
        weaponsSelected.WeaponsAttachmentsSelected[_shop.CurrentWeaponName].MuzzleIndex = _currentAttachment;
        _shop.WeaponsSelected = weaponsSelected;
        _weaponAttachmentSelected = _currentAttachment;
        ScrollThrough(0);
    }
}
