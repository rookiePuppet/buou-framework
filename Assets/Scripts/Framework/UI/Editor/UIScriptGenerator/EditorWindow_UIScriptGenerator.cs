using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Game.Framework.Editor;

namespace Game.Framework.UI.Editor
{
    public class EditorWindow_UIScriptGenerator : EditorWindow
    {
        private readonly List<UIComponentSearchResult> _foundComponents = new();
        private UIScriptGenerator _generator;
        private ObjectField _targetObjectField;
        private TextField _scriptField;
        private Label _folderLabel;
        private ListView _elementsListView;

        private string _savePath = $"{Application.dataPath}/Scripts/UI";
        private StyleSheet _styleSheet;

        [MenuItem("Puppet Toolkit/UI Script Generator")]
        public static void OpenWindow()
        {
            var window = GetWindow<EditorWindow_UIScriptGenerator>();
            window.titleContent = new GUIContent("UI Script Generator");
        }

        private void OnEnable()
        {
            var currentPath = FrameworkEditorUtility.GetCurrentRootPath(this);
            _styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>($"{currentPath}/UIScriptGenerator.uss");
            _generator = new UIScriptGenerator(currentPath);
        }

        public void CreateGUI()
        {
            var topLayout = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            _folderLabel = new Label(_savePath) { style = { unityTextAlign = TextAnchor.MiddleLeft } };
            var chooseFolderButton = new Button { text = "选择目录" };
            topLayout.Add(_folderLabel);
            topLayout.Add(chooseFolderButton);
            rootVisualElement.Add(topLayout);
            chooseFolderButton.clicked += OnChooseFolderButtonClicked;

            var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal)
                { style = { marginTop = 5 } };

            var leftPane = new VisualElement { style = { flexDirection = FlexDirection.Column } };
            var rightPane = new VisualElement { style = { flexGrow = 1 } };
            leftPane.AddToClassList("function-area");
            rightPane.AddToClassList("function-area");
            splitView.Add(leftPane);
            splitView.Add(rightPane);

            // 设置目标UI预制体
            _targetObjectField = new ObjectField("UI根对象") { objectType = typeof(GameObject) };
            var setCurrentSelectedButton = new Button { text = "设置为当前选中对象" };
            leftPane.Add(_targetObjectField);
            leftPane.Add(setCurrentSelectedButton);
            _targetObjectField.RegisterValueChangedCallback(OnTargetFieldChanged);
            setCurrentSelectedButton.clicked += OnSetCurrentSelectedButtonClicked;

            // 显示和编辑查找到的控件
            _elementsListView = new ListView(_foundComponents, -1, CreateElementsListItem, BindElementsListItem)
            {
                showAddRemoveFooter = true, allowAdd = false, selectionType = SelectionType.Multiple,
                style = { flexGrow = 1, marginTop = 5 }
            };

            leftPane.Add(_elementsListView);
            _elementsListView.onRemove += OnElementListItemRemoved;

            // 生成和保存脚本
            var bottomLayout = new VisualElement { style = { flexDirection = FlexDirection.Row } };
            var generateScriptButton = new Button { text = "生成脚本" };
            var saveScriptButton = new Button { text = "保存脚本" };
            bottomLayout.Add(generateScriptButton);
            bottomLayout.Add(saveScriptButton);
            leftPane.Add(bottomLayout);
            generateScriptButton.clicked += OnGenerateScriptButtonClicked;
            saveScriptButton.clicked += OnSaveScriptButtonClicked;

            // 显示和编辑自动生成的脚本
            var scrollView = new ScrollView(ScrollViewMode.Vertical) { };
            _scriptField = new TextField { style = { flexGrow = 1 }, multiline = true, };
            scrollView.Add(_scriptField);
            rightPane.Add(scrollView);

            rootVisualElement.Add(splitView);
            rootVisualElement.style.paddingTop = 5;
            rootVisualElement.style.paddingLeft = 5;
            rootVisualElement.style.paddingRight = 5;
            rootVisualElement.style.paddingBottom = 5;

            rootVisualElement.styleSheets.Add(_styleSheet);
        }

        private void OnChooseFolderButtonClicked()
        {
            _savePath = EditorUtility.SaveFolderPanel("选择脚本存储位置", _savePath, string.Empty);
            _folderLabel.text = _savePath;
        }

        private void OnElementListItemRemoved(BaseListView view)
        {
            foreach (var index in view.selectedIndices.Reverse())
            {
                _foundComponents.RemoveAt(index);
            }

            view.RefreshItems();
            view.ClearSelection();
        }

        private void OnSetCurrentSelectedButtonClicked()
        {
            _targetObjectField.value = Selection.activeGameObject;
        }

        private void OnSaveScriptButtonClicked()
        {
            if (_scriptField.value == string.Empty)
            {
                Debug.LogError("请先生成脚本");
                return;
            }

            if (!Directory.Exists(_savePath))
            {
                Directory.CreateDirectory(_savePath);
            }

            var path = $"{_savePath}/{_targetObjectField.value.name}Base.cs";
            if (!File.Exists(path) || EditorUtility.DisplayDialog("提示", "基类脚本已存在，是否覆盖？", "是", "否"))
            {
                File.WriteAllText(path, _scriptField.value);
            }

            path = $"{_savePath}/{_targetObjectField.value.name}.cs";
            if (!File.Exists($"{_savePath}/{_targetObjectField.value.name}.cs"))
            {
                File.WriteAllText(path, _generator.GetInheritScript(_targetObjectField.value.name));
            }

            AssetDatabase.Refresh();

            // CompilationPipeline.assemblyCompilationFinished += OnScriptCompileFinished;
        }

        private void OnScriptCompileFinished(string arg1, CompilerMessage[] arg2)
        {
            var hasError = arg2.Any(arg => arg.type == CompilerMessageType.Error &&
                                           arg.message.Contains($"{_targetObjectField.value.name}Base"));
            if (!hasError)
            {
                var obj = _targetObjectField.value as GameObject;
                var path = $"{Application.dataPath[..Application.dataPath.LastIndexOf('/')]}/{arg1}";
                var assembly = System.Reflection.Assembly.LoadFrom(path);
                obj.AddComponent(assembly.GetType(_targetObjectField.value.name));
            }

            CompilationPipeline.assemblyCompilationFinished -= OnScriptCompileFinished;
        }

        private void OnGenerateScriptButtonClicked()
        {
            if (!_targetObjectField.value)
            {
                Debug.LogError("请先选择目标UI对象");
                return;
            }

            _scriptField.value = _generator.GetBaseScript(_targetObjectField.value.name, _foundComponents);
        }

        private void BindElementsListItem(VisualElement root, int index)
        {
            var data = _foundComponents[index];
            var label = root.Q<Label>();
            label.text = $"[{data.ScriptType.Name}] {data.Name} ";
        }

        private VisualElement CreateElementsListItem()
        {
            var item = new VisualElement();
            item.Add(new Label { style = { flexGrow = 1, unityTextAlign = TextAnchor.MiddleLeft } });
            return item;
        }

        private void OnTargetFieldChanged(ChangeEvent<Object> e)
        {
            var newObject = e.newValue as GameObject;
            if (!newObject)
            {
                return;
            }

            _foundComponents.Clear();
            _foundComponents.AddRange(UIAutomationUtility.FindAllUIComponents(newObject));
            _elementsListView.Rebuild();
        }
    }
}