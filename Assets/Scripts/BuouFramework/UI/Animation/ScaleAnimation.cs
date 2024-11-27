using BuouFramework.Tweening;
using UnityEngine;

namespace BuouFramework.UI
{
    [System.Serializable]
    public class ScaleAnimation : PropertyAnimation<Vector3>
    {
        private static int _id;

        public override void Apply(ViewEffectTarget target)
        {
            target.RectTransform.TweenScale(startValue, endValue, Duration, _id++)
                .SetDelay(startTime)
                .SetEase(easeType);
        }
    }
}