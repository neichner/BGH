using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Core
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ScriptableObjectDropdownAttribute : PropertyAttribute
    {
        public ScriptableObjectDropdownAttribute() { }
    }
}
