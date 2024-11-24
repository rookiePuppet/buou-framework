using System;
using UnityEngine;

namespace Game.Framework.Tweening
{
    /// <summary>
    /// 通用补间方法类
    /// </summary>
    public static class Tweener
    {
        public static Tween<float> Do(Func<float> getter, Action<float> setter, float endValue, float duration)
            => Do<float>(getter, setter, endValue, duration);
        
        public static Tween<int> Do(Func<int> getter, Action<int> setter, int endValue, float duration)
            => Do<int>(getter, setter, endValue, duration);

        public static Tween<Vector2> Do(Func<Vector2> getter, Action<Vector2> setter, Vector2 endValue,
            float duration) => Do<Vector2>(getter, setter, endValue, duration);

        public static Tween<Vector3> Do(Func<Vector3> getter, Action<Vector3> setter, Vector3 endValue,
            float duration) => Do<Vector3>(getter, setter, endValue, duration);

        public static Tween<Color> Do(Func<Color> getter, Action<Color> setter, Color endValue,
            float duration) => Do<Color>(getter, setter, endValue, duration);

        public static Tween<Quaternion> Do(Func<Quaternion> getter, Action<Quaternion> setter, Quaternion endValue,
            float duration) => Do<Quaternion>(getter, setter, endValue, duration);
        
        private static Tween<TValue> Do<TValue>(Func<TValue> getter, Action<TValue> setter, TValue endValue,
            float duration)
        {
            var identifier = $"{getter.GetHashCode()}_{typeof(TValue).Name}";
            var startValue = getter();
            var tween = new Tween<TValue>(getter.Target, identifier, startValue, endValue, duration,
                value => { setter(value); });
            return tween;
        }
    }
}