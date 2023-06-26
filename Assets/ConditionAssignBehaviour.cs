using NE.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NE
{
    public class ConditionAssignBehaviour : MonoBehaviour
    {
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;
        [SerializeField] private ConditionPrefabsSO conditionPrefabsSO;
        private void Awake()
        {
            foreach(var conditionPrefab in conditionPrefabsSO.ConditionPrefabs)
            {
                var condition = Instantiate(conditionPrefab.prefab, transform);
                condition.name = conditionPrefab.condition;
                condition.GetComponent<ConditionClickBehaviour>().enabled = true;
            }
        }
    }
}
