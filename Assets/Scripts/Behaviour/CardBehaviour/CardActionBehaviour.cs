using NE.Cards;
using NE.Config;
using NE.Core.Cards;
using NE.DataModels;
using NE.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace NE.Behaviour.CardBehaviour
{
    public class CardActionBehaviour : MonoBehaviour
    {
        public int CardNumber { get; set; }
        
        [SerializeField] private AppEvent onCardSelectedEvent;
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;
        [SerializeField] private StateMachine uiStateMachine;
        [SerializeField] private State onlyUpdateIfStateIs;

        private List<MonsterCard> monsterCards = new List<MonsterCard>();
        private GameObject monsterCardLow, monsterCardHigh;
        private Camera mainCamera;
        private CardData currentCard;
        private int currentLevel = -1;
        private TouchListener touchListener = new TouchListener();

        private void Awake()
        {
            monsterStateConfiguration.SelectedCard = null;
            mainCamera = Camera.main;
        }

        private void Update()
        {
            if (uiStateMachine.CurrentState != onlyUpdateIfStateIs)
                return;
            
            if (Input.GetMouseButtonDown(0) && currentCard != null)
            {
                var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null)
                {
                    var cardHitBox = hit.transform.GetComponent<CardHitBox>();
                    if (cardHitBox != null)
                    {
                        var parent = hit.transform.GetComponentInParent<CardActionBehaviour>();
                        if (parent.CardNumber != CardNumber)
                            return;
                        if (!AddMonsterIfNotExists(cardHitBox.CardNumber, out MonsterCard monsterCard))
                        {
                            onCardSelectedEvent?.Raise();
                        }
                        else
                        {
                            cardHitBox.OnClick();
                            monsterCards.Add(monsterCard);
                            monsterStateConfiguration.SelectedCard = monsterCard;
                        }
                    }
                }
            }
            if(monsterStateConfiguration.Level != currentLevel)
            {
                currentLevel = monsterStateConfiguration.Level;
                ChangeLevelCard(currentLevel);
                ChangeMonsterCards(currentLevel);
            }
        }

        private void ChangeMonsterCards(int currentLevel)
        {
            foreach(var card in monsterCards)
            {
                card.ChangeLevel(currentLevel);
            }
        }

        private void ChangeLevelCard(int currentLevel)
        {
            monsterCardLow.SetActive(currentLevel < 4);
            monsterCardHigh.SetActive(currentLevel >= 4);
            var rotation = currentLevel % 4;
            monsterCardLow.transform.rotation = Quaternion.Euler(0f, 0f, rotation * 90f);
            monsterCardHigh.transform.rotation = Quaternion.Euler(0f, 0f, rotation * 90f);
        }

        public void SetCard(int parentCardNumber, CardData card, GameObject monsterCardLow, GameObject monsterCardHigh)
        {
            currentCard = card;
            this.CardNumber = parentCardNumber;
            this.monsterCardLow = monsterCardLow;
            this.monsterCardHigh = monsterCardHigh;
        }

        /// <summary>
        /// Add monster to the monster configuration
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns>Returns true if added</returns>
        private bool AddMonsterIfNotExists(int cardNumber, out MonsterCard monsterCard)
        {
            monsterCard = monsterStateConfiguration.Monsters.FirstOrDefault(x => x.CardNumber == cardNumber);
            if(monsterCard != null) {
                return false;
            }
            monsterCard = new MonsterCard(currentCard, CardNumber, cardNumber, monsterStateConfiguration.Level, true, true);
            monsterStateConfiguration.Monsters.Add(monsterCard);
            return true;
        }
    }
}