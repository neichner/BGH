using UnityEngine;
using UnityEngine.Events;

namespace NE.Events
{
    public class IntAppEventListener : AppEventListener<int>
    {
        public override void OnEventRaised(int data)
        {
            Response.Invoke(data);
        }
    }
}
