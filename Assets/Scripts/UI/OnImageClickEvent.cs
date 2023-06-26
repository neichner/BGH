using NE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NE.UI
{
    public class OnImageClickEvent : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private AppEvent onClick;
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick.Raise();
        }
    }
}
