using UnityEngine;
using TMPro;

public class PurchaseButton : GSButton
{
    [SerializeField] private string purchaseTag;
    [SerializeField] private TMP_Text isBoughtText;
    [SerializeField] private TMP_Text isNotBoughtText;

    private void OnEnable()
    {
        RefreshBoughtText();
    }

    public void RefreshBoughtText()
    {
        isBoughtText.gameObject.SetActive(GSConnect.IsBought(purchaseTag));
        isNotBoughtText.gameObject.SetActive(!GSConnect.IsBought(purchaseTag));
    }

    public void Purchase() => GSConnect.Purchase(purchaseTag);
}
