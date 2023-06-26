using NE.Cards;
using NE.Core.Cards;
using System.Collections.Generic;
using UnityEngine;

namespace NE.Config
{
    [CreateAssetMenu(fileName = "MonsterStateConfiguration", menuName = "Config/MonsterStateConfiguration", order = 1)]
    public class MonsterStateConfiguration : ScriptableObject
    {
        public List<MonsterCard> Monsters = new List<MonsterCard>();
        public MonsterCard SelectedCard;
        public int Level = 0;

        public void ChangeLevel(int level)
        {
            Level = level;
        }

        public void ChangeHealth(int health)
        {
            SelectedCard?.SetHealth(health);
        }
    }
}
