using NE.Core;
using NE.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Config
{
    [CreateAssetMenu(fileName = "StateMachine", menuName = "Config/StateMachine/Create State Machine", order = 1)]
    public class StateMachine : ScriptableObject
    {
        [Tooltip("Write down all states, then they can be configured in modules")]
        public State[] States;
        public State CurrentState;
        public State PreviousState;
        public AppEvent onStateChanged;

        public void SetState(State state)
        {
            PreviousState = CurrentState;
            CurrentState = state;
            onStateChanged?.Raise();
        }
    }
}
