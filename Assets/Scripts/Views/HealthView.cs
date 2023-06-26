using NE.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace NE
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;
        [SerializeField] private TMPro.TMP_Text healthValue;
        private int cardNumber;
        void Awake()
        {
            var hitbox = GetComponentInParent<CardHitBox>();
            cardNumber = hitbox.CardNumber;
        }

        void Update()
        {
            var monsterCard = monsterStateConfiguration.Monsters.FirstOrDefault(x => x.CardNumber == cardNumber);
            if (monsterCard != null)
                healthValue.text = monsterCard.CurrentHealth.ToString();
        }
    }
}
