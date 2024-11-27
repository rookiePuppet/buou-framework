using System.Linq;
using System.Reflection;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace BuouFramework.UI.Editor
{
    [UnityEditor.CustomEditor(typeof(View), true)]
    public class ViewCustomEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement { style = { flexDirection = FlexDirection.Column, flexGrow = 1 } };
            var button = new Button { text = "Auto bind components" };
            root.Add(button);
            button.clicked += ReferenceComponents;
            
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            return root;
        }
        
        
        
        private void ReferenceComponents()
        {
            var obj = ((View)target).gameObject;
            var searchResult = UIAutomationUtility.FindAllUIComponentsToDictionary(obj);
            var fields = target.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Default);
            foreach (var field in fields)
            {
                if (!field.IsDefined(typeof(SerializeField), false)) continue;
                if (!searchResult.TryGetValue(field.Name, out var results)) continue;

                foreach (var result in results.Where(result => field.FieldType == result.ScriptType))
                {
                    serializedObject.FindProperty(field.Name).objectReferenceValue = result.Script;
                    break;
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}