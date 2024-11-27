using UnityEngine;

namespace BuouFramework.UI
{
    public abstract class View : MonoBehaviour
    {
        [Header("Components")] [SerializeField]
        protected ViewEffectTarget effectTarget;

        public virtual bool IsVisible
        {
            get => gameObject.activeInHierarchy;
            set => gameObject.SetActive(value);
        }

        public ViewEffectTarget EffectTarget => effectTarget;
        public bool HasEffect => effectTarget is not null;

        public virtual void OnShow() { }

        public virtual void OnHide() { }

        public virtual void AfterShowEffect() { }

        public virtual void AfterHideEffect() { }
    }
}