using UnityEngine;

namespace Game.Framework.UI
{
    public interface IViewEffect
    {
        float Duration { get; }
        Awaitable ApplyTo(View view);

        const float ShortDuration = 0.2f;
        const float MiddleDuration = 0.4f;
        const float LongDuration = 0.6f;
    }
}