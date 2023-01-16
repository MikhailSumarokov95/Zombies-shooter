using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private int transformChildNotIsSkin;

    private void Start()
    {
        SetRandomAppearance();
    }

    private void SetRandomAppearance()
    {
        var numberSkin = Random.Range(transformChildNotIsSkin, transform.childCount);
        transform.GetChild(numberSkin).gameObject.SetActive(true);
    }
}
