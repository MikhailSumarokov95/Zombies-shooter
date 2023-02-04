using System;
using System.Collections.Generic;
using UnityEngine;

public static class Progress
{
    public static void SaveWeaponsSelected(WeaponsSelected weapons)
    {
        GSPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons).ToString());

        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath
        //  + "/WeaponsSelected.dat");
        //bf.Serialize(file, weapons);
        //file.Close();
    }

    public static WeaponsSelected LoadWeaponsSelected()
    {
        return JsonUtility.FromJson<WeaponsSelected>(GSPrefs.GetString("weaponsSelected", null));

        //if (File.Exists(Application.persistentDataPath
        //  + "/WeaponsSelected.dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file =
        //      File.Open(Application.persistentDataPath
        //      + "/WeaponsSelected.dat", FileMode.Open);
        //    WeaponsSelected weapons = (WeaponsSelected)bf.Deserialize(file);
        //    file.Close();
        //    return weapons;
        //}
        //else Debug.LogError("There is no save WeaponsSelected!");
        //return null;
    }    

    public static void SaveWeaponsBought(WeaponsBought weapons)
    {
        GSPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons).ToString());

        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath
        //  + "/WeaponsBought.dat");
        //bf.Serialize(file, weapons);
        //file.Close();
    }



    public static WeaponsBought LoadWeaponsBought()
    {
        return JsonUtility.FromJson<WeaponsBought>(GSPrefs.GetString("weaponsBought", null));

        //if (File.Exists(Application.persistentDataPath
        //  + "/WeaponsBought.dat"))
        //{
        //    BinaryFormatter bf = new BinaryFormatter();
        //    FileStream file =
        //      File.Open(Application.persistentDataPath
        //      + "/WeaponsBought.dat", FileMode.Open);
        //    WeaponsBought weapons = (WeaponsBought)bf.Deserialize(file);
        //    file.Close();
        //    return weapons;
        //}
        //else Debug.LogError("There is no save WeaponsBought!");
        //return null;
    }

    public static bool CheckSave()
    {
        return GSPrefs.GetString("weaponsSelected", null) == null;
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
        return GSPrefs.GetInt("battlePass", 0) == 1 ? true : false;
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
        GSPrefs.SetFloat("sensitivity", value);
        GSPrefs.Save();
    }

    public static float LoadSensitivity()
    {
        return GSPrefs.GetFloat("sensitivity", 1);
    }

    [Serializable]
    public class WeaponsSelected
    {
        public Dictionary<string, WeaponAttachmentSelected> WeaponsAttachmentsSelected;
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
        public Dictionary<string, WeaponAttachmentsBought> WeaponsAttachmentsBought;
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