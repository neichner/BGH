using NE.Config;
using UnityEngine;

namespace NE.UI
{
    public class MonsterOverlayUIController : MonoBehaviour, IUIController
    {
        [SerializeField] private GameObject monsterOverlay;
        [SerializeField] private State activateOnState;
        public State ActivateOnState => activateOnState;

        public void Display()
        {
            monsterOverlay.SetActive(true);
        }

        public void Hide()
        {
            monsterOverlay.SetActive(false);
        }
    }
}
