using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using BuouFramework.Logging;
using BuouFramework.SingleInstance;
using UnityEngine;

namespace BuouFramework.Schedule
{
    public class TimerManager : MonoSingleton<TimerManager>
    {
        [SerializeField] private float pooledTimersCount;
        [SerializeField] private float totalTimersCount;

        private readonly Queue<PooledTimer> _timerPool = new();
        private readonly HashSet<Timer> _timers = new();
        private readonly List<Timer> _timersToRemove = new();

        private void Update()
        {
            foreach (var timer in _timers)
            {
                if (!timer.Host)
                {
                    timer.Kill();
                    Log.Info("Timer is killed because its host is lost.", this);
                }

                if (timer.IsKilled)
                {
                    HandleKilledTimer(timer);
                }

                timer.Tick(timer.IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
            }

            if (_timersToRemove.Count > 0)
            {
                foreach (var timer in _timersToRemove)
                {
                    _timers.Remove(timer);
                }

                _timersToRemove.Clear();
            }

#if UNITY_EDITOR
            totalTimersCount = _timers.Count;
            pooledTimersCount = _timerPool.Count;
#endif
        }

        private void HandleKilledTimer(Timer timer)
        {
            if (timer is PooledTimer pooledTimer)
            {
                if (pooledTimer.IsReleased) return;

                pooledTimer.Release(_timerPool);
            }
            else
            {
                _timersToRemove.Add(timer);
            }
        }

        /// <summary>
        /// 注册一个计时器
        /// </summary>
        /// <param name="timer">计时器对象</param>
        public async void RegisterTimer(Timer timer)
        {
            if (timer is PooledTimer) return;

            await Awaitable.NextFrameAsync();
            _timers.Add(timer);
        }

        /// <summary>
        /// 获取池化计时器
        /// </summary>
        /// <param name="time">倒计时时长</param>
        /// <returns></returns>
        public PooledTimer GetPooledTimer(float time)
        {
            if (_timerPool.Count > 0)
            {
                var timer = _timerPool.Dequeue();
                timer.Reset(time);
                pooledTimersCount = _timerPool.Count;

                return timer;
            }
            else
            {
                var timer = Activator.CreateInstance(typeof(PooledTimer),
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null, new object[] { Instance, time }, CultureInfo.CurrentCulture) as PooledTimer;
                timer?.Reset(time);
                _timers.Add(timer);

                return timer;
            }
        }

        /// <summary>
        /// 启动倒计时
        /// </summary>
        /// <param name="time">倒计时时长</param>
        /// <returns></returns>
        public static PooledTimer CountDown(float time)
        {
            var timer = Instance.GetPooledTimer(time);
            timer.Start();
            return timer;
        }
    }
}