using UnityEngine;
using TMPro;

public class IconMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    public void SetMoney(int value)
    {
        moneyText.text = value.ToString();
    }
}
