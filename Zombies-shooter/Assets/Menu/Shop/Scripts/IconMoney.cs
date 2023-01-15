using UnityEngine;
using TMPro;

public class IconMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    public void ChangeAmountOfMoney(int value)
    {
        moneyText.text = (int.Parse(moneyText.text) + value).ToString();
    }
}
