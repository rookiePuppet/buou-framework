using UnityEngine;
using UnityEngine.Events;

namespace Game.Framework.EventSystem
{
    public class EventListener<T> : MonoBehaviour
    {
        [SerializeField] private EventChanel<T> eventChanel;
        [SerializeField] private UnityEvent<T> receivedEvent;

        private void Awake()
        {
            eventChanel.AddListener(this);
        }

        private void OnDestroy()
        {
            eventChanel.RemoveListener(this);
        }

        public void Receive(T message)
        {
            receivedEvent?.Invoke(message);
        }
    }

    public class EventListener : EventListener<Empty>
    {
    }
}