using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Framework.UI.Editor
{
    public static class UIAutomationUtility
    {
        public static IEnumerable<UIComponentSearchResult> FindAllUIComponents(GameObject rootObj,
            string namePrefix = "#")
        {
            return from ui in rootObj.GetComponentsInChildren<UIBehaviour>()
                where ui.gameObject.name.StartsWith(namePrefix)
                select new UIComponentSearchResult(ui.gameObject.name[1..], ui, ui.GetType());
        }

        public static Dictionary<string, List<UIComponentSearchResult>> FindAllUIComponentsToDictionary(
            GameObject rootObj, string namePrefix = "#")
        {
            var dict = new Dictionary<string, List<UIComponentSearchResult>>();
            foreach (var ui in rootObj.GetComponentsInChildren<UIBehaviour>())
            {
                if (!ui.gameObject.name.StartsWith(namePrefix))
                {
                    continue;
                }

                var result = new UIComponentSearchResult(ui.gameObject.name[1..], ui, ui.GetType());
                if (!dict.TryGetValue(result.CamelCaseName, out var list))
                {
                    list = new List<UIComponentSearchResult>();
                    dict[result.CamelCaseName] = list;
                }

                list.Add(result);
            }

            return dict;
        }
    }
}