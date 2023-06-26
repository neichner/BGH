using NE.Config;
using UnityEngine;

namespace NE.UI
{
    public interface IUIController
    {
        /// <summary>
        /// Only activate this window when state is set here, flag enum.
        /// </summary>
        State ActivateOnState { get; }
        void Display();
        void Hide();
    }
}
