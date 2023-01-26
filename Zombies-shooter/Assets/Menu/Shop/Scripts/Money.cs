using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private IconMoney[] iconsMoney;

    private int _amountOfMoney;
    public int AmountOfMoney
    {
        get
        {
            return _amountOfMoney;
        }

        set
        { 
            _amountOfMoney = value;
            PlayerPrefs.SetInt("money", _amountOfMoney);
            Display—hangeMoney(_amountOfMoney);
        }
    }

    private void Start()
    {
        AmountOfMoney = PlayerPrefs.GetInt("money", 0);
    }

    public bool SpendMoney(int value)
    {
        if (AmountOfMoney < value) return false;
        else
        {
            AmountOfMoney -= value;
            return true;
        }
    }

    public void MakeMoney(int value)
    {
        AmountOfMoney += value;
    }

    private void Display—hangeMoney(int value)
    {
        if (iconsMoney == null) return;
        foreach (var icon in iconsMoney)
            icon.SetMoney(value);
    }
}
