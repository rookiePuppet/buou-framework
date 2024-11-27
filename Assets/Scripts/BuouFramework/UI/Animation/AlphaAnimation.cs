using BuouFramework.Tweening;

namespace BuouFramework.UI
{
    [System.Serializable]
    public class AlphaAnimation : PropertyAnimation<float>
    {
        private static int _id;

        public override void Apply(ViewEffectTarget target)
        {
            target.CanvasGroup.TweenAlpha(startValue, endValue, Duration, _id++)
                .SetDelay(startTime)
                .SetEase(easeType);
        }
    }
}