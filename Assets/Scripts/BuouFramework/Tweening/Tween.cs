using System;
using BuouFramework.Logging;
using UnityEngine;

namespace BuouFramework.Tweening
{
    public class Tween<T> : ITween
    {
        /// <summary>
        /// 每帧更新时触发
        /// </summary>
        public event Action Updating;
        
        /// <summary>
        /// 补间完成时触发
        /// </summary>
        public event Action Completed;
        
        /// <summary>
        /// 每轮完成时触发（针对循环补间）
        /// </summary>
        public event Action EveryTimeCompleted;

        private readonly T _startValue;
        private readonly T _endValue;
        private readonly float _duration;
        private Action<T> _tweenUpdate;
        private EaseType _easeType = EaseType.Linear;
        private int _loopCount = 1;
        private bool _pingPong;

        private float _elapsedTime;
        private bool _reversed;
        private int _loopCompletedCount;

        public Tween(object target, string identifier, T startValue, T endValue, float duration, Action<T> tweenUpdate)
        {
            Target = target;
            Identifier = identifier;
            _startValue = startValue;
            _endValue = endValue;
            _duration = duration;
            _tweenUpdate = tweenUpdate;

            TweenManager.Instance.AddTween(this);
        }
        
        public object Target { get; private set; }
        public string Identifier { get; private set; }
        
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }
        
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPaused { get; set; }
        
        /// <summary>
        /// 是否停止
        /// </summary>
        public bool WasKilled { get; private set; }
        
        /// <summary>
        /// 是否忽略时间缩放
        /// </summary>
        public bool IgnoreTimeScale { get; set; }

        /// <summary>
        /// 目标对象是否已销毁
        /// </summary>
        public bool WasTargetDestroyed
        {
            get
            {
                if (Target is Component component && !component)
                {
                    return true;
                }
                else if (Target is Delegate del && del.Target == null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 补间更新方法
        /// <para>由TweenManager统一执行，无需手动调用</para>
        /// </summary>
        public void Update()
        {
            if (IsCompleted || IsPaused || WasKilled) return;

            if (WasTargetDestroyed)
            {
                FullKill();
                Log.Info("Target has been destroy. Tween was killed.", this);
                return;
            }

            _elapsedTime += IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;
            float t = _elapsedTime / _duration;
            float easedT = EaseHelper.Ease(_easeType, t);

            var currentValue = _reversed
                ? Interpolate(_endValue, _startValue, easedT)
                : Interpolate(_startValue, _endValue, easedT);
            _tweenUpdate?.Invoke(currentValue);

            Updating?.Invoke();

            // has completed one time
            if (_elapsedTime >= _duration)
            {
                _loopCompletedCount++;
                _elapsedTime = 0;

                if (_pingPong)
                {
                    _reversed = !_reversed;
                    EveryTimeCompleted?.Invoke();
                }

                if (_loopCompletedCount >= _loopCount && _loopCount > 0)
                {
                    Completed?.Invoke();
                    OnCompletedKill();
                }
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void FullKill()
        {
            OnCompletedKill();
            Completed = null;
            WasKilled = true;
        }

        public Tween<T> SetEase(EaseType easeType)
        {
            _easeType = easeType;
            return this;
        }

        /// <summary>
        /// 设置循环次数
        /// <para></para>
        /// </summary>
        /// <param name="loopCount">循环次数。当小于0时，补间将无限循环，并且每次循环都会逆转补间方向</param>
        /// <returns></returns>
        public Tween<T> SetPingPong(int loopCount = 1)
        {
            _loopCount = loopCount;
            _pingPong = true;
            return this;
        }

        private void OnCompletedKill()
        {
            IsCompleted = true;

            _tweenUpdate = null;
            Updating = null;

            _elapsedTime = 0f;
            _loopCount = 1;
            _loopCompletedCount = 0;
            _pingPong = false;
            _reversed = false;
            _easeType = EaseType.Linear;
        }
        
        private T Interpolate(T start, T end, float t)
        {
            if (start is float startFloat && end is float endFloat)
                return (T)(object)Mathf.LerpUnclamped(startFloat, endFloat, t);

            if (start is int startInt && end is int endInt)
                return (T)(object)Mathf.RoundToInt(Mathf.LerpUnclamped(startInt, endInt, t));

            if (start is Vector2 startVector2 && end is Vector2 endVector2)
                return (T)(object)Vector2.LerpUnclamped(startVector2, endVector2, t);

            if (start is Vector3 startVector3 && end is Vector3 endVector3)
                return (T)(object)Vector3.LerpUnclamped(startVector3, endVector3, t);

            if (start is Color startColor && end is Color endColor)
                return (T)(object)Color.LerpUnclamped(startColor, endColor, t);

            if (start is Quaternion startQuaternion && end is Quaternion endQuaternion)
                return (T)(object)Quaternion.LerpUnclamped(startQuaternion, endQuaternion, t);

            throw new NotImplementedException($"Interpolation for {typeof(T)} is not implemented");
        }
    }
}