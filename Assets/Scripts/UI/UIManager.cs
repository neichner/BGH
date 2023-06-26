using NE.Config;
using NE.Core;
using NE.Events;
using NE.Helpers;
using System.Linq;
using UnityEngine;

namespace NE.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private StateMachine stateMachine;
        [SerializeField] private State startState;
        [SerializeField] private AppEvent onUIStateChanged;

        private IUIController[] uiControllers;

        private void Awake()
        {
            stateMachine.CurrentState = startState;
            stateMachine.PreviousState = startState;
        }

        private void Start()
        {
            uiControllers = GetComponentsInChildren<IUIController>(true);
        }

        public void OnUIStateChanged()
        {
            var activeUIControllers = uiControllers.Where(x => x.ActivateOnState == stateMachine.CurrentState);
            foreach (var uiController in activeUIControllers)
            {
                uiController.Display();
            }
            
            foreach(var uiController in uiControllers.Except(activeUIControllers))
            {
                uiController.Hide();
            }
        }
    }
}