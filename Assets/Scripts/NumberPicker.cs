using NE.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public class NumberPickerEvent : UnityEvent<int> {}

public class NumberPicker : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public NumberPickerEvent onNumberSelected;
    [SerializeField] private RectTransform content;
    [SerializeField] private ObjectPoolBehaviour objectPool;
    [SerializeField] private Vector2Int numberRange = Vector2Int.zero;
    private float contentHeight = 0f;
    private float sliderMinY = 0f;
    private float sliderMaxY = 0f;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        contentHeight = rectTransform.sizeDelta.y;
    }

    private void Start()
    {
        if (numberRange != Vector2Int.zero)
        {
            SetRange(numberRange.x, numberRange.y);
        }
    }

    /// <summary>
    /// Set the range of numbers to display.
    /// </summary>
    /// <param name="start">Set start of range, inclusive</param>
    /// <param name="end">Set end of range, exclusive</param>
    /// <param name="value">Set the initial value, defaults to start</param>
    public void SetRange(int start, int end, int? value = null)
    {
        content.gameObject.SetActive(true);
        Clear();

        for (int i = start; i < end; i++)
        {
            var obj = objectPool.GetPooledObject();
            var numberRT = obj.GetComponent<RectTransform>();
            numberRT.sizeDelta = rectTransform.sizeDelta;
            obj.transform.parent = content.transform;
            var tmproText = obj.GetComponent<TMPro.TMP_Text>();
            tmproText.text = i.ToString();
            tmproText.ForceMeshUpdate();
            obj.SetActive(true);
        }
        var totalContentHeight = contentHeight * (end - start);
        content.sizeDelta = new Vector2(content.sizeDelta.x, totalContentHeight);

        sliderMinY = 0f;
        sliderMaxY = content.sizeDelta.y - contentHeight;
        SetNumber(value ?? start);
    }

    private void Clear()
    {
        List<GameObject> toReturnToPool = new List<GameObject>();
        foreach (Transform child in content)
        {
            // This seems redundant, but Unity will return every other object if you change the list while looping.
            toReturnToPool.Add(child.gameObject);
        }

        foreach (var child in toReturnToPool)
        {
            objectPool.ReturnObjectToPool(child.gameObject);
        }
    }

    private void SetNumber(int number)
    {
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, ((float)number * contentHeight) + sliderMinY);
    }

    public void OnDrag(PointerEventData eventData)
    {
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, content.anchoredPosition.y + eventData.delta.y);
        var clampedHeight = Mathf.Clamp(content.anchoredPosition.y, sliderMinY, sliderMaxY);
        content.anchoredPosition = new Vector2(content.anchoredPosition.x, clampedHeight);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find nearest selection
        var nearestFloat = ((content.anchoredPosition.y + sliderMinY) / contentHeight);
        var nearestInt = Mathf.RoundToInt(nearestFloat);
        SetNumber(nearestInt);
        onNumberSelected?.Invoke(nearestInt);
    }
}
