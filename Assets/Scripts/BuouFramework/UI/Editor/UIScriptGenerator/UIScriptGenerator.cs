using System.Collections.Generic;
using System.Text;
using UnityEditor;

namespace BuouFramework.UI.Editor
{
    public class UIScriptGenerator
    {
        private const string ScriptTemplateName = "UIScriptTemplate.txt";
        private const string BaseScriptTemplateName = "UIScriptTemplate_Base.txt";

        private readonly string _templateRootPath;
        private readonly StringBuilder _stringBuilder = new();

        public UIScriptGenerator(string templateRootPath)
        {
            _templateRootPath = templateRootPath;
        }

        private string ScriptTemplate => ReadFrom($"{_templateRootPath}/{ScriptTemplateName}");
        private string BaseScriptTemplate => ReadFrom($"{_templateRootPath}/{BaseScriptTemplateName}");

        public string GetBaseScript(string scriptName, IEnumerable<UIComponentSearchResult> results)
        {
            _stringBuilder.Clear();
            foreach (var result in results)
            {
                _stringBuilder.AppendLine(
                    $"\t\t[SerializeField] protected {result.ScriptType.Name} {result.CamelCaseName};");
            }

            return string.Format(BaseScriptTemplate, scriptName, _stringBuilder);
        }

        public string GetInheritScript(string scriptName)
        {
            return string.Format(ScriptTemplate, scriptName);
        }

        private string ReadFrom(string path)
        {
            var asset = AssetDatabase.LoadAssetAtPath<UnityEngine.TextAsset>(path);
            return asset.text;
        }
    }
}