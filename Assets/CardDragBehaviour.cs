using NE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NE
{
    public class CardDragBehaviour : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] AppEvent onDragStartEvent;
        [SerializeField] AppEvent onDragStopEvent;
        public void OnDrag(PointerEventData eventData)
        {
            transform.parent.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            onDragStartEvent.Raise();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onDragStopEvent.Raise();
        }
    }
}
