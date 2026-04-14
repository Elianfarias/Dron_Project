using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverSelector : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("References")]
    [SerializeField] private GameObject selectionLeft;
    [SerializeField] private GameObject selectionRight;

    private void Awake()
    {
        SetSelectionActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetSelectionActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetSelectionActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetSelectionActive(false);
    }

    private void SetSelectionActive(bool active)
    {
        if (selectionLeft != null)
            selectionLeft.SetActive(active);
        if (selectionRight != null)
            selectionRight.SetActive(active);
    }
}