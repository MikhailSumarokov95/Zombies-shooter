using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Shop shop;

    private void Start()
    {
        if (!Progress.CheckSave()) shop.SetDefaultSetting();
    }
}
