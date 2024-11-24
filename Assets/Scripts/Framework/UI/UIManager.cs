using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;
using Object = UnityEngine.Object;

namespace Game.Framework.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Canvas _canvas;
        private Transform _bottomLayer;
        private Transform _middleLayer;
        private Transform _topLayer;

        /// <summary>
        /// 用于缓存UI界面的字典
        /// <para>Key->界面类型名称，Value->界面脚本</para>
        /// </summary>
        private readonly Dictionary<string, View> _views = new();

        private UIManager()
        {
            // 初始化画布
            var handle = Addressables.LoadAssetAsync<GameObject>("Canvas");
            var canvasObj = Object.Instantiate(handle.WaitForCompletion());
            Object.DontDestroyOnLoad(canvasObj);
            _canvas = canvasObj.GetComponent<Canvas>();
            handle.Release();

            // 初始化摄像机
            handle = Addressables.LoadAssetAsync<GameObject>("UICamera");
            var cameraObj = Object.Instantiate(handle.WaitForCompletion());
            handle.Release();
            Object.DontDestroyOnLoad(cameraObj);
            var camera = cameraObj.GetComponent<Camera>();
            // 处理URP管线中的摄像机层叠，将UI摄像机叠加到主摄像机上
            var cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(camera);
            _canvas.worldCamera = camera;

            // 初始化EventSystem
            handle = Addressables.LoadAssetAsync<GameObject>("EventSystem");
            var eventSystemObj = Object.Instantiate(handle.WaitForCompletion());
            Object.DontDestroyOnLoad(eventSystemObj);

            // 获取用于分层Transform
            _bottomLayer = canvasObj.transform.GetChild(0);
            _middleLayer = canvasObj.transform.GetChild(1);
            _topLayer = canvasObj.transform.GetChild(2);
        }

        /// <summary>
        /// 打开UI动画时长
        /// </summary>
        public float OpenViewDuration { get; set; } = 0.6f;

        /// <summary>
        /// 关闭UI动画时长
        /// </summary>
        public float CloseViewDuration { get; set; } = 0.6f;

        /// <summary>
        /// 通过异步加载并打开一个UI界面
        /// </summary>
        /// <param name="loaded">UI预制体加载完成的回调</param>
        /// <param name="layer">UI所在层级</param>
        /// <param name="effect">动画效果</param>
        /// <typeparam name="T">UI界面类型</typeparam>
        public async void Open<T>(Action<T> loaded = null, UILayer layer = UILayer.Middle,
            IViewEffect effect = null) where T : View
        {
            var viewName = typeof(T).Name;

            if (!_views.TryGetValue(viewName, out var view))
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(viewName);
                var obj = Object.Instantiate(await handle.Task, GetLayerTransform(layer));
                view = obj.GetComponent<T>();
                _views.Add(viewName, view);

                handle.Release();
            }

            loaded?.Invoke(view as T);
            view.gameObject.SetActive(true);
            view.Show();

            effect ??= new FadeEffect(FadeType.In);
            effect.ApplyTo(view);
        }

        /// <summary>
        /// 关闭UI界面
        /// </summary>
        /// <param name="destroy">是否立即销毁UI对象</param>
        /// <typeparam name="T">界面类型</typeparam>
        public async void Close<T>(bool destroy = false) where T : View
        {
            var viewName = typeof(T).Name;
            if (!_views.TryGetValue(viewName, out var view))
            {
                return;
            }

            var effect = new FadeEffect(FadeType.Out);
            await effect.ApplyTo(view);

            view.Hide();
            view.gameObject.SetActive(false);

            if (destroy)
            {
                Object.Destroy(view.gameObject);
            }
        }

        /// <summary>
        /// 获取UI界面
        /// 若界面不在缓存中，则返回null
        /// </summary>
        /// <typeparam name="T">界面类型</typeparam>
        /// <returns>界面脚本实例</returns>
        public T Get<T>() where T : View
        {
            var viewName = typeof(T).Name;

            if (_views.TryGetValue(viewName, out var view))
            {
                return view as T;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断界面是否正在显示
        /// </summary>
        /// <typeparam name="T">界面类型</typeparam>
        /// <returns></returns>
        public bool IsShow<T>() where T : View
        {
            var viewName = typeof(T).Name;
            return _views.TryGetValue(viewName, out var view) && view.gameObject.activeInHierarchy;
        }

        /// <summary>
        /// 将缓存中不在显示的界面对象销毁并清理掉它们的缓存
        /// </summary>
        public void ClearCache()
        {
            var clearing = (
                from name in _views.Keys
                let view = _views[name]
                where !view.gameObject.activeInHierarchy
                select name
            ).ToList();

            foreach (var name in clearing)
            {
                Object.Destroy(_views[name]);
                _views.Remove(name);
            }
        }

        /// <summary>
        /// 获取指定层级的Transform对象
        /// </summary>
        /// <param name="layer">UI层级</param>
        /// <returns></returns>
        private Transform GetLayerTransform(UILayer layer)
        {
            return layer switch
            {
                UILayer.Bottom => _bottomLayer,
                UILayer.Middle => _middleLayer,
                UILayer.Top => _topLayer,
                _ => _canvas.transform
            };
        }
    }
}