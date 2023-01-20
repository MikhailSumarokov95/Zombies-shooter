using UnityEngine;

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
        _shop.WeaponsBought.WeaponsAttachmentsBought[_shop.CurrentWeaponName].ScopeIndex.Add(_currentAttachment);
        _weaponAttachmentBought.Add(_currentAttachment);
        ScrollThrough(0);
    }

    public override void SelectAttachment()
    {
        _shop.WeaponsSelected.WeaponsAttachmentsSelected[_shop.CurrentWeaponName].ScopeIndex = _currentAttachment;
        _weaponAttachmentSelected = _currentAttachment;
        ScrollThrough(0);
    }
}