using NE.Cards;
using NE.Config;
using NE.Events;
using NE.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NE.Views
{
    public class MonsterOverlayView : MonoBehaviour, IView
    {
        [SerializeField] private TMPro.TMP_Text title;
        [SerializeField] private NumberPicker health;
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;

        void OnEnable()
        {
            var selectedCard = monsterStateConfiguration.SelectedCard;
            if (selectedCard != null)
            {
                title.text = $"{monsterStateConfiguration.SelectedCard.MonsterName} ({selectedCard.CardNumber})";
                health.SetRange(0, selectedCard.MaximumHealth + 1, selectedCard.CurrentHealth);
            }

        }
    }
}
