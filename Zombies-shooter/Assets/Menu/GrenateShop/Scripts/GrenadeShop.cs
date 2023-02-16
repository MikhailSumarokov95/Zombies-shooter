using System;
using UnityEngine;
using TMPro;

public class GrenadeShop : MonoBehaviour
{
    public Action OnBought;
    [SerializeField] private TMP_Text currentCountText;
    [SerializeField] private int price;
    [SerializeField] private int maxCount = 10;
    [SerializeField] private GameObject addGrenadeImage;
    private Money _money;

    private int _currentCount;
    public int CurrentCount
    {
        get
        {
            return _currentCount;
        }

        set
        {
            _currentCount = value;
            if (currentCountText != null)
                currentCountText.text = _currentCount.ToString();
            Progress.SaveGrenades(_currentCount);
            OnBought?.Invoke();
        }
    }

    private void OnEnable()
    {
        CurrentCount = Progress.LoadGrenades();
    }

    public void BuyOne()
    {
        if (CurrentCount >= maxCount) return;

        if (_money == null) _money = FindObjectOfType<Money>(true);

        if (_money.SpendMoney(price)) CurrentCount++;
    }

    public void BuyFull()
    {
        if (_money == null) _money = FindObjectOfType<Money>(true);

        var priceForMax = (maxCount - CurrentCount) * price;

        if (_money.SpendMoney(priceForMax))
            CurrentCount = maxCount;
    }

    public void TryRewardFull()
    {
        GSConnect.ShowRewardedAd(GSConnect.GrenadesReward);
    }

    public void RewardFull()
    {
        CurrentCount = maxCount;

        addGrenadeImage.SetActive(true);
    }
}