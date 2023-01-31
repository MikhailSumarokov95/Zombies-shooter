using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Awake()
    {
        if (!Progress.CheckSave()) FindObjectOfType<Shop>(true).SetDefaultSetting();
        FindObjectOfType<BattlePassRewarder>(true).Awake();
    }
}
