using UnityEngine;

public class MobileUIController : MonoBehaviour
{
    [SerializeField] private GameObject[] mobileUI;

    private void Start()
    {
        var isMobile = FindObjectOfType<LevelManager>().IsMobile;

        foreach(var UI in mobileUI)
        {
            UI.SetActive(isMobile);
        }
    }
}
