using NE.DataModels;
using UnityEngine;
using UnityEngine.Events;

namespace NE.Events
{
    public class CardDataAppEventListener : AppEventListener<CardData>
    {
        public override void OnEventRaised(CardData data)
        {
            Response.Invoke(data);
        }
    }
}
