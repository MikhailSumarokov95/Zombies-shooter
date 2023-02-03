using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Progress
{
    public static void SaveWeaponsSelected(WeaponsSelected weapons)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/WeaponsSelected.dat");
        bf.Serialize(file, weapons);
        file.Close();
    }

    public static WeaponsSelected LoadWeaponsSelected()
    {
        if (File.Exists(Application.persistentDataPath
          + "/WeaponsSelected.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/WeaponsSelected.dat", FileMode.Open);
            WeaponsSelected weapons = (WeaponsSelected)bf.Deserialize(file);
            file.Close();
            return weapons;
        }
        else Debug.LogError("There is no save WeaponsSelected!");
        return null;
    }    

    public static void SaveWeaponsBought(WeaponsBought weapons)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/WeaponsBought.dat");
        bf.Serialize(file, weapons);
        file.Close();
    }

    public static bool CheckSave()
    {
        return File.Exists(Application.persistentDataPath + "/WeaponsBought.dat");
    }

    public static WeaponsBought LoadWeaponsBought()
    {
        if (File.Exists(Application.persistentDataPath
          + "/WeaponsBought.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/WeaponsBought.dat", FileMode.Open);
            WeaponsBought weapons = (WeaponsBought)bf.Deserialize(file);
            file.Close();
            return weapons;
        }
        else Debug.LogError("There is no save WeaponsBought!");
        return null;
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

    public static void SaveBattlePass()
    {
        PlayerPrefs.SetInt("battlePass", 1);
    }

    public static bool LoadBattlePass()
    {
        return PlayerPrefs.GetInt("battlePass", 0) == 1 ? true : false;
    }

    public static void SaveGrenades(int value)
    {
        PlayerPrefs.SetInt("grenades", value);
    }

    public static int LoadGrenades()
    {
        return PlayerPrefs.GetInt("grenades", 0);
    }

    public static void SaveSensitivity(float value)
    {
        PlayerPrefs.SetFloat("sensitivity", value);
    }

    public static float LoadSensitivity()
    {
        return PlayerPrefs.GetFloat("sensitivity", 1);
    }

    [Serializable]
    public class WeaponsSelected
    {
        public Dictionary<string, WeaponAttachmentSelected> WeaponsAttachmentsSelected;
    }

    [Serializable]
    public class WeaponAttachmentSelected
    {
        //public Sprite Sprite;
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
        //public Sprite[] Sprite;
        public bool IsBoughtWeapon;
        public List<int> ScopeIndex;
        public List<int> MuzzleIndex;
        public List<int> LaserIndex;
        public List<int> GripIndex;
        public int AmmunitionSum;
    }
}