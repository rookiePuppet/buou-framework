using System.Collections.Generic;
using BuouFramework.SingleInstance;
using UnityEngine;

namespace BuouFramework.Tweening
{
    public class TweenManager : MonoSingleton<TweenManager>
    {
#if UNITY_EDITOR
        [SerializeField] private int activeTweenCount;
#endif

        private readonly Dictionary<string, ITween> _activeTweens = new();
        private readonly List<ITween> _removingTweens = new();

        private void Update()
        {
            foreach (var tween in _activeTweens.Values)
            {
                tween.Update();
                if (tween.IsCompleted || tween.WasKilled)
                {
                    _removingTweens.Add(tween);
                }
            }

            if (_removingTweens.Count > 0)
            {
                foreach (var tween in _removingTweens)
                {
                    _activeTweens.Remove(tween.Identifier);
                }

                _removingTweens.Clear();
            }

#if UNITY_EDITOR
            activeTweenCount = _activeTweens.Count;
#endif
        }

        /// <summary>
        /// 添加一个补间对象
        /// 若存在相同标识符的对象，原对象会被停止，并被新对象替代
        /// </summary>
        /// <param name="tween"></param>
        public async void AddTween(ITween tween)
        {
            if (_activeTweens.TryGetValue(tween.Identifier, out var activeTween))
            {
                activeTween.FullKill();
            }

            await Awaitable.EndOfFrameAsync();
            _activeTweens[tween.Identifier] = tween;
        }
    }
}