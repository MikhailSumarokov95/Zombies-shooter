using System;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public enum Weapon
    {
        One,
        Two,
        Three,
        Four,
        Five
    }

    public static void SaveWeapon(Weapon weapon)
    {
        PlayerPrefs.SetInt(weapon.ToString(), 1);
    }
    
    public static void DeleteWeapon(Weapon weapon)
    {
        PlayerPrefs.SetInt(weapon.ToString(), 0);
    }

    public static Weapon[] LoadWeapon()
    {
        var weapons = new List<Weapon>();
        foreach (Weapon weapon in Enum.GetValues(typeof(Weapon)))
        {
            if (PlayerPrefs.GetInt(weapon.ToString(), 0) == 1)
                weapons.Add(weapon);
        }
        return weapons.ToArray();
    }

    public static void SaveMoney(int value)
    {
        PlayerPrefs.SetInt("money", value);
    }
    
    public static int LoadMoney()
    {
        return PlayerPrefs.GetInt("money", 0);
    }

    public static void SaveLevel(int value)
    {
        PlayerPrefs.SetInt("level", value);
    }

    public static int LoadLevel()
    {
        return PlayerPrefs.GetInt("level", 1);
    }
}
