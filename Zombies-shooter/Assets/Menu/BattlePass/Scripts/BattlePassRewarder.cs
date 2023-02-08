using System;
using UnityEngine;

public class BattlePassRewarder : MonoBehaviour
{
    public Action OnBoughtBattlePass;
    [SerializeField] private RewardBattlePassPerLevel[] _rewardBattlePassPerLevel;

    public void Awake()
    {
        var numberLevel = FindObjectOfType<Level>().CurrentLevel;
        var rewardBattlePass = Progress.LoadBattlePass() ?
            _rewardBattlePassPerLevel[numberLevel].IsHaveBattlePassReward : 
            _rewardBattlePassPerLevel[numberLevel].IsNotHaveBattlePassReward;
        RewardPerLevel(rewardBattlePass);
    }

    [ContextMenu("BoughtBattlePass")]
    public void BoughtBattlePass()
    {
        Progress.SaveBattlePass();
        for (var i = 1; i < FindObjectOfType<Level>().CurrentLevel + 1; i++)
            RewardPerLevel(_rewardBattlePassPerLevel[i].IsHaveBattlePassReward);

        OnBoughtBattlePass?.Invoke();
    }

    private void RewardPerLevel(RewardBattlePass reward)
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

    [Serializable]
    public class RewardBattlePassPerLevel
    {
        public RewardBattlePass IsNotHaveBattlePassReward;
        public RewardBattlePass IsHaveBattlePassReward;
    }

    [Serializable]
    public class RewardBattlePass
    {
        public string NameWeapon;
        public int AmountMoney;
    }
}
