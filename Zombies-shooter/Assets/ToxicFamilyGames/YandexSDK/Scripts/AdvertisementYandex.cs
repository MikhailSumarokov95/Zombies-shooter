using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ToxicFamilyGames.YandexSDK
{
    public class AdvertisementYandex : MonoBehaviour
    {
        private static AdvertisementYandex instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        public static extern void ShowInterstitial();
        [DllImport("__Internal")]
        public static extern void ShowRewarded(int id);
#else

        public static void ShowInterstitial()
        {
            print("Межстраничная реклама была показана!");
        }

        public static void ShowRewarded(int id)
        {
            print("Реклама за вознаграждение была показана, игрок вознаграждён!");
            instance.OnRewarded(id);
        }
#endif
    
        private static bool advIsOpen = false;
        private void OnApplicationFocus(bool focus)
        {
            if (advIsOpen) return;
            MuteVoice(!focus);
        }

        private void MuteVoice(bool value)
        {
            AudioListener.pause = value;
            AudioListener.volume = value ? 0 : 1;
        }

        #region called from JavaScript
        [SerializeField] private UnityEvent[] rewarded;
        [SerializeField] private UnityEvent onOpen, onClose;

        public void OnRewarded(int id)
        {
            rewarded[id]?.Invoke();
        }

        public void OnOpen()
        {
            advIsOpen = true;
            MuteVoice(true);
            onOpen?.Invoke();
        }

        public void OnClose()
        {
            advIsOpen = false;
            MuteVoice(false);
            onClose?.Invoke();
        }

        public void SetPrefferedLanguage(string lang)
        {
            PlayerPrefs.SetString("selectedLanguage", lang);
        }
        #endregion
    }
}