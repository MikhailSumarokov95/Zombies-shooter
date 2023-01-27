using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        if (!Progress.CheckSave()) FindObjectOfType<Shop>(true).SetDefaultSetting();
        FindObjectOfType<BattlePassRewarder>(true).Awake();
    }
}
