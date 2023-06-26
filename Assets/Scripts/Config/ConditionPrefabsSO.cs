using NE.DataModels;
using UnityEngine;

namespace NE.Config
{
    [CreateAssetMenu(fileName = "ConditionPrefabs", menuName = "Config/ConditionPrefabs", order = 1)]
    public class ConditionPrefabsSO : ScriptableObject
    {
        public ConditionPrefabs[] ConditionPrefabs = new ConditionPrefabs[] { };
    }
}
