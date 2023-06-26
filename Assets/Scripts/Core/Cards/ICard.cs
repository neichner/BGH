using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NE.Core.Cards
{
    interface ICard
    {
        public bool IsActive { get; }
        public int CardNumber { get; }
        public int ParentCardNumber { get; }
    }
}
