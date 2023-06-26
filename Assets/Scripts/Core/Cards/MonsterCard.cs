using NE.Cards;
using NE.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NE.Core.Cards
{
    [Serializable]
    public class MonsterCard : ICard
    {
        public int MonsterLevel { private set; get; }
        public string MonsterName { get { return monster.Name; } }
        public List<string> ActiveConditions { get { return activeConditions; } }

        public bool IsElite { private set; get; }

        public int MaximumHealth {
            get {
                return IsElite ? monster.EliteHealthLevels[MonsterLevel] : monster.Normal[MonsterLevel];
            }
        }

        public int CurrentHealth { get; private set; }

        public bool IsActive => isActive;
        public int CardNumber => monsterNumber;
        public int ParentCardNumber => parentCardNumber;

        [SerializeField] private int parentCardNumber;
        [SerializeField] private int monsterNumber;
        [SerializeField] private bool isActive;
        [SerializeField] private CardData monster;
        [SerializeField] private List<string> activeConditions = new List<string>();

        public MonsterCard(CardData monster, int parentCardNumber, int monsterNumber, int monsterLevel, bool isElite, bool isActive)
        {
            this.parentCardNumber = parentCardNumber;
            this.monsterNumber = monsterNumber;
            MonsterLevel = monsterLevel;
            this.isActive = isActive;
            this.monster = monster;
            IsElite = isElite;
            CurrentHealth = MaximumHealth;
        }

        public void SetHealth(int newHealth)
        {
            CurrentHealth = newHealth;
        }

        public void ChangeLevel(int newLevel)
        {
            var changeCurrentHealth = CurrentHealth == MaximumHealth;
            MonsterLevel = newLevel;
            if(changeCurrentHealth)
            {
                CurrentHealth = MaximumHealth;
            }
        }

        public bool IsConditionActive(string condition)
        {
            return activeConditions.Contains(condition);
        }

        public bool ToggleCondition(string condition)
        {
            var active = false;
            if(!activeConditions.Remove(condition))
            {
                active = true;
                activeConditions.Add(condition);
            }
            return active;
        }
    }
}