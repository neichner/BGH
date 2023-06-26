using System;
using System.Linq;
using UnityEngine;

namespace NE.DataModels
{
    [Serializable]
    public struct ConditionPrefabs
    {
        [Tooltip("Then name of the condition, can be anything.")]
        public string condition;
        [Tooltip("The unique 5x5 UI Image prefab.")]
        public GameObject prefab;

        public static GameObject GetPrefab(ConditionPrefabs[] conditionPrefabs, string condition)
        {
            return conditionPrefabs.First(cp => cp.condition == condition).prefab;
        }
    }
}