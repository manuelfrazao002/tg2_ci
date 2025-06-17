using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, ICustomDrag
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnCurrentDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        // Convert screen position to local UI position within the canvas
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
            out localPoint))
        {
            rectTransform.localPosition = localPoint;
        }
    }
}