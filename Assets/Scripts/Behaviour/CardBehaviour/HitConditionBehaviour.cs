using NE.Config;
using NE.Core.Cards;
using NE.DataModels;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NE.Behaviour.CardBehaviour
{
    public class HitConditionBehaviour : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup[] conditionLayoutGroups = new HorizontalLayoutGroup[0];
        [SerializeField] private ConditionPrefabsSO conditionPrefabsSO;
        [SerializeField] private MonsterStateConfiguration monsterStateConfiguration;
        [SerializeField] private int cardNumber;
        private ConditionPrefabs[] conditionPrefabMapping;
        private Dictionary<string, GameObject> conditionGOMapping = new Dictionary<string, GameObject>();
        private List<string> conditions = new List<string>();
        private CardActionBehaviour parentCard;

        private void Update()
        {
            var monster = monsterStateConfiguration.Monsters.Where(x => x.CardNumber == cardNumber && x.ParentCardNumber == parentCard.CardNumber).FirstOrDefault();
            if(monster != null)
            {
                foreach (var condition in monster.ActiveConditions) {
                    if (!conditionGOMapping.ContainsKey(condition))
                    {
                        SetCondition(condition, true);
                    }
                }

                var allConditions = conditionGOMapping.Select(x => x.Key).ToList();
                var inactiveConditions = allConditions.Except(monster.ActiveConditions);
                foreach (var condition in inactiveConditions)
                {
                    SetCondition(condition, false);
                }
            }
            
        }

        private void Awake()
        {
            parentCard = GetComponentInParent<CardActionBehaviour>();
            if (conditionPrefabsSO == null)
            {
                throw new System.Exception("You need to set Condition Prefab settings");
            }
            if (conditionLayoutGroups.Length < 2)
            {
                throw new System.Exception("Expecting condition layout to have 2 rows");
            }
            conditionPrefabMapping = conditionPrefabsSO.ConditionPrefabs;
        }

        public void SetCondition(string condition, bool asActive)
        {
            var prefab = ConditionPrefabs.GetPrefab(conditionPrefabMapping, condition);
            var row = conditionGOMapping.Count < 4 ? conditionLayoutGroups[0] : conditionLayoutGroups[1];
            if (conditionGOMapping.ContainsKey(condition) && !asActive)
            {
                Destroy(conditionGOMapping[condition]);
                conditionGOMapping.Remove(condition);
            }
            else if (!conditionGOMapping.ContainsKey(condition) && asActive)
            {
                var go = Instantiate(prefab, row.gameObject.transform);
                conditionGOMapping.Add(condition, go);
            }
        }
    }
}