using NE.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NE
{
    public class MonsterSelectorView : MonoBehaviour
    {
        [SerializeField] private JOTL jotlData;
        [SerializeField] private GameObject monsterParent;
        [SerializeField] private GameObject monsterButtonPrefab;
        [SerializeField] private RectTransform sideBar;
        [SerializeField] private GameObject overlayBlocker;
        [SerializeField] private CardDataAppEvent onCardSelected;

        private bool sideBarActive = false;

        public void Awake()
        {
            foreach (var card in jotlData.cardData)
            {
                var gameObject = Instantiate(monsterButtonPrefab, monsterParent.transform);
                var button = gameObject.GetComponent<Button>();
                button.onClick.AddListener(() =>
                {
                    onCardSelected.Raise(card);
                });
                gameObject.GetComponentsInChildren<TMPro.TMP_Text>()[1].text = card.Name;
            }
        }

        public void OnSideBarOpenClicked()
        {
            sideBarActive = true;
            Redraw();
        }

        public void OnSideBarCloseClicked()
        {
            sideBarActive = false;
            Redraw();
        }

        public void Redraw()
        {
            sideBar.anchoredPosition = new Vector2(sideBarActive ? -sideBar.sizeDelta.x / 2 : sideBar.sizeDelta.x / 2, sideBar.anchoredPosition.y);
            overlayBlocker.SetActive(sideBarActive);
        }
    }
}
