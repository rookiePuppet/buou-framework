using System;

namespace BuouFramework.EventSystem
{
    public abstract class Event<T> : IEvent
    {
        public event Action<T> Triggered;

        public virtual void Trigger(T args)
        {
            Triggered?.Invoke(args);
        }
    }

    public abstract class Event : Event<EmptyArgs>
    {
    }

    public interface IEvent
    {
    }

    public readonly struct EmptyArgs
    {
    }
}