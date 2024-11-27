using UnityEngine;

namespace BuouFramework.Tweening
{
    /// <summary>
    /// 缓动方法类
    /// </summary>
    public static class EaseHelper
    {
        public static float Ease(EaseType easeType, float t) => easeType switch
        {
            EaseType.Linear => t,
            EaseType.QuadraticIn => QuadIn(t),
            EaseType.QuadraticOut => QuadOut(t),
            EaseType.QuadraticInOut => QuadInOut(t),
            EaseType.CubicIn => CubicIn(t),
            EaseType.CubicOut => CubicOut(t),
            EaseType.CubicInOut => CubicInOut(t),
            EaseType.QuarticIn => QuartIn(t),
            EaseType.QuarticOut => QuartOut(t),
            EaseType.QuarticInOut => QuartInOut(t),
            EaseType.QuinticIn => QuintIn(t),
            EaseType.QuinticOut => QuintOut(t),
            EaseType.QuinticInOut => QuintInOut(t),
            EaseType.SineIn => SineIn(t),
            EaseType.SineOut => SineOut(t),
            EaseType.SineInOut => SineInOut(t),
            EaseType.CircularIn => CircularIn(t),
            EaseType.CircularOut => CircularOut(t),
            EaseType.CircularInOut => CircularInOut(t),
            EaseType.ExponentialIn => ExponentialIn(t),
            EaseType.ExponentialOut => ExponentialOut(t),
            EaseType.ExponentialInOut => ExponentialInOut(t),
            EaseType.ElasticIn => ElasticIn(t),
            EaseType.ElasticOut => ElasticOut(t),
            EaseType.ElasticInOut => ElasticInOut(t),
            EaseType.BackIn => BackIn(t),
            EaseType.BackOut => BackOut(t),
            EaseType.BackInOut => BackInOut(t),
            EaseType.BounceIn => BounceIn(t),
            EaseType.BounceOut => BounceOut(t),
            EaseType.BounceInOut => BounceInOut(t),
            _ => t
        };

        // 二次方缓入
        public static float QuadIn(float t)
        {
            return t * t;
        }

        // 二次方缓出
        public static float QuadOut(float t)
        {
            return t * (2 - t);
        }

        // 二次方缓入缓出
        public static float QuadInOut(float t)
        {
            if (t < 0.5f)
                return 2 * t * t;
            else
                return -1 + (4 - 2 * t) * t;
        }

        // 三次方缓入
        public static float CubicIn(float t)
        {
            return t * t * t;
        }

        // 三次方缓出
        public static float CubicOut(float t)
        {
            float f = t - 1;
            return f * f * f + 1;
        }

        // 三次方缓入缓出
        public static float CubicInOut(float t)
        {
            if (t < 0.5f)
                return 4 * t * t * t;
            else
            {
                float f = (2 * t) - 2;
                return 0.5f * f * f * f + 1;
            }
        }

        // 四次方缓入
        public static float QuartIn(float t)
        {
            return t * t * t * t;
        }

        // 四次方缓出
        public static float QuartOut(float t)
        {
            float f = t - 1;
            return f * f * f * (1 - t) + 1;
        }

        // 四次方缓入缓出
        public static float QuartInOut(float t)
        {
            if (t < 0.5f)
                return 8 * t * t * t * t;
            else
            {
                float f = t - 1;
                return -8 * f * f * f * f + 1;
            }
        }

        // 五次方缓入
        public static float QuintIn(float t)
        {
            return t * t * t * t * t;
        }

        // 五次方缓出
        public static float QuintOut(float t)
        {
            float f = t - 1;
            return f * f * f * f * f + 1;
        }

        // 五次方缓入缓出
        public static float QuintInOut(float t)
        {
            if (t < 0.5f)
                return 16 * t * t * t * t * t;
            else
            {
                float f = (2 * t) - 2;
                return 0.5f * f * f * f * f * f + 1;
            }
        }

