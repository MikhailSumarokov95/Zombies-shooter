using System;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public static void SaveWeaponsSelected(WeaponsSelected weapons)
    {
        if (Application.isEditor)
            PlayerPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons));
        else
            GSPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons));
    }

    public static WeaponsSelected LoadWeaponsSelected()
    {
        if (Application.isEditor)
            return JsonUtility.FromJson<WeaponsSelected>(PlayerPrefs.GetString("weaponsSelected", null));
        else
            return JsonUtility.FromJson<WeaponsSelected>(GSPrefs.GetString("weaponsSelected", null));
    }

    public static void SaveWeaponsBought(WeaponsBought weapons)
    {
        if (Application.isEditor)
            PlayerPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons));
        else
            GSPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons));
    }



    public static WeaponsBought LoadWeaponsBought()
    {
        if (Application.isEditor)
            return JsonUtility.FromJson<WeaponsBought>(PlayerPrefs.GetString("weaponsBought", null));
        else
            return JsonUtility.FromJson<WeaponsBought>(GSPrefs.GetString("weaponsBought", null));
    }

    public static bool CheckSave()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetString("weaponsSelected", null) == null;
        else return GSPrefs.GetString("weaponsSelected", null) == null;
    }

    public static void SaveMoney(int value)
    {
        if (Application.isEditor)
            PlayerPrefs.SetInt("money", value);
        else
        {
            GSPrefs.SetInt("money", value);
            GSPrefs.Save();
        }
    }

    public static int LoadMoney()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetInt("money", 0);
        else
            return GSPrefs.GetInt("money", 0);
    }

    public static void SaveLevel(int value)
    {
        if (Application.isEditor)
            PlayerPrefs.SetInt("level", value);
        else
        {
            GSPrefs.SetInt("level", value);
            GSPrefs.Save();
        }
    }

    public static int LoadLevel()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetInt("level", 1);
        else
            return GSPrefs.GetInt("level", 1);
    }

    public static void SaveBattlePass()
    {
        if (Application.isEditor)
            PlayerPrefs.SetInt("battlePass", 1);
        else
        {
            GSPrefs.SetInt("battlePass", 1);
            GSPrefs.Save();
        }
    }

    public static bool LoadBattlePass()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetInt("battlePass", 0) == 1;
        else
            return GSPrefs.GetInt("battlePass", 0) == 1;
    }

    public static void SaveGrenades(int value)
    {
        if (Application.isEditor)
            PlayerPrefs.SetInt("grenades", value);
        else
        {
            GSPrefs.SetInt("grenades", value);
            GSPrefs.Save();
        }
    }

    public static int LoadGrenades()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetInt("grenades", 0);
        else
            return GSPrefs.GetInt("grenades", 0);
    }

    public static void SaveSensitivity(float value)
    {
        if (Application.isEditor)
            PlayerPrefs.SetFloat("sensitivity", value);
        else
        {
            GSPrefs.SetFloat("sensitivity", value);
            GSPrefs.Save();
        }

    }

    public static float LoadSensitivity()
    {
        if (Application.isEditor)
            return PlayerPrefs.GetInt("sensitivity", 1);
        else
            return GSPrefs.GetFloat("sensitivity", 1);
    }

    [Serializable]
    public class WeaponsSelected
    {
        public TFG.Generic.Dictionary<string, WeaponAttachmentSelected> WeaponsAttachmentsSelected;
    }

    [Serializable]
    public class WeaponAttachmentSelected
    {
        public bool IsSelectedWeapon;
        public int ScopeIndex;
        public int MuzzleIndex;
        public int LaserIndex;
        public int GripIndex;
    }

    [Serializable]
    public class WeaponsBought
    {
        public TFG.Generic.Dictionary<string, WeaponAttachmentsBought> WeaponsAttachmentsBought;
    }

    [Serializable]
    public class WeaponAttachmentsBought
    {
        public bool IsBoughtWeapon;
        public List<int> ScopeIndex;
        public List<int> MuzzleIndex;
        public List<int> LaserIndex;
        public List<int> GripIndex;
        public int AmmunitionSum;
    }
}