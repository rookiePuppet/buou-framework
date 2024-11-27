using BuouFramework.Tweening;
using UnityEngine;

namespace BuouFramework.UI.Animation
{
    [System.Serializable]
    public class RotationAnimation : PropertyAnimation<Vector3>
    {
        private static int _id;

        public override void Apply(ViewEffectTarget target)
        {
            target.RectTransform
                .TweenLocalRotation(Quaternion.Euler(startValue), Quaternion.Euler(endValue), Duration, _id++)
                .SetDelay(startTime)
                .SetEase(easeType);
        }
    }
}