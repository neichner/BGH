using UnityEngine;
using UnityEngine.Events;

namespace NE.Events
{
    public class StringAppEventListener : AppEventListener<string>
    {
        public override void OnEventRaised(string data)
        {
            Response.Invoke(data);
        }
    }
}
