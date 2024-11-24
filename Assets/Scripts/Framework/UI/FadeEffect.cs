using Game.Framework.Tweening;
using UnityEngine;

namespace Game.Framework.UI
{
    public enum FadeType
    {
        In,
        Out
    }

    public struct FadeEffect : IViewEffect
    {
        public FadeEffect(FadeType fadeType, float duration = IViewEffect.ShortDuration,
            EaseType easeType = EaseType.QuinticEaseOut)
        {
            Type = fadeType;
            EaseType = easeType;
            Duration = duration;
        }

        public FadeType Type { get; private set; }
        public EaseType EaseType { get; private set; }
        public float Duration { get; private set; }

        public Awaitable ApplyTo(View view)
        {
            var startAlpha = Type == FadeType.In ? 0f : 1f;
            var endAlpha = Type == FadeType.In ? 1f : 0f;

            var completionSource = new AwaitableCompletionSource();
            view.CanvasGroup.TweenAlpha(startAlpha, endAlpha, Duration).Completed += () =>
            {
                completionSource.SetResult();
            };

            return completionSource.Awaitable;
        }
    }
}