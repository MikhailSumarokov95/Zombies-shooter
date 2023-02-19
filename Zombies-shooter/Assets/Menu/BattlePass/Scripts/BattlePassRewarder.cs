using System;
using UnityEngine;
using UnityEngine.UI;

public class BattlePassRewarder : MonoBehaviour
{
    public Action OnBoughtBattlePass;
    [SerializeField] private RewardBattlePassPerLevel[] _rewardBattlePassPerLevel;
    [SerializeField] private LevelAchievementMark[] levelAchievementMark;

    private void OnEnable()
    {
        EnableLevelAchievementMark();
    }

    [ContextMenu("BoughtBattlePass")]
    public void BoughtBattlePass()
    {
        Progress.SaveBattlePass();
        for (var i = 1; i < FindObjectOfType<Level>().CurrentLevel + 1; i++)
            Reward(_rewardBattlePassPerLevel[i].IsHaveBattlePassReward);

        OnBoughtBattlePass?.Invoke();
    }

    public void RewardPerLevels()
    {
        for (var i = 1; i < FindObjectOfType<Level>().CurrentLevel + 1; i++)
            Reward(_rewardBattlePassPerLevel[i].IsHaveBattlePassReward);

        for (var i = 1; i < FindObjectOfType<Level>().CurrentLevel + 1; i++)
            Reward(_rewardBattlePassPerLevel[i].IsNotHaveBattlePassReward);

        Progress.SaveBattlePassRewardDebug();
    }

    public void RewardPerLevel()
    {
        var currentLevel = FindObjectOfType<Level>().CurrentLevel;

        Reward(_rewardBattlePassPerLevel[currentLevel].IsNotHaveBattlePassReward);

        if (Progress.LoadBattlePass())
            Reward(_rewardBattlePassPerLevel[currentLevel].IsHaveBattlePassReward);
    }

    private void Reward(RewardBattlePass reward)
    {
        if (reward.NameWeapon != null && reward.NameWeapon != "")
        {
            var weaponsBought = Progress.LoadWeaponsBought();
            weaponsBought.WeaponsAttachmentsBought[reward.NameWeapon].IsBoughtWeapon = true;
            Progress.SaveWeaponsBought(weaponsBought);
        }

        if (reward.AmountMoney != 0) 
            FindObjectOfType<Money>().MakeMoney(reward.AmountMoney);
    }

    private void EnableLevelAchievementMark()
    {
        var currentLevel = FindObjectOfType<Level>(true).CurrentLevel;

        for (var i = 1; i < levelAchievementMark.Length; i++)
        {
            levelAchievementMark[i].CloseImage.gameObject.SetActive(!(i <= currentLevel));
            levelAchievementMark[i].OpenedImage.gameObject.SetActive(i <= currentLevel);
        }
    }

    [Serializable]
    private class RewardBattlePassPerLevel
    {
        public RewardBattlePass IsNotHaveBattlePassReward;
        public RewardBattlePass IsHaveBattlePassReward;
    }

    [Serializable]
    private class RewardBattlePass
    {
        public string NameWeapon;
        public int AmountMoney;
    }

    [Serializable]
    private class LevelAchievementMark
    {
        public Image CloseImage;
        public Image OpenedImage;
    }
}