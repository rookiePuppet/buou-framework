using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BuouFramework.Tweening
{
    /// <summary>
    /// 针对Component类型扩展的常用方法类
    /// </summary>
    public static class TweenExtensions
    {
        #region Transform

        public static Tween<Vector3> TweenPosition(this Transform transform, Vector3 from, Vector3 to, float duration,
            int suffix = 0)
        {
            var identifier = $"{transform.GetInstanceID()}_Position_{suffix}";
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                    value => { transform.position = value; })
                .OnStart(() => transform.position = from);

            return tween;
        }

        public static Tween<Vector3> TweenLocalPosition(this Transform transform, Vector3 from, Vector3 to,
            float duration, int suffix = 0)
        {
            var identifier = $"{transform.GetInstanceID()}_LocalPosition_{suffix}";
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                    value => { transform.localPosition = value; })
                .OnStart(() => transform.localPosition = from);

            return tween;
        }

        public static Tween<Vector3> TweenScale(this Transform transform, Vector3 from, Vector3 to, float duration,
            int suffix = 0)
        {
            var identifier = $"{transform.GetInstanceID()}_Scale_{suffix}";
            var tween = new Tween<Vector3>(transform, identifier, from, to, duration,
                    value => { transform.localScale = value; })
                .OnStart(() => transform.localScale = from);

            return tween;
        }

        public static Tween<Quaternion> TweenRotation(this Transform transform, Quaternion from, Quaternion to,
            float duration, int suffix = 0)
        {
            var identifier = $"{transform.GetInstanceID()}_Rotation_{suffix}";
            var tween = new Tween<Quaternion>(transform, identifier, from, to, duration,
                    value => { transform.rotation = value; })
                .OnStart(() => transform.rotation = from);

            return tween;
        }

        public static Tween<Quaternion> TweenLocalRotation(this Transform transform, Quaternion from, Quaternion to,
            float duration, int suffix = 0)
        {
            var identifier = $"{transform.GetInstanceID()}_LocalRotation_{suffix}";
            var tween = new Tween<Quaternion>(transform, identifier, from, to, duration,
                    value => { transform.localRotation = value; })
                .OnStart(() => transform.localRotation = from);

            return tween;
        }

        #endregion

        #region RectTransform

        public static Tween<Vector2> TweenAnchoredPosition(this RectTransform rectTransform, Vector2 from, Vector2 to,
            float duration, int suffix = 0)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPosition_{suffix}";
            var tween = new Tween<Vector2>(rectTransform, identifier, from, to, duration,
                    value => { rectTransform.anchoredPosition = value; })
                .OnStart(() => rectTransform.anchoredPosition = from);

            return tween;
        }

        public static Tween<float> TweenAnchoredX(this RectTransform rectTransform, float from, float to,
            float duration, int suffix = 0)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPositionX_{suffix}";
            var tween = new Tween<float>(rectTransform, identifier, from, to, duration, value =>
            {
                var position = rectTransform.anchoredPosition;
                position.x = value;
                rectTransform.anchoredPosition = position;
            }).OnStart(() =>
            {
                var fromPosition = rectTransform.anchoredPosition;
                fromPosition.x = from;
                rectTransform.anchoredPosition = fromPosition;
            });


            return tween;
        }

        public static Tween<float> TweenAnchoredY(this RectTransform rectTransform, float from, float to,
            float duration, int suffix = 0)
        {
            var identifier = $"{rectTransform.GetInstanceID()}_AnchoredPositionY_{suffix}";
            var tween = new Tween<float>(rectTransform, identifier, from, to, duration, value =>
            {
                var position = rectTransform.anchoredPosition;
                position.y = value;
                rectTransform.anchoredPosition = position;
            }).OnStart(() =>
            {
                var fromPosition = rectTransform.anchoredPosition;
                fromPosition.y = from;
                rectTransform.anchoredPosition = fromPosition;
            });

            return tween;
        }

        #endregion

        #region SpriteRenderer

        public static Tween<Color> TweenColor(this SpriteRenderer spriteRenderer, Color from, Color to, float duration,
            int suffix = 0)
        {
            var identifier = $"{spriteRenderer.GetInstanceID()}_Color_{suffix}";
            var tween = new Tween<Color>(spriteRenderer, identifier, from, to, duration,
                    value => { spriteRenderer.color = value; })
                .OnStart(() => spriteRenderer.color = from);

            return tween;
        }

        public static Tween<float> TweenAlpha(this SpriteRenderer spriteRenderer, float from, float to, float duration,
            int suffix = 0)
        {
            var identifier = $"{spriteRenderer.GetInstanceID()}_Alpha_{suffix}";
            var tween = new Tween<float>(spriteRenderer, identifier, from, to, duration, value =>
            {
                var color = spriteRenderer.color;
                color.a = value;
                spriteRenderer.color = color;
            }).OnStart(() =>
            {
                var fromColor = spriteRenderer.color;
                fromColor.a = from;
                spriteRenderer.color = fromColor;
            });

            return tween;
        }

        #endregion

        #region Image

        public static Tween<Color> TweenColor(this Image image, Color from, Color to, float duration, int suffix = 0)
        {
            var identifier = $"{image.GetInstanceID()}_Color_{suffix}";
            var tween = new Tween<Color>(image, identifier, from, to, duration, value => { image.color = value; })
                .OnStart(() => { image.color = from; });

            return tween;
        }

        public static Tween<float> TweenAlpha(this Image image, float from, float to, float duration, int suffix = 0)
        {
            var identifier = $"{image.GetInstanceID()}_Alpha_{suffix}";

            var tween = new Tween<float>(image, identifier, from, to, duration, value =>
            {
                var color = image.color;
                color.a = value;
                image.color = color;
            }).OnStart(() =>
            {
                var fromColor = image.color;
                fromColor.a = from;
                image.color = fromColor;
            });

            return tween;
        }

        #endregion

        #region Canvas Group

        public static Tween<float> TweenAlpha(this CanvasGroup canvasGroup, float from, float to, float duration,
            int suffix = 0)
        {
            var identifier = $"{canvasGroup.GetInstanceID()}_Alpha_{suffix}";
            var tween = new Tween<float>(canvasGroup, identifier, from, to, duration,
                    value => { canvasGroup.alpha = value; })
                .OnStart(() => canvasGroup.alpha = from);

            return tween;
        }

        #endregion

        #region Audio Source

        public static Tween<float> TweenVolume(this AudioSource audioSource, float from, float to, float duration,
            int suffix = 0)
        {
            var identifier = $"{audioSource.GetInstanceID()}_Volume_{suffix}";
            var tween = new Tween<float>(audioSource, identifier, from, to, duration,
                    value => { audioSource.volume = value; })
                .OnStart(() => audioSource.volume = from);

            return tween;
        }

        #endregion

        #region TMP_Text

        public static Tween<int> TweenText(this TMP_Text text, string content, float duration, int suffix = 0)
        {
            var identifier = $"{text.GetInstanceID()}_Text_{suffix}";
            var tween = new Tween<int>(text, identifier, 0, content.Length, duration,
                    value => { text.maxVisibleCharacters = value; })
                .OnStart(() =>
                {
                    text.maxVisibleCharacters = 0;
                    text.text = content;
                });

            return tween;
        }

        public static Tween<Color> TweenColor(this TMP_Text text, Color from, Color to, float duration, int suffix = 0)
        {
            var identifier = $"{text.GetInstanceID()}_Color_{suffix}";
            var tween = new Tween<Color>(text, identifier, from, to, duration, value => { text.color = value; })
                .OnStart(() => text.color = from);

            return tween;
        }

        public static Tween<float> TweenAlpha(this TMP_Text text, float from, float to, float duration, int suffix = 0)
        {
            var identifier = $"{text.GetInstanceID()}_Alpha_{suffix}";
            var tween = new Tween<float>(text, identifier, from, to, duration, value =>
            {
                var color = text.color;
                color.a = value;
                text.color = color;
            }).OnStart(() =>
            {
                var fromColor = text.color;
                fromColor.a = from;
                text.color = fromColor;
            });

            return tween;
        }

        #endregion
    }
}