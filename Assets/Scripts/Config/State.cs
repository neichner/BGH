using NE.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Config
{
    [CreateAssetMenu(fileName = "State", menuName = "Config/StateMachine/Create State", order = 1)]
    public class State : ScriptableObject
    {
        public string StateName;
    }
}
