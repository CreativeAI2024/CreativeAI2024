using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FollowFocusedButton : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private RectTransform _viewportRectTransform;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private RectTransform _itemButtonPrefab;
    [SerializeField] private VerticalLayoutGroup _verticalLayoutGroup;
    private GameObject previousGameObject;

    void Start()
    {
        previousGameObject = EventSystem.current.currentSelectedGameObject;
        _scrollRect.verticalNormalizedPosition = 1.0f;
    }
    void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedGameObject != null && previousGameObject != currentSelectedGameObject)
        {
            previousGameObject = currentSelectedGameObject;
            Scroll(currentSelectedGameObject.GetComponent<ItemButton>().Index);
        }
    }

    void Scroll(int nodeIndex)
    {
        float spacing = _verticalLayoutGroup.spacing;
        int buttonCount = _contentTransform.childCount;
        float viewportSize = _viewportRectTransform.sizeDelta.y;
        float halfViewportSize = viewportSize * 0.5f;
        float buttonSize = _itemButtonPrefab.sizeDelta.y + spacing;
        float normalizedButtonSize = buttonSize / (buttonSize * buttonCount - viewportSize);
        float flippedScrollPosition = 1.0f - _scrollRect.verticalNormalizedPosition;
        float centerPosition = (buttonSize * buttonCount - viewportSize) * flippedScrollPosition + halfViewportSize;
        float topPosition = centerPosition - halfViewportSize;
        float buttonCenterPosition = buttonSize * nodeIndex + buttonSize / 2.0f - topPosition;
        if (0 > buttonCenterPosition)
        {
            _scrollRect.verticalNormalizedPosition += normalizedButtonSize;
        }
        else if (buttonCenterPosition > viewportSize)
        {
            _scrollRect.verticalNormalizedPosition -= normalizedButtonSize;
        }
    }

    public void ScrollToTop()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }
}
