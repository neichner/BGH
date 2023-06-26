using NE.Config;
using NE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NE
{
    public class ConditionClickBehaviour : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;
        private Image image;
        private void Awake()
        {
            image = GetComponent<Image>();
            
        }

        private void OnEnable()
        {
            Redraw();
        }

        public void OnConditionClicked(string conditionName)
        {
            monsterStateConfiguration.SelectedCard.ToggleCondition(conditionName);
            Redraw();
        }

        private void Redraw()
        {
            image.color = new Color(
                image.color.r, 
                image.color.g, 
                image.color.b, 
                monsterStateConfiguration.SelectedCard.IsConditionActive(gameObject.name) ? 1f : 0.1f
                );
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnConditionClicked(gameObject.name);
        }
    }
}
