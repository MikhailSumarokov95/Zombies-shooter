using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private IconMoney[] iconsMoney;
    private int _amountOfMoney;

    private void Start()
    {
        _amountOfMoney = Progress.LoadMoney();
    }

    public bool SpendMoney(int value)
    {
        if (_amountOfMoney < value) return false;
        else
        {
            _amountOfMoney -= value;
            DisplayÑhangeMoney(- value);
            Progress.SaveMoney(_amountOfMoney);
            return true;
        }
    }

    public void MakeMoney(int value)
    {
        _amountOfMoney += value;
        DisplayÑhangeMoney(value);
        Progress.SaveMoney(_amountOfMoney);
    }

    private void DisplayÑhangeMoney(int value)
    {
        if (iconsMoney == null) return;
        foreach (var icon in iconsMoney)
            icon.ChangeAmountOfMoney(value);
    }
}
