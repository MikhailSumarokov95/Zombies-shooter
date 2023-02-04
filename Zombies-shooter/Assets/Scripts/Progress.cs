using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public static class Progress
{
    public static void SaveWeaponsSelected(WeaponsSelected weapons)
    {
        if (Application.isEditor)
            PlayerPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons));
        else
            GSPrefs.SetString("weaponsSelected", JsonUtility.ToJson(weapons));

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
        if (Application.isEditor)
            PlayerPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons));
        else
            GSPrefs.SetString("weaponsBought", JsonUtility.ToJson(weapons));

        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath
        //  + "/WeaponsBought.dat");
        //bf.Serialize(file, weapons);
        //file.Close();
    }



    public static WeaponsBought LoadWeaponsBought()
    {
        if (Application.isEditor)
            return JsonUtility.FromJson<WeaponsBought>(PlayerPrefs.GetString("weaponsBought", null));
        else
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

    //public static string ToString(WeaponsSelected weaponsSelected)
    //{
    //    //var weaponsBuilder = new StringBuilder();

    //        //foreach (var weapon in weaponsSelected.WeaponsAttachmentsSelected.Keys)
    //        //{
    //        //    weaponsBuilder.Append('{');


    //        //    weaponsBuilder.Append(weapon);
    //        //    weaponsBuilder.Append(':');

    //        //    weaponsBuilder.Append(weaponsSelected.WeaponsAttachmentsSelected[weapon].IsSelectedWeapon);
    //        //    weaponsBuilder.Append(',');
    //        //    weaponsBuilder.Append(weaponsSelected.WeaponsAttachmentsSelected[weapon].ScopeIndex);
    //        //    weaponsBuilder.Append(',');
    //        //    weaponsBuilder.Append(weaponsSelected.WeaponsAttachmentsSelected[weapon].MuzzleIndex);
    //        //    weaponsBuilder.Append(',');
    //        //    weaponsBuilder.Append(weaponsSelected.WeaponsAttachmentsSelected[weapon].LaserIndex);
    //        //    weaponsBuilder.Append(',');
    //        //    weaponsBuilder.Append(weaponsSelected.WeaponsAttachmentsSelected[weapon].GripIndex);

    //        //    weaponsBuilder.Append('}');
    //        //}

    //        //return weaponsBuilder.ToString();
    //}

    //public static WeaponsSelected FromStringWeaponsSelected(string strWeapon)
    //{
    //    var weaponsSelected = new WeaponsSelected();
    //    weaponsSelected.WeaponsAttachmentsSelected = new Dictionary<string, WeaponAttachmentSelected>();
    //    foreach (var ch in strWeapon)
    //    {
    //        bool isNameWeapon = false;

    //        var name = new StringBuilder();

    //        var attachment = new WeaponAttachmentSelected();

    //        if (ch == '{')
    //        {
    //            isNameWeapon = true;
    //            continue;
    //        }

    //        if (isNameWeapon) name.Append(ch);

    //        if (ch == ':')
    //        {
    //            isNameWeapon = false;
    //        }
    //    }
    //}
}