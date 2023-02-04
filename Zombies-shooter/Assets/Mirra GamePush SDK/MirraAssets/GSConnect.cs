using GameScore;
using System.Collections.Generic;
using UnityEngine;

public class GSConnect : MonoBehaviour {

    static GSConnect instance;

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
    // [...]

    // Ключи для rewarded награды:

    public const string
        GrenadesReward = "grenades-reward-max",
        ContinueReward = "continue-reward",
        MoneyReward = "money-reward",
        DoubleMoneyReward = "double-money-reward";


        //NewQuestsKey = "reward-new-quests",
        //ExpBoostKey = "reward-exp-boost";
        // [...]

    // Ключи для внутриигровых покупок:

    public const string
        Neuro500Key = "neuro-500",
        Neuro1000Key = "neuro-1000",
        Neuro2000Key = "neuro-2000",
        Neuro5000Key = "neuro-5000",
        Neuro10000Key = "neuro-10000",
        Bruble50000Key = "bruble-50000",
        Bruble1000000Key = "bruble-1000000";
    // [...]

    // Свойства для чтения / записи переменных игрока:

    /// <summary>
    /// Имя игрока в GameScore.
    /// Отображается в лидерборде.
    /// </summary>
    //public static string Name {
    //    get {
    //        if (Application.isEditor)
    //            return PlayerPrefs.GetString(NameKey, "Player");
    //        else return GS_Player.GetName();
    //    }
    //    set {
    //        if (Application.isEditor)
    //            PlayerPrefs.SetString(NameKey, value);
    //        else GS_Player.SetName(value);
    //    }
    //}

    ///// <summary>
    ///// Очки опыта игрока в GameScore.
    ///// Отображаются в лидерборде.
    ///// Критерий для сортировки.
    ///// </summary>
    //public static int Score {
    //    get {
    //        if (Application.isEditor)
    //            return PlayerPrefs.GetInt(ScoreKey, 0);
    //        else return (int)GS_Player.GetScore();
    //    }
    //    set {
    //        if (Application.isEditor)
    //            PlayerPrefs.SetInt(ScoreKey, value);
    //        else GS_Player.SetScore(value);
    //    }
    //}

    // [...]

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

    }

    // Социальные сети:

    /// <summary>
    /// Сделать пост в социальной сети.
    /// Текст должен быть локализован!
    /// </summary>
    public static void CreatePost(string text) {
        GS_Socials.Post(text);
    }

}