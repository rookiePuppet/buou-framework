using UnityEngine;

namespace BuouFramework.Schedule
{
    public class StopwatchTimer : Timer
    {
        public StopwatchTimer(Component host) : base(host, 0) { }

        public override void Tick(float deltaTime)
        {
            if (!IsRunning || IsKilled) return;
            
            Time += deltaTime;
            Ticking?.Invoke();
        }
    }
}