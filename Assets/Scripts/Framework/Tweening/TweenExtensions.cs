using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Framework.Tweening
{
    /// <summary>
    /// 针对Component类型扩展的常用方法类
    /// </summary>
    public static class TweenExtensions
    {
        #region Transform

        public static Tween<Vector3> TweenPosition(this Transform transform, Vector3 from, Vector3 to, float duration)
        {
            var identifier = $"{transform.GetInstanceID()}_Position";
            transform.position = from;
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                value => { transform.position = value; });

            return tween;
        }

        public static Tween<Vector3> TweenLocalPosition(this Transform transform, Vector3 from, Vector3 to,
            float duration)
        {
            var identifier = $"{transform.GetInstanceID()}_LocalPosition";
            transform.localPosition = from;
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                value => { transform.localPosition = value; });

            return tween;
        }

        public static Tween<Vector3> TweenScale(this Transform transform, Vector3 from, Vector3 to, float duration)
        {
            var identifier = $"{transform.GetInstanceID()}_Scale";
            transform.localScale = from;
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                value => { transform.localScale = value; });

            return tween;
        }

        public static Tween<Quaternion> TweenRotation(this Transform transform, Quaternion from, Quaternion to,
            float duration)
        {
            var identifier = $"{transform.GetInstanceID()}_Rotation";
            transform.rotation = from;
            var tween = new Tween<Quaternion>(transform, identifier, from, to, duration,
                value => { transform.rotation = value; });

            return tween;
        }

        public static Tween<Quaternion> TweenLocalRotation(this Transform transform, Quaternion from, Quaternion to,
            float duration)
        {
            var identifier = $"{transform.GetInstanceID()}_LocalRotation";
            transform.localRotation = from;
            var tween = new Tween<Quaternion>(transform, identifier, from, to, duration,
                value => { transform.localRotation = value; });

            return tween;
        }

        #endregion

        #region RectTransform

        public static Tween<Vector2> TweenAnchoredPosition(this RectTransform rectTransform, Vector2 from, Vector2 to,
            float duration)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPosition";
            rectTransform.anchoredPosition = from;
            var tween = new Tween<Vector2>(rectTransform, identifier, from, to, duration,
                value => { rectTransform.anchoredPosition = value; });

            return tween;
        }

        public static Tween<float> TweenAnchoredX(this RectTransform rectTransform, float from, float to,
            float duration)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPositionX";
            var fromPosition = rectTransform.anchoredPosition;
            fromPosition.x = from;
            rectTransform.anchoredPosition = fromPosition;
            var tween = new Tween<float>(rectTransform, identifier, from, to, duration,
                value =>
                {
                    var position = rectTransform.anchoredPosition;
                    position.x = value;
                    rectTransform.anchoredPosition = position;
                });

            return tween;
        }

        public static Tween<float> TweenAnchoredY(this RectTransform rectTransform, float from, float to,
            float duration)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPositionY";
            var fromPosition = rectTransform.anchoredPosition;
            fromPosition.y = from;
            rectTransform.anchoredPosition = fromPosition;
            var tween = new Tween<float>(rectTransform, identifier, from, to, duration,
                value =>
                {
                    var position = rectTransform.anchoredPosition;
                    position.y = value;
                    rectTransform.anchoredPosition = position;
                });

            return tween;
        }

        #endregion

        #region SpriteRenderer

        public static Tween<Color> TweenColor(this SpriteRenderer spriteRenderer, Color from, Color to, float duration)
        {
            var identifier = $"{spriteRenderer.GetInstanceID()}_Color";
            spriteRenderer.color = from;
            var tween = new Tween<Color>(spriteRenderer, identifier, from, to, duration,
                value => { spriteRenderer.color = value; });

            return tween;
        }

        public static Tween<float> TweenAlpha(this SpriteRenderer spriteRenderer, float from, float to, float duration)
        {
            var identifier = $"{spriteRenderer.GetInstanceID()}_Alpha";
            var fromColor = spriteRenderer.color;
            fromColor.a = from;
            spriteRenderer.color = fromColor;
            var tween = new Tween<float>(spriteRenderer, identifier, from, to, duration, value =>
            {
                var color = spriteRenderer.color;
                color.a = value;
                spriteRenderer.color = color;
            });

            return tween;
        }

        #endregion

        #region Image

        public static Tween<Color> TweenColor(this Image image, Color from, Color to, float duration)
        {
            var identifier = $"{image.GetInstanceID()}_Color";
            image.color = from;
            var tween = new Tween<Color>(image, identifier, from, to, duration,
                value => { image.color = value; });

            return tween;
        }

        public static Tween<float> TweenAlpha(this Image image, float from, float to, float duration)
        {
            var identifier = $"{image.GetInstanceID()}_Alpha";
            var fromColor = image.color;
            fromColor.a = from;
            image.color = fromColor;
            var tween = new Tween<float>(image, identifier, from, to, duration, value =>
            {
                var color = image.color;
                color.a = value;
                image.color = color;
            });

            return tween;
        }

        #endregion

        #region Canvas Group

        public static Tween<float> TweenAlpha(this CanvasGroup canvasGroup, float from, float to, float duration)
        {
            var identifier = $"{canvasGroup.GetInstanceID()}_Alpha";
            canvasGroup.alpha = from;
            var tween = new Tween<float>(canvasGroup, identifier, from, to, duration,
                value => { canvasGroup.alpha = value; });

            return tween;
        }

        #endregion

        #region Audio Source

        public static Tween<float> TweenVolume(this AudioSource audioSource, float from, float to, float duration)
        {
            var identifier = $"{audioSource.GetInstanceID()}_Volume";
            audioSource.volume = from;
            var tween = new Tween<float>(audioSource, identifier, from, to, duration,
                value => { audioSource.volume = value; });
            return tween;
        }

        #endregion

        #region TeshMeshProUGUI

        public static Tween<int> TweenText(this TextMeshProUGUI text, string content, float duration)
        {
            var identifier = $"{text.GetInstanceID()}_Text";
            text.maxVisibleCharacters = 0;
            text.text = content;
            var tween = new Tween<int>(text, identifier, 0, content.Length, duration,
                value => { text.maxVisibleCharacters = value; });

            return tween;
        }

        public static Tween<Color> TweenColor(this TextMeshProUGUI text, Color from, Color to, float duration)
        {
            var identifier = $"{text.GetInstanceID()}_Color";
            text.color = from;
            var tween = new Tween<Color>(text, identifier, from, to, duration, value => { text.color = value; });

            return tween;
        }

        public static Tween<float> TweenAlpha(this TextMeshProUGUI text, float from, float to, float duration)
        {
            var identifier = $"{text.GetInstanceID()}_Alpha";
            var fromColor = text.color;
            fromColor.a = from;
            text.color = fromColor;
            var tween = new Tween<float>(text, identifier, from, to, duration, value =>
            {
                var color = text.color;
                color.a = value;
                text.color = color;
            });

            return tween;
        }

        #endregion
    }
}