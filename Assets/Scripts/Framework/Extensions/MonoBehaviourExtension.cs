using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.Framework.Extensions
{
    public static class MonoBehaviourExtension
    {
        public static EventTrigger AddEventEntry(this MonoBehaviour behaviour, EventTrigger trigger,
            EventTriggerType eventType, UnityAction<BaseEventData> eventCallback)
        {
            var entry = new EventTrigger.Entry
            {
                eventID = eventType
            };
            entry.callback.AddListener(eventCallback);
            trigger.triggers.Add(entry);
            
            return trigger;
        }
    }
}