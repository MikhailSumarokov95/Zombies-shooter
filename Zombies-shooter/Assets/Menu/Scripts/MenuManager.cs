using UnityEngine;
using GameScore;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Shop>(true).SetDefaultSetting();

        if (!Application.isEditor) PlayerPrefs.SetString("selectedLanguage", GS_Language.Current());
    }

    private void Start()
    {
        //debug вознаграждение игроков, которым не дало награду из-за бага
        if (!Progress.LoadBattlePassRewardDebug())
            FindObjectOfType<BattlePassRewarder>(true).RewardPerLevels();
    }
}
