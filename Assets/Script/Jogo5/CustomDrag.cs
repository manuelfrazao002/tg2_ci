using UnityEngine;
using UnityEngine.EventSystems;

public interface ICustomDrag
{
    void OnCurrentDrag(PointerEventData eventData); // ← Pass event data for position
}

public class CustomDrag : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject objectToInteractWith;
    private ICustomDrag onDrag;

    void Start()
    {
        onDrag = objectToInteractWith.GetComponent<ICustomDrag>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        onDrag.OnCurrentDrag(eventData); // ← Pass event data
    }
}
