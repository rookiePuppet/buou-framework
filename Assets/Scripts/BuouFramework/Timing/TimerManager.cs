using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using BuouFramework.SingleInstance;

namespace BuouFramework.Timing
{
    /// <summary>
    /// 集中处理所有的定时器，使用了对象池管理定时器对象
    /// </summary>
    public class TimerManager : MonoSingleton<TimerManager>
    {
        private readonly Queue<Timer> _timerPool = new();
        private readonly List<Timer> _activeTimers = new();

        private void Update()
        {
            for (var i = _activeTimers.Count - 1; i >= 0; i--)
            {
                var timer = _activeTimers[i];
                timer.Tick();
                if (timer.IsFinished || timer.IsKilled)
                {
                    _activeTimers.RemoveAt(i);
                    _timerPool.Enqueue(timer);
                }
            }
        }

        /// <summary>
        /// 开启一个新的计时器
        /// </summary>
        /// <param name="duration">新的时长</param>
        /// <param name="ignoreTimeScale">是否忽略时间缩放</param>
        /// <returns></returns>
        public Timer StartNewTimer(float duration, bool ignoreTimeScale = false)
        {
            var timer = GetTimerFromPool();
            timer.Reset(duration, ignoreTimeScale);
            _activeTimers.Add(timer);

            return timer;
        }

        /// <summary>
        /// 从对象池中取出一个定时器对象
        /// 若对象池没有空闲对象，则通过反射进行实例化
        /// </summary>
        /// <returns></returns>
        private Timer GetTimerFromPool()
        {
            if (_timerPool.Count > 0)
            {
                return _timerPool.Dequeue();
            }

            return Activator.CreateInstance(typeof(Timer), BindingFlags.Instance | BindingFlags.NonPublic,
                null, null, CultureInfo.CurrentCulture) as Timer;
        }
    }
}