using System.Collections.Generic;
using UnityEngine;

namespace NE.Events
{
    [CreateAssetMenu]
    public class AppEvent: ScriptableObject
    {
        /// <summary>
        /// Description of the event. 
        /// This helps for later reference when visually using or editing app events.
        /// </summary>
        [TextArea(3, 10)]
        public string Description;
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        protected readonly List<AppEventListener> eventListeners =
            new List<AppEventListener>();

        public virtual void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised();
        }

        public void RegisterListener(AppEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(AppEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }

    public abstract class AppEvent<T> : ScriptableObject
    {
        /// <summary>
        /// Description of the event. 
        /// This helps for later reference when visually using or editing app events.
        /// </summary>
        [TextArea(3, 10)]
        public string Description;
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        protected readonly List<AppEventListener<T>> eventListeners =
            new List<AppEventListener<T>>();

        public virtual void Raise(T data)
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].OnEventRaised(data);
        }

        public void RegisterListener(AppEventListener<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(AppEventListener<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}
