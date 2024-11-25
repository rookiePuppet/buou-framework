using UnityEngine;

namespace BuouFramework.Schedule
{
    public class CountDownTimer : Timer
    {
        public CountDownTimer(Component host, float initialTime) : base(host,initialTime) { }

        public float Remain => Time / InitialTime;

        public override void Tick(float deltaTime)
        {
            if (!IsRunning || IsKilled) return;
            
            Time -= deltaTime;
            if (Time <= 0)
            {
                Time = 0;
                Stop();
            }

            Ticking?.Invoke();
        }

        public virtual void Reset(float initialTime)
        {
            InitialTime = initialTime;
        }
    }
}