using UnityEngine;

namespace BuouFramework.Logging
{
    public static class Log
    {
        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content)
        {
            Debug.Log($"Default Tag: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content)
        {
            Debug.LogError($"Default Tag: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content)
        {
            Debug.LogWarning($"Default Tag: {content}");
        }
        
        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content, object source)
        {
            Debug.Log($"{source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content, object source)
        {
            Debug.LogError($"{source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content, object source)
        {
            Debug.LogWarning($"{source.GetType().Name}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Info(object content, string tag)
        {
            Debug.Log($"{tag}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Error(object content, string tag)
        {
            Debug.LogError($"{tag}: {content}");
        }

        [System.Diagnostics.Conditional("DEVELOPMENT")]
        public static void Warning(object content, string tag)
        {
            Debug.LogWarning($"{tag}: {content}");
        }
    }
}