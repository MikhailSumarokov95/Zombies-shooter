using UnityEngine;
using GameScore;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Shop>(true).SetDefaultSetting();

        FindObjectOfType<BattlePassRewarder>(true).Awake();

        if (!Application.isEditor) PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }
}
