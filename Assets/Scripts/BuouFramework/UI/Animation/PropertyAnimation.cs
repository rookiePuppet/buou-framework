using BuouFramework.Tweening;
using UnityEngine;

namespace BuouFramework.UI.Animation
{
    [System.Serializable]
    public abstract class PropertyAnimation<T>
    {
        public T startValue;
        public T endValue;
        public EaseType easeType;
        public float startTime;
        public float endTime;

        public float Duration => Mathf.Max(endTime - startTime, 0);
        
        public abstract void Apply(ViewEffectTarget target);
    }
}