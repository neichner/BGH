using NE.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NE.Helpers
{
    public class UIStateHolder
    {
        public UIState previousState;
        public UIState currentState;

        public UIStateHolder(Tuple<int, int> tupleState)
        {
            previousState = (UIState)tupleState.Item1;
            currentState = (UIState)tupleState.Item2;
        }
    }

    class UIStateEventHelper
    {
        public static UIStateHolder FromIntTuple(Tuple<int, int> tupleState)
        {
            return new UIStateHolder(tupleState);
        }

        public static Tuple<int, int> ToIntTuple(UIState previousState, UIState currentState)
        {
            return new Tuple<int, int>((int)previousState, (int)currentState);
        }
    }
}
