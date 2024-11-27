using System.Collections.Generic;
using BuouFramework.UI.Animation;
using UnityEngine;

namespace BuouFramework.UI
{
    [CreateAssetMenu(menuName = "UI/ViewEffect")]
    public class ViewEffect : ScriptableObject
    {
        [SerializeField] private List<AlphaAnimation> alphaAnimations;
        [SerializeField] private List<ScaleAnimation> scaleAnimations;
        [SerializeField] private List<PositionAnimation> positionAnimations;
        [SerializeField] private List<RotationAnimation> rotationAnimations;

        public Awaitable Apply(ViewEffectTarget target)
        {
            if (target is null)
            {
                return Awaitable.WaitForSecondsAsync(0f);
            }

            var destinationTime = 0f;

            foreach (var animation in alphaAnimations) Apply(target, animation, ref destinationTime);
            foreach (var animation in scaleAnimations) Apply(target, animation, ref destinationTime);
            foreach (var animation in positionAnimations) Apply(target, animation, ref destinationTime);
            foreach (var animation in rotationAnimations) Apply(target, animation, ref destinationTime);

            return Awaitable.WaitForSecondsAsync(destinationTime);
        }

        private static void Apply<T>(ViewEffectTarget target, PropertyAnimation<T> animation, ref float destinationTime)
        {
            animation.Apply(target);
            destinationTime = Mathf.Max(animation.endTime, destinationTime);
        }

        public void PrepareForAnimation(ViewEffectTarget target)
        {
            if (alphaAnimations.Count > 0)
            {
                target.CanvasGroup.alpha = alphaAnimations[0].startValue;
            }

            if (scaleAnimations.Count > 0)
            {
                target.CanvasGroup.transform.localScale = scaleAnimations[0].startValue;
            }

            if (positionAnimations.Count > 0)
            {
                target.CanvasGroup.transform.localPosition = positionAnimations[0].startValue;
            }

            if (rotationAnimations.Count > 0)
            {
                target.CanvasGroup.transform.localRotation = Quaternion.Euler(rotationAnimations[0].startValue);
            }
        }
    }
}