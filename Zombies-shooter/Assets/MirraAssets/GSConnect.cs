using GameScore;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GSConnect : MonoBehaviour {

    static GSConnect instance;

    public static Action OnPurchaseWeapon;

    /// <summary>
    /// Состояние инициализации SDK.
    /// </summary>
    public static bool Ready => ready;
    static bool ready = false;

    /// <summary>
    /// Полностью отключает звук и игру.
    /// </summary>
    public static bool Pause {
        get => Time.timeScale == 0;
        set {
            Time.timeScale = value ? 0 : 1;
            AudioListener.pause = value;
        }
    }

    // Ключи для переменных игрока:

    //public const string
    //    NameKey = "key-name",
    //    ScoreKey = "key-score";

    // Ключи для rewarded награды:

    public const string
        GrenadesReward = "grenades-reward-max",
        ContinueReward = "continue-reward",
        MoneyReward = "money-reward",
        DoubleMoneyReward = "double-money-reward";

    // Ключи для внутриигровых покупок:

    public const string
        GrenadeLauncher = "GrenadeLauncher",
        RocketLauncher = "RocketLauncher",
        Battlepass = "Battlepass";

    //Свойства для чтения / записи переменных игрока:

    /// <summary>
    /// Имя игрока в GameScore.
    /// Отображается в лидерборде.
    /// </summary>
    //public static string Name
    //{
    //    get
    //    {
    //        return GS_Player.GetName();
    //    }
    //    set
    //    {
    //        GS_Player.SetName(value);
    //    }
    //}

    /// <summary>
    /// Очки опыта игрока в GameScore.
    /// Отображаются в лидерборде.
    /// Критерий для сортировки.
    /// </summary>
    //public static int Score
    //{
    //    get
    //    {
    //        return (int)GS_Player.GetScore();
    //    }
    //    set
    //    {
    //        GS_Player.SetScore(value);
    //    }
    //}


    /// <summary>
    /// Вызывать сразу после важных событий,
    /// чтобы сохранить изменения на сервер.
    /// </summary>
    public static void Sync() {
        Debug.Log("GamePush: Sync request.");
        PlayerPrefs.Save();
        if (!Application.isEditor)
            GS_Player.Sync();
    }

    // Unity события:

    void OnEnable() {
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        // Подключить события для SDK.
        GS_SDK.OnReady += OnReady;
        // Межстраничная реклама.
        GS_Ads.OnFullscreenClose += OnMidgameClosed;
        // Rewarded реклама.
        GS_Ads.OnRewardedReward += OnRewardedSuccess;
        GS_Ads.OnRewardedClose += OnRewardedClosed;
        // In-app покупки.
        GS_Payments.OnPaymentsFetchProducts += OnFetchProductsSuccess;
        GS_Payments.OnPaymentsPurchase += OnPurchaseSuccess;
    }

    void OnDisable() {
        // Отключить события от SDK.
        GS_SDK.OnReady -= OnReady;
        // Межстраничная реклама.
        GS_Ads.OnFullscreenClose -= OnMidgameClosed;
        // Rewarded реклама.
        GS_Ads.OnRewardedReward -= OnRewardedSuccess;
        GS_Ads.OnRewardedClose -= OnRewardedClosed;
        // In-app покупки.
        GS_Payments.OnPaymentsFetchProducts -= OnFetchProductsSuccess;
        GS_Payments.OnPaymentsPurchase -= OnPurchaseSuccess;
    }

    // События SDK:

    void OnReady() {
        ready = true;
        if (Application.isEditor) {
            Debug.Log("GamePush: Fetch products.");
        }
        else GS_Payments.FetchProducts();
        Debug.Log("GamePush: SDK ready.");
    }

    // Межстраничная реклама:

    public static void ShowMidgameAd() {
        if (Application.isEditor) {
            Debug.Log("GamePush: Midgame AD.");
            return;
        }
        if (!GS_Ads.IsFullscreenAvailable()) return;
        GS_Ads.ShowFullscreen();
        Pause = true;
    }

    void OnMidgameClosed(bool success) {
        Pause = false;
    }

    // Rewarded реклама:

    /// <summary>
    /// Безопасная проверка доступности
    /// Rewarded рекламы.
    /// </summary>
    public static bool RewardedAvailable {
        get {
            if (Application.isEditor) return true;
            return GS_Ads.IsRewardedAvailable();
        }
    }

    /// <summary>
    /// Безопасная проверка доступности
    /// межстраничной рекламы.
    /// </summary>
    public static bool MidgameAvailable {
        get {
            if (Application.isEditor) return true;
            return GS_Ads.IsFullscreenAvailable();
        }
    }

    public static void ShowRewardedAd(string reward) {
        if (Application.isEditor) {
            Debug.Log($"GamePush: Rewarded AD {reward}.");
            instance.OnRewardedSuccess(reward);
            return;
        }
        if (!GS_Ads.IsRewardedAvailable()) return;
        GS_Ads.ShowRewarded(reward);
        Pause = true;
    }

    /// <summary>
    /// Rewarded успешно просмотрен,
    /// дать игроку его награду.
    /// </summary>
    void OnRewardedSuccess(string reward) {
        switch (reward) {
            case ContinueReward:
                {
                    FindObjectOfType<LevelManager>().Respawn();
                    break;
                }
            case GrenadesReward:
                {
                    FindObjectOfType<GrenadeShop>().RewardFull();
                    break;
                }
            case MoneyReward:
                {
                    FindObjectOfType<Money>().MakeMoney(1000);
                    break;
                }
            case DoubleMoneyReward:
                {
                    FindObjectOfType<RewardGame>().CountRewardPerWave(2);
                    break;
                }
        }
        Sync();
    }

    void OnRewardedClosed(bool success) {
        Pause = false;
    }

    // In-app покупки:

    public static bool ProductsReady => productsReady;
    static bool productsReady = false;

    /// <summary>
    /// Перечень товаров и их цен успешно получен.
    /// </summary>
    void OnFetchProductsSuccess(List<FetchProducts> products) {
        prices.Clear();
        foreach (var product in products) {
            prices.Add(
                product.tag,
                $"{product.price} {product.currencySymbol}"
            );
        }
        productsReady = true;
    }

    static readonly Dictionary<string, string> prices = new();

    /// <summary>
    /// Возвращает цену на товар в виде:
    /// "100 YAN", "50 Голосов", итд.
    /// При неудаче, возвращает "Error".
    /// </summary>
    public static string GetPrice(string purchaseTag) {
        return prices.GetValueOrDefault(purchaseTag, "Error");
    }

    /// <summary>
    /// Начать процесс покупки товара.
    /// </summary>
    public static void Purchase(string purchaseTag) {
        if (Application.isEditor) {
            Debug.Log($"GamePush: Purchase {purchaseTag}.");
            instance.OnPurchaseSuccess(purchaseTag);
            return;
        }
        GS_Payments.Purchase(purchaseTag);
    }

    /// <summary>
    /// Товар приобретен успешно,
    /// дать игроку то за что он платил.
    /// </summary>
    void OnPurchaseSuccess(string purchaseTag)
    {
        switch (purchaseTag)
        {
            case GrenadeLauncher:
                var boughtGL = Progress.LoadWeaponsBought();
                boughtGL.WeaponsAttachmentsBought["Grenade Launcher 01"].IsBoughtWeapon = true;
                Progress.SaveWeaponsBought(boughtGL);

                OnPurchaseWeapon?.Invoke();
                break;

            case RocketLauncher:
                var boughtRL = Progress.LoadWeaponsBought();
                boughtRL.WeaponsAttachmentsBought["Rocket Launcher 01"].IsBoughtWeapon = true;
                Progress.SaveWeaponsBought(boughtRL);

                OnPurchaseWeapon?.Invoke();
                break;

            case Battlepass:
                FindObjectOfType<BattlePassRewarder>(true).BoughtBattlePass();
                break;
        }

        var purchaseButtons = FindObjectsOfType<PurchaseButton>();
        foreach (var button in purchaseButtons)
            button.RefreshBoughtText();
    }

    // Социальные сети:

    /// <summary>
    /// Сделать пост в социальной сети.
    /// Текст должен быть локализован!
    /// </summary>
    public static void CreatePost(string text) {
        GS_Socials.Post(text);
    }

    public static bool IsBought(string purchaseTag)
    {
        switch (purchaseTag) 
        {
            case GrenadeLauncher:
                var boughtGL = Progress.LoadWeaponsBought();
                return boughtGL.WeaponsAttachmentsBought["Grenade Launcher 01"].IsBoughtWeapon;
                    
            case RocketLauncher:
                var boughtRL = Progress.LoadWeaponsBought();
                return boughtRL.WeaponsAttachmentsBought["Rocket Launcher 01"].IsBoughtWeapon;

            case Battlepass:
                return Progress.LoadBattlePass();

                default: return false;
        }
    }
}