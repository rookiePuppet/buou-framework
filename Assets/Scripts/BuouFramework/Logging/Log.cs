using UnityEngine;

namespace BuouFramework.Logging
{
    public static class Log
    {
        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content)
        {
            Debug.Log($"[{Time.frameCount}]: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content)
        {
            Debug.LogError($"[{Time.frameCount}]: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content)
        {
            Debug.LogWarning($"[{Time.frameCount}]: {content}");
        }
        
        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content, object source)
        {
            Debug.Log($"[{Time.frameCount}] {source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content, object source)
        {
            Debug.LogError($"[{Time.frameCount}] {source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content, object source)
        {
            Debug.LogWarning($"[{Time.frameCount}] {source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content, string tag)
        {
            Debug.Log($"[{Time.frameCount}] {tag}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content, string tag)
        {
            Debug.LogError($"[{Time.frameCount}] {tag}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content, string tag)
        {
            Debug.LogWarning($"[{Time.frameCount}] {tag}: {content}");
        }
    }
}