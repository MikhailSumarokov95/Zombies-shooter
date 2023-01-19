using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour 
{
    [SerializeField] private Sprite sprite;
    public void Load()
    {
    }

    [ContextMenu("Save")]
    public void Save()
    {
        
        var weaponOptions = new Progress.WeaponOptionsSelected() { ScopeIndex = 4, MuzzleIndex = 3, LaserIndex = 1, GripIndex = 2, MagazineIndex = 0,};
        var weaponOptSel = new Dictionary<string, Progress.WeaponOptionsSelected>();
        weaponOptSel.Add("Assault Rifle 02", weaponOptions);
        var weapons = new Progress.WeaponsSelected() { WeaponsOptionsSelected = weaponOptSel };
        Progress.SaveWeaponsSelected(weapons);
    }
}
