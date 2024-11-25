using System;
using BuouFramework.Logging;
using UnityEngine;

namespace BuouFramework.Schedule
{
    public abstract class Timer
    {
        protected Action Started;
        protected Action Ticking;
        protected Action Stopped;
        protected float InitialTime;

        protected Timer(Component host, float initialTime)
        {
            Host = host;
            InitialTime = initialTime;
        }

        public Component Host { get; protected set; }
        public float Time { get; protected set; }
        public bool IsRunning { get; protected set; }
        public bool IsKilled { get; protected set; }
        public bool IgnoreTimeScale { get; set; }

        public virtual void Start()
        {
            if (IsRunning) return;

            Time = InitialTime;
            IsRunning = true;
            Started?.Invoke();
        }

        public virtual void Stop()
        {
            if (IsRunning)
            {
                IsRunning = false;
                Stopped?.Invoke();
            }
        }

        public virtual void Kill()
        {
            IsRunning = false;
            IsKilled = true;
        }

        public virtual Timer OnStarted(Action action)
        {
            Started += action;
            return this;
        }

        public virtual Timer OnTicking(Action action)
        {
            Ticking += action;
            return this;
        }

        public virtual Timer OnStopped(Action action)
        {
            Stopped += action;
            return this;
        }
        
        public virtual void Pause() => IsRunning = false;
        public virtual void Resume() => IsRunning = true;

        public abstract void Tick(float deltaTime);
    }
}