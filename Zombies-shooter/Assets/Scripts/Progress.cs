using System;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public static string DefaultWeaponsBought;
    public static string DefaultWeaponsSelected;

    public static void SaveWeaponsSelected(WeaponsSelected weapons)
    {
        Debug.Log(weapons);
        Debug.Log(JsonUtility.ToJson(weapons));

        GSPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons).ToString());
        GSPrefs.Save();
    }

    public static WeaponsSelected LoadWeaponsSelected()
    {
        Debug.Log(GSPrefs.GetString("weaponsSelected", DefaultWeaponsSelected));

        return JsonUtility.FromJson<WeaponsSelected>(GSPrefs.GetString("weaponsSelected", DefaultWeaponsSelected));
    }

    public static void SaveWeaponsBought(WeaponsBought weapons)
    {
        Debug.Log(weapons);
        Debug.Log(JsonUtility.ToJson(weapons));

        GSPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons).ToString());
        GSPrefs.Save();
    }

    public static WeaponsBought LoadWeaponsBought()
    {
        Debug.Log(GSPrefs.GetString("weaponsBought", DefaultWeaponsBought));

        return JsonUtility.FromJson<WeaponsBought>(GSPrefs.GetString("weaponsBought", DefaultWeaponsBought));
    }

    public static void SaveMoney(int value)
    {
        GSPrefs.SetInt("money", value);
        GSPrefs.Save();
    }

    public static int LoadMoney()
    {
        return GSPrefs.GetInt("money", 0);
    }

    public static void SaveLevel(int value)
    {
        GSPrefs.SetInt("level", value);
        GSPrefs.Save();
    }

    public static int LoadLevel()
    {
        return GSPrefs.GetInt("level", 1);
    }

    public static void SaveBattlePass()
    {
        GSPrefs.SetInt("battlePass", 1);
        GSPrefs.Save();
    }

    public static bool LoadBattlePass()
    {
        return GSPrefs.GetInt("battlePass", 0) == 1;
    }

    public static void SaveGrenades(int value)
    {
        GSPrefs.SetInt("grenades", value);
        GSPrefs.Save();
    }

    public static int LoadGrenades()
    {
        return GSPrefs.GetInt("grenades", 0);
    }

    public static void SaveSensitivity(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
    }

    public static float LoadSensitivity()
    {
        return PlayerPrefs.GetFloat("sensitivity", 1);
    }

    public static void SaveVolume(float value)
    {
        PlayerPrefs.SetFloat("volume", value);
    }

    public static float LoadVolume()
    {
        return PlayerPrefs.GetFloat("volume", 1);
    }  
    
    public static void SaveMusicVolume(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public static float LoadMusicVolume()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1);
    }

    public static void SaveBattlePassRewardDebug()
    {
        GSPrefs.SetInt("battlePassRewardDebug", 1);
        GSPrefs.Save();
    }

    public static bool LoadBattlePassRewardDebug()
    {
        return GSPrefs.GetInt("battlePassRewardDebug", 0) == 1;
    }

    [Serializable]
    public class WeaponsSelected
    {
        public TFG.Generic.Dictionary<string, WeaponAttachmentSelected> WeaponsAttachmentsSelected;
    }

    [Serializable]
    public class WeaponAttachmentSelected
    {
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