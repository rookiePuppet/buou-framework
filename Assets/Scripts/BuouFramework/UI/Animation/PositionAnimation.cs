using BuouFramework.Logging;
using BuouFramework.Tweening;
using UnityEngine;

namespace BuouFramework.UI
{
    [System.Serializable]
    public class PositionAnimation : PropertyAnimation<Vector2>
    {
        private static int _id;

        public override void Apply(ViewEffectTarget target)
        {
            var halfSize = new Vector2(target.RectTransform.sizeDelta.x / 2, target.RectTransform.sizeDelta.y / 2);
            //Log.Info(target.RectTransform.sizeDelta);
            halfSize = new Vector2(Screen.width / 2f, Screen.height / 2f);
            //Log.Info($"{startValue * halfSize}, {endValue * halfSize}", this);
            target.RectTransform.TweenAnchoredPosition(startValue * halfSize, endValue * halfSize, Duration, _id++)
                .SetDelay(startTime)
                .SetEase(easeType);
        }
    }
}