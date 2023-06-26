using NE.Core.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE
{
    public class CardHitBox : MonoBehaviour
    {
        public int CardNumber;
        private HealthView healthView;
        private void Awake()
        {
            healthView = GetComponentInChildren<HealthView>();
            healthView.gameObject.SetActive(false);
        }

        public void OnClick()
        {
            healthView.gameObject.SetActive(true);
        }
    }
}
