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
            EaseType.QuadraticEaseIn => QuadEaseIn(t),
            EaseType.QuadraticEaseOut => QuadEaseOut(t),
            EaseType.QuadraticEaseInOut => QuadEaseInOut(t),
            EaseType.CubicEaseIn => CubicEaseIn(t),
            EaseType.CubicEaseOut => CubicEaseOut(t),
            EaseType.CubicEaseInOut => CubicEaseInOut(t),
            EaseType.QuarticEaseIn => QuartEaseIn(t),
            EaseType.QuarticEaseOut => QuartEaseOut(t),
            EaseType.QuarticEaseInOut => QuartEaseInOut(t),
            EaseType.QuinticEaseIn => QuintEaseIn(t),
            EaseType.QuinticEaseOut => QuintEaseOut(t),
            EaseType.QuinticEaseInOut => QuintEaseInOut(t),
            EaseType.SineEaseIn => SineEaseIn(t),
            EaseType.SineEaseOut => SineEaseOut(t),
            EaseType.SineEaseInOut => SineEaseInOut(t),
            EaseType.CircularEaseIn => CircularEaseIn(t),
            EaseType.CircularEaseOut => CircularEaseOut(t),
            EaseType.CircularEaseInOut => CircularEaseInOut(t),
            EaseType.ExponentialEaseIn => ExponentialEaseIn(t),
            EaseType.ExponentialEaseOut => ExponentialEaseOut(t),
            EaseType.ExponentialEaseInOut => ExponentialEaseInOut(t),
            EaseType.ElasticEaseIn => ElasticEaseIn(t),
            EaseType.ElasticEaseOut => ElasticEaseOut(t),
            EaseType.ElasticEaseInOut => ElasticEaseInOut(t),
            EaseType.BackEaseIn => BackEaseIn(t),
            EaseType.BackEaseOut => BackEaseOut(t),
            EaseType.BackEaseInOut => BackEaseInOut(t),
            EaseType.BounceEaseIn => BounceEaseIn(t),
            EaseType.BounceEaseOut => BounceEaseOut(t),
            EaseType.BounceEaseInOut => BounceEaseInOut(t),
            _ => t
        };

        // 二次方缓入
        public static float QuadEaseIn(float t)
        {
            return t * t;
        }

        // 二次方缓出
        public static float QuadEaseOut(float t)
        {
            return t * (2 - t);
        }

        // 二次方缓入缓出
        public static float QuadEaseInOut(float t)
        {
            if (t < 0.5f)
                return 2 * t * t;
            else
                return -1 + (4 - 2 * t) * t;
        }

        // 三次方缓入
        public static float CubicEaseIn(float t)
        {
            return t * t * t;
        }

        // 三次方缓出
        public static float CubicEaseOut(float t)
        {
            float f = t - 1;
            return f * f * f + 1;
        }

        // 三次方缓入缓出
        public static float CubicEaseInOut(float t)
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
        public static float QuartEaseIn(float t)
        {
            return t * t * t * t;
        }

        // 四次方缓出
        public static float QuartEaseOut(float t)
        {
            float f = t - 1;
            return f * f * f * (1 - t) + 1;
        }

        // 四次方缓入缓出
        public static float QuartEaseInOut(float t)
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
        public static float QuintEaseIn(float t)
        {
            return t * t * t * t * t;
        }

        // 五次方缓出
        public static float QuintEaseOut(float t)
        {
            float f = t - 1;
            return f * f * f * f * f + 1;
        }

        // 五次方缓入缓出
        public static float QuintEaseInOut(float t)
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
        public static float SineEaseIn(float t)
        {
            return 1 - Mathf.Cos(t * Mathf.PI / 2);
        }

        // 正弦缓出
        public static float SineEaseOut(float t)
        {
            return Mathf.Sin(t * Mathf.PI / 2);
        }

        // 正弦缓入缓出
        public static float SineEaseInOut(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
        }

        // 圆形缓入
        public static float CircularEaseIn(float t)
        {
            return 1 - Mathf.Sqrt(1 - t * t);
        }

        // 圆形缓出
        public static float CircularEaseOut(float t)
        {
            return Mathf.Sqrt((2 - t) * t);
        }

        // 圆形缓入缓出
        public static float CircularEaseInOut(float t)
        {
            if (t < 0.5f)
                return 0.5f * (1 - Mathf.Sqrt(1 - 4 * t * t));
            else
                return 0.5f * (Mathf.Sqrt((-2 * t + 2) * (2 * t - 2)) + 1);
        }

        // 指数缓入
        public static float ExponentialEaseIn(float t)
        {
            return Mathf.Pow(2, 10 * (t - 1));
        }

        // 指数缓出
        public static float ExponentialEaseOut(float t)
        {
            return (-Mathf.Pow(2, -10 * t) + 1);
        }

        // 指数缓入缓出
        public static float ExponentialEaseInOut(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            if (t < 0.5f)
                return 0.5f * Mathf.Pow(2, 20 * t - 10);
            else
                return -0.5f * Mathf.Pow(2, -20 * t + 10) + 1;
        }

        // 弹性缓入
        public static float ElasticEaseIn(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            return Mathf.Sin(13 * Mathf.PI / 2 * t) * Mathf.Pow(2, 10 * (t - 1));
        }

        // 弹性缓出
        public static float ElasticEaseOut(float t)
        {
            if (t == 0 || Mathf.Approximately(t, 1))
                return t;

            return Mathf.Sin(-13 * Mathf.PI / 2 * (t + 1)) * Mathf.Pow(2, -10 * t) + 1;
        }

        // 弹性缓入缓出
        public static float ElasticEaseInOut(float t)
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
        public static float BackEaseIn(float t)
        {
            const float c1 = 1.70158f;
            return t * t * ((c1 + 1) * t - c1);
        }

        // 回退缓出
        public static float BackEaseOut(float t)
        {
            const float c1 = 1.70158f;
            return (t - 1) * t * ((c1 + 1) * t + c1) + 1;
        }

        // 回退缓入缓出
        public static float BackEaseInOut(float t)
        {
            const float c1 = 1.70158f;
            const float c2 = c1 * 1.525f;

            if (t < 0.5f)
                return (t * t * ((c2 + 1) * t - c2)) * 2;
            else
                return ((t - 2) * t * ((c2 + 1) * t + c2) + 2) / 2;
        }

        public static float BounceEaseIn(float t)
        {
            return 1 - BounceEaseOut(1 - t);
        }

        public static float BounceEaseOut(float t)
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

        public static float BounceEaseInOut(float t)
        {
            if (t < 0.5f)
            {
                return 0.5f * (1 - BounceEaseOut(1 - 2 * t));
            }
            else
            {
                return 0.5f * BounceEaseOut(2 * t - 1) + 0.5f;
            }
        }
    }
}