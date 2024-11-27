using UnityEngine;

namespace BuouFramework.UI
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(CanvasGroup))]
    public sealed class ViewEffectTarget : MonoBehaviour
    {
        [SerializeField] private ViewEffect showEffect;
        [SerializeField] private ViewEffect hideEffect;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private bool _isAnimating;

        public CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();
        public RectTransform RectTransform => _rectTransform ??= GetComponent<RectTransform>();
        public ViewEffect ShowEffect => showEffect;
        public ViewEffect HideEffect => hideEffect;

        public async Awaitable ApplyShowEffect()
        {
            if (_isAnimating || showEffect is null) return;

            _isAnimating = true;
            showEffect.PrepareForAnimation(this);
            await showEffect.Apply(this);
            _isAnimating = false;
        }

        public async Awaitable ApplyHideEffect()
        {
            if (_isAnimating || hideEffect is null) return;

            _isAnimating = true;
            await hideEffect.Apply(this);
            _isAnimating = false;
        }

#if UNITY_EDITOR
        private void Awake()
        {
            showEffect ??= Resources.Load<ViewEffect>("ViewEffect/Fade&Zoom[In]");
            hideEffect ??= Resources.Load<ViewEffect>("ViewEffect/Fade&Zoom[Out]");
        }
#endif
    }
}