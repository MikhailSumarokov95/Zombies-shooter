using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private WeaponIcon[] weaponsIcon;
    [SerializeField] private float transfomIsChoose = - 10f;
    private GameObject _currentWeaponIcon;

    private void Start()
    {
        DisableWeaponsIcon();
        ActivateAvailableWeaponsIcon();
    }

    public void ChooseWeapon(Progress.Weapon weapon)
    {
        if (_currentWeaponIcon != null) 
            _currentWeaponIcon.transform.Translate(0, - transfomIsChoose, 0);

        foreach (var weaponI in weaponsIcon)
            if (weaponI.Weapon == weapon)
            {
                weaponI.IconIsBuy.transform.Translate(0, transfomIsChoose, 0);
                _currentWeaponIcon = weaponI.IconIsBuy;
            }
    }

    private void ActivateAvailableWeaponsIcon()
    {
        var weapons = Progress.LoadWeapon();
        foreach (var weapon in weapons)
        {
            foreach (var weaponIcon in weaponsIcon)
                if (weaponIcon.Weapon == weapon)
                {
                    weaponIcon.IconIsBuy.SetActive(true);
                    weaponIcon.IconIsNotBuy.SetActive(false);
                }
        }
    }

    private void DisableWeaponsIcon()
    {
        foreach (var weaponIcon in weaponsIcon)
        {
            weaponIcon.IconIsBuy.SetActive(false);
            weaponIcon.IconIsNotBuy.SetActive(true);
        }
    }
}