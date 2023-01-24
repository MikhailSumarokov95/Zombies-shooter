using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private bool isMobile;
    public bool IsMobile { get { return isMobile; } private set { isMobile = value; } }
}
