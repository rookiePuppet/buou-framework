using UnityEngine;

namespace BuouFramework.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ViewEffectTarget : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();
                }

                return _canvasGroup;
            }
            protected set => _canvasGroup = value;
        }

        public RectTransform RectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
            protected set => _rectTransform = value;
        }
    }
}