using System.Collections.Generic;
using UnityEngine;

namespace Game.Framework.EventSystem
{
    public class EventChanel<T> : ScriptableObject
    {
        private readonly HashSet<EventListener<T>> _listeners = new();

        public void AddListener(EventListener<T> listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(EventListener<T> listener)
        {
            _listeners.Remove(listener);
        }

        public void Broadcast(T message)
        {
            foreach (var listener in _listeners)
            {
                listener.Receive(message);
            }
        }
    }

    public readonly struct Empty
    {
    }

    [CreateAssetMenu(menuName = "Events/EventChanel")]
    public class EventChanel : EventChanel<Empty>
    {
    }
}