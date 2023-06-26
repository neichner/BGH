using NE.Behaviour.CardBehaviour;
using NE.DataModels;
using NE.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE
{
    public class MonsterSelectorUIController : MonoBehaviour
    {
        [SerializeField] GameObject monsterCardSpawnParent;
        [SerializeField] Canvas worldSpaceCanvas;
        [SerializeField] CardActionBehaviour healthCardPrefab;
        private int i = 0;
        public void OnMonsterSelected(CardData card)
        {
            var go = Instantiate(monsterCardSpawnParent, worldSpaceCanvas.transform);
            go.name = card.Name + " Card";
            var monsterCardLow = Instantiate(card.LowLevelImage, go.transform);
            var monsterCardHigh = Instantiate(card.HighLevelImage, go.transform);
            var healthCard = Instantiate(healthCardPrefab.gameObject, go.transform);
            var cardActionBehaviour = healthCard.GetComponent<CardActionBehaviour>();
            cardActionBehaviour.SetCard(i, card, monsterCardLow, monsterCardHigh);
            healthCard.SetActive(true);
            i++;
        }
    }
}
