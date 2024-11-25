using System.Collections.Generic;
using UnityEngine;

namespace BuouFramework.Schedule
{
    public sealed class PooledTimer : CountDownTimer
    {
        private PooledTimer(Component host, float initialTime) : base(host, initialTime) { }
        
        public bool IsReleased { get; private set; }

        public void Release(Queue<PooledTimer> pool)
        {
            pool.Enqueue(this);
            IsReleased = true;
        }

        public override void Stop()
        {
            if (!IsRunning) return;
            
            base.Stop();
            Kill();
        }

        public override void Reset(float initialTime)
        {
            base.Reset(initialTime);

            IsKilled = false;
            IsReleased = false;

            Started = null;
            Stopped = null;
            Ticking = null;
        }
    }
}