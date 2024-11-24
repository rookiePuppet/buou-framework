using System;
using UnityEngine;

namespace Game.Framework.Timing
{
    /// <summary>
    /// 定时器
    /// <para>不可直接实例化，请使用静态方法StartNew开启新的定时器</para>
    /// </summary>
    public class Timer
    {
        /// <summary>
        /// 当计时器结束后触发的事件
        /// </summary>
        public event Action Finished;
        
        /// <summary>
        /// 当计时器执行Tick方法时会触发该事件，相当于每帧执行
        /// </summary>
        public event Action Ticking;

        private Timer()
        {
        }

        public float Duration { get; private set; }
        public float ElapsedTime { get; private set; }
        public bool IgnoreTimeScale { get; set; }
        public bool IsFinished { get; private set; }
        
        /// <summary>
        /// 是否停止
        /// <para>当计时器正在运行时设置为True，计时器将会停止运行，并被回收</para>
        /// </summary>
        public bool IsKilled { get; set; }
        
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPaused { get; set; }
        
        /// <summary>
        /// 剩余时间
        /// </summary>
        public float Remain => ElapsedTime - Duration;

        /// <summary>
        /// 更新计时器
        /// <para>由TimerManager统一执行，无需手动调用</para>
        /// </summary>
        public void Tick()
        {
            if (IsFinished || IsPaused) return;

            ElapsedTime += IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            Ticking?.Invoke();
            if (ElapsedTime >= Duration)
            {
                IsFinished = true;
                Finished?.Invoke();
            }
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        /// <param name="duration">新的时长</param>
        /// <param name="ignoreTimeScale">是否忽略时间缩放</param>
        public void Reset(float duration, bool ignoreTimeScale = false)
        {
            Duration = duration;
            ElapsedTime = 0;
            IgnoreTimeScale = ignoreTimeScale;
            IsFinished = false;
            IsKilled = false;
            IsPaused = false;

            Finished = null;
            Ticking = null;
        }

        /// <summary>
        /// 开启一个新的计时器
        /// </summary>
        /// <param name="duration">新的时长</param>
        /// <param name="ignoreTimeScale">是否忽略时间缩放</param>
        /// <returns></returns>
        public static Timer StartNew(float duration, bool ignoreTimeScale = false)
        {
            var timer = TimerManager.Instance.StartNewTimer(duration, ignoreTimeScale);
            return timer;
        }
    }
}