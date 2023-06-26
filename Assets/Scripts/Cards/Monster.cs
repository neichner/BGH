using NE.Core.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NE.Cards
{
    [Serializable]
    public class Monster
    {
        public string Name => name;
        public int Health => health;

        [SerializeField]
        private int health;
        [SerializeField]
        private string name;

        public Monster(string name, int health)
        {
            this.name = name;
            this.health = health;
        }
    }
}
