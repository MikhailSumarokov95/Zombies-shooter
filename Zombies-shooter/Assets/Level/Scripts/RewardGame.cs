using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardGame : MonoBehaviour
{
    [SerializeField] private int factorMoney = 3;
    [SerializeField] private TMP_Text moneyPerWaveText;
    [SerializeField] private Button doubleRewardButton;
    private KillCounter _killCounter;
    private int _moneyPerWave;

    private void Awake()
    {
        _killCounter = FindObjectOfType<KillCounter>();
    }

    private void OnEnable()
    {
        doubleRewardButton.gameObject.SetActive(true);
        CountRewardPerWave(1);
    }

    public void CountRewardPerWave(int factor)
    {
        _moneyPerWave = _killCounter.SumKilledPerWave * factor * factorMoney;
        moneyPerWaveText.text = _moneyPerWave.ToString();
    }

    public void RewardPerWave()
    {
        FindObjectOfType<Money>().MakeMoney(_moneyPerWave);
        _moneyPerWave = 0;
    }

    public void TryRewardDoubleMoney()
    {
        GSConnect.ShowRewardedAd(GSConnect.DoubleMoneyReward);
    }
}