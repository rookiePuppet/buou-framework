using System;

namespace BuouFramework.Tweening
{
    public interface ITween
    {
        event Action Completed;
        event Action EveryTimeCompleted;
        
        object Target { get;}
        string Identifier { get; }
        bool IsCompleted { get; }
        bool IsPaused { get; set; }
        bool WasKilled { get; }
        bool IgnoreTimeScale { get; set; }
        
        void Update();
        void FullKill();
    }
}