        // 正弦缓入
        public static float SineIn(float t)
        {
            return 1 - Mathf.Cos(t * Mathf.PI / 2);
        }

        // 正弦缓出
        public static float SineOut(float t)
        {
            return Mathf.Sin(t * Mathf.PI / 2);
        }

        // 正弦缓入缓出
        public static float SineInOut(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }

        // 圆形缓入
        public static float CircularIn(float t)
        {
            return 1 - Mathf.Sqrt(1 - t * t);
        }

        // 圆形缓出
        public static float CircularOut(float t)
        {
            return Mathf.Sqrt((2 - t) * t);
        }

        // 圆形缓入缓出
        public static float CircularInOut(float t)
        {
            if (t < 0.5f)
                return 0.5f * (1 - Mathf.Sqrt(1 - 4 * t * t));
            else
                return 0.5f * (Mathf.Sqrt((-2 * t + 2) * (2 * t - 2)) + 1);
        }

        // 指数缓入
        public static float ExponentialIn(float t)
        {
            return Mathf.Pow(2, 10 * (t - 1));
        }

        // 指数缓出
        public static float ExponentialOut(float t)
        {
            return (-Mathf.Pow(2, -10 * t) + 1);
        }

        // 指数缓入缓出
        public static float ExponentialInOut(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            if (t < 0.5f)
                return 0.5f * Mathf.Pow(2, 20 * t - 10);
            else
                return -0.5f * Mathf.Pow(2, -20 * t + 10) + 1;
        }

        // 弹性缓入
        public static float ElasticIn(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            return Mathf.Sin(13 * Mathf.PI / 2 * t) * Mathf.Pow(2, 10 * (t - 1));
        }

        // 弹性缓出
        public static float ElasticOut(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            return Mathf.Sin(-13 * Mathf.PI / 2 * (t + 1)) * Mathf.Pow(2, -10 * t) + 1;
        }

        // 弹性缓入缓出
        public static float ElasticInOut(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            if (t < 0.5f)
                return 0.5f * Mathf.Sin(13 * Mathf.PI / 2 * (2 * t)) * Mathf.Pow(2, 10 * ((2 * t) - 1));
            else
                return 0.5f * (Mathf.Sin(-13 * Mathf.PI / 2 * ((2 * t - 1) + 1)) * Mathf.Pow(2, -10 * (2 * t - 1)) +
                               2);
        }

        // 回退缓入
        public static float BackIn(float t)
        {
            const float c1 = 1.70158f;
            return t * t * ((c1 + 1) * t - c1);
        }

        // 回退缓出
        public static float BackOut(float t)
        {
            const float c1 = 1.70158f;
            return (t - 1) * t * ((c1 + 1) * t + c1) + 1;
        }

        // 回退缓入缓出
        public static float BackInOut(float t)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            if (t < 0.5f)
                return (t * t * ((c2 + 1) * t - c2)) * 2;
            else
                return ((t - 2) * t * ((c2 + 1) * t + c2) + 2) / 2;
        }

        public static float BounceIn(float t)
        {
            return 1 - BounceOut(1 - t);
        }

        public static float BounceOut(float t)
        {
            const float n1 = 7.5625f;
            const float d1 = 2.75f;
            
            if (t < 1 / d1)
            {
                return n1 * t * t;
            }
            else if (t < 2 / d1)
            {
                return n1 * (t -= 1.5f / d1) * t + 0.75f;
            }
            else if (t < 2.5 / d1)
            {
                return n1 * (t -= 2.25f / d1) * t + 0.9375f;
            }
            else
            {
                return n1 * (t -= 2.625f / d1) * t + 0.984375f;
            }
        }

        public static float BounceInOut(float t)
        {
            if (t < 0.5f)
            {
                return 0.5f * (1 - BounceOut(1 - 2 * t));
            }
            else
            {
                return 0.5f * BounceOut(2 * t - 1) + 0.5f;
            }
        }
    }
}