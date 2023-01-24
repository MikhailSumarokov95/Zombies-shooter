using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.Networking;
using System.Threading.Tasks;

namespace ToxicFamilyGames.YandexSDK
{
    public class AuthorizationYandex : MonoBehaviour
    {
#if !UNITY_EDITOR && UNITY_WEBGL
        [DllImport("__Internal")]
        public static extern bool IsMobile();
        [DllImport("__Internal")]
        private static extern void Authorization(bool scopes, string photoSize);
#else
        public static bool IsMobile()
        {
            return false;
        }
        private static void Authorization(bool scopes, string photoSize)
        {
            data = JsonUtility.FromJson<PlayerData>("{\"id\":\"8\",\"name\":\"Programmer\",\"photoURL\": null}");
            print(data);
        }
#endif
        [SerializeField] private PhotoSize photoSize;
        public static PlayerData data { get; private set; }
        public static bool AuthorizationStatus { get; private set; }
        private void Awake()
        {
            PlayerData.photoSize = photoSize;
        }
        private void Start()
        {
            Authorization(false, PlayerData.photoSize.ToString());
        }
        public static void Authorization()
        {
            Authorization(true, PlayerData.photoSize.ToString());
        }

        #region called from JavaScript
        [SerializeField] private UnityEvent onAuthorizationFailed, onAuthorizationPassed;
        private void SetPlayerData(string data)
        {
            AuthorizationYandex.data = JsonUtility.FromJson<PlayerData>(data);
        }

        public void OnAuthorizationFailed()
        {
            SetPlayerData("{\"id\": null,\"name\": null,\"photoURL\": null}");
            AuthorizationStatus = false;
            onAuthorizationFailed?.Invoke();
        }

        public void OnAuthorizationPassed(string data)
        {
            SetPlayerData(data);
            print(data);
            AuthorizationStatus = true;
            onAuthorizationPassed?.Invoke();
        }
        #endregion
    }

    [System.Serializable]
    public class PlayerData
    {
        public static PhotoSize photoSize = PhotoSize.small;
        [SerializeField]
        private string id = null;
        [SerializeField]
        private string name = null;
        [SerializeField]
        private string photoURL = null;
        public string Id { get { return id; } }
        public string Name { get { return name; } }
        public override string ToString()
        {
            return id + " " + name + "\n" + photoURL;
        }
    }
    public enum PhotoSize
    {
        small,
        medium,
        large
    }
}