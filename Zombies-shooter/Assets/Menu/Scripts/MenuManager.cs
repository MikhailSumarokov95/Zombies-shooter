using UnityEngine;
using GameScore;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        if (!Progress.CheckSave()) FindObjectOfType<Shop>(true).SetDefaultSetting();
        FindObjectOfType<BattlePassRewarder>(true).Awake();

        PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }
}
