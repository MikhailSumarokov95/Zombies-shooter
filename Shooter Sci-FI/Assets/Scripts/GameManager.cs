using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isMobile;
    public bool IsMobile { get { return isMobile; } private set { isMobile = value; } }
}
