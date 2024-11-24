using System;
using UnityEngine.EventSystems;

namespace Game.Framework.UI.Editor
{
    public class UIComponentSearchResult
    {
        public readonly string Name;
        public readonly string CamelCaseName;
        public readonly UIBehaviour Script;
        public readonly Type ScriptType;

        public UIComponentSearchResult(string name, UIBehaviour script, Type scriptType)
        {
            Name = name;
            Script = script;
            ScriptType = scriptType;
            CamelCaseName = char.ToLower(Name[0]) + Name[1..];
        }
    }
}