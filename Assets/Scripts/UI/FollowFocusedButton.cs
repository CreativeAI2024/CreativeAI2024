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
    private float spacing;
    private int buttonCount;
    private float viewportSize;
    private float halfViewportSize;
    private float buttonSize;
    private float normalizedButtonSize;

    void Start()
    {
        previousGameObject = EventSystem.current.currentSelectedGameObject;
        spacing = _verticalLayoutGroup.spacing;
        buttonCount = _contentTransform.childCount;
        viewportSize = _viewportRectTransform.sizeDelta.y;
        halfViewportSize = viewportSize * 0.5f;
        buttonSize = _itemButtonPrefab.sizeDelta.y + spacing;
        normalizedButtonSize = buttonSize/(buttonSize * buttonCount - viewportSize);
        _scrollRect.verticalNormalizedPosition = 1.0f;
    }

    void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        if (previousGameObject != currentSelectedGameObject)
        {
            previousGameObject = currentSelectedGameObject;
            Scroll(currentSelectedGameObject.GetComponent<ItemButton>().Index);
        }
    }

    void Scroll(int nodeIndex)
    {   
        float flippedScrollPosition = 1.0f - _scrollRect.verticalNormalizedPosition;
        float centerPosition = (buttonSize * buttonCount - viewportSize) * flippedScrollPosition + halfViewportSize;
        float topPosition = centerPosition - halfViewportSize;
        float bottomPosition = centerPosition + halfViewportSize;
        float buttonCenterPosition = buttonSize * nodeIndex + buttonSize / 2.0f - topPosition;
        Debug.Log("-----------------");
        Debug.Log("nodeCenterPosition: "+buttonCenterPosition);
        Debug.Log("centerPosition: " + centerPosition);
        Debug.Log("topPosition: " + topPosition);

        if (0 > buttonCenterPosition)
        {
            Debug.Log("Top passed.");
            _scrollRect.verticalNormalizedPosition += normalizedButtonSize;
            return;
        }

        if (buttonCenterPosition > viewportSize)
        {
            Debug.Log("Bottom passed.");
            _scrollRect.verticalNormalizedPosition -= normalizedButtonSize;
        }
    }
}
