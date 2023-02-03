using UnityEngine;
using UnityEngine.EventSystems;

public class TouchSystem : MonoBehaviour, IDragHandler
{ 
    public static Vector3 Move = Vector2.zero;

    private void Awake()
    {
        if (!FindObjectOfType<LevelManager>().IsMobile)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
    }

    private void LateUpdate() => Move = Vector3.zero;

    public void OnDrag(PointerEventData eventData)
    {
        Move = eventData.delta;
    }
}