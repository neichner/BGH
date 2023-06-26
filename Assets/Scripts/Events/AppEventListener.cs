using UnityEngine;
using UnityEngine.Events;

namespace NE.Events
{
    public class AppEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public AppEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public virtual void OnEventRaised() 
        {
            Response.Invoke();
        }
    }

    public abstract class AppEventListener<T> : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public AppEvent<T> Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<T> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public virtual void OnEventRaised(T data) { }
    }
}
