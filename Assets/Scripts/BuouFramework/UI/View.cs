using UnityEngine;

namespace BuouFramework.UI
{
    public abstract class View : MonoBehaviour
    {
        [SerializeField] protected ViewEffectTarget effectTarget;
        [SerializeField] protected ViewEffect showEffect;
        [SerializeField] protected ViewEffect hideEffect;

        protected bool IsAnimating;

        public virtual bool IsVisible
        {
            get => gameObject.activeInHierarchy;
            set => gameObject.SetActive(value);
        }

        public virtual async Awaitable ApplyShowEffect()
        {
            if (IsAnimating) return;
            if (effectTarget is null || showEffect is null) return;

            IsAnimating = true;
            showEffect.PrepareForAnimation(effectTarget);
            await showEffect.Apply(effectTarget);
            IsAnimating = false;
        }

        public virtual async Awaitable ApplyHideEffect()
        {
            if (IsAnimating) return;
            if (effectTarget is null || hideEffect is null) return;

            IsAnimating = true;
            await hideEffect.Apply(effectTarget);
            IsAnimating = false;
        }

        public virtual void OnShow() { }

        public virtual void OnHide() { }

        public virtual void AfterShowEffect() { }

        public virtual void AfterHideEffect() { }
    }
}