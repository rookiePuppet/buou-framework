﻿using UnityEditor;
using UnityEngine;

namespace BuouFramework.Editor
{
    public static class FrameworkEditorUtility
    {
        public static string GetCurrentRootPath(ScriptableObject target)
        {
            var script = MonoScript.FromScriptableObject(target);
            var path = AssetDatabase.GetAssetPath(script);
            return  path[..path.LastIndexOf('/')];
        }
    }
}