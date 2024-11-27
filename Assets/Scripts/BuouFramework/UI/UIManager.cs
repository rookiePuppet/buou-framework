using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering.Universal;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;
using BuouFramework.Logging;
using BuouFramework.SingleInstance;

namespace BuouFramework.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private class ViewCache
        {
            public View View { get; set; }
            public AsyncOperationHandle<GameObject> AssetHandle { get; set; }
            public bool IsOpening { get; set; }
            public bool IsClosing { get; set; }
        }

        private readonly Canvas _canvas;

        /// <summary>
        /// 用于缓存UI界面的字典
        /// </summary>
        private readonly Dictionary<Type, ViewCache> _caches = new();

        private readonly Dictionary<int, Transform> _layerTransforms = new();

        private UIManager()
        {
            // 初始化画布
            var canvasObj = LoadAndInstantiate("Canvas");
            Object.DontDestroyOnLoad(canvasObj);
            _canvas = canvasObj.GetComponent<Canvas>();

            // 初始化摄像机
            var cameraObj = LoadAndInstantiate("UICamera");
            Object.DontDestroyOnLoad(cameraObj);
            var camera = cameraObj.GetComponent<Camera>();
            // 处理URP管线中的摄像机层叠，将UI摄像机叠加到主摄像机上
            var cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(camera);
            _canvas.worldCamera = camera;

            // 初始化EventSystem
            var eventSystemObj = LoadAndInstantiate("EventSystem");
            Object.DontDestroyOnLoad(eventSystemObj);

            // 初始化层级
            InitializeLayer();
        }

        /// <summary>
        /// 通过异步加载并打开一个UI界面
        /// </summary>
        /// <param name="loaded">UI预制体加载完成的回调</param>
        /// <param name="layer">UI所在层级</param>
        /// <typeparam name="T">UI界面类型</typeparam>
        public async void Open<T>(Action<T> loaded = null, UILayer layer = UILayer.Middle) where T : View
        {
            // 还未加载，不在缓存当中
            if (!_caches.TryGetValue(typeof(T), out var cache))
            {
                cache = new ViewCache();
                cache.AssetHandle = Addressables.LoadAssetAsync<GameObject>(typeof(T).Name);
                _caches.Add(typeof(T), cache);

                var prefab = await cache.AssetHandle.Task;
                var obj = Object.Instantiate(prefab, GetLayerTransform(layer));
                cache.View = obj.GetComponent<T>();
            }

            // 已经正在加载并等待界面显示，此次打开无效
            if (!cache.AssetHandle.IsDone) return;
            if (cache.IsOpening)
            {
                Log.Warning($"{typeof(T).Name} is opening. You're trying to open it repeatedly.", this);
                return;
            }

            loaded?.Invoke(cache.View as T);

            cache.IsOpening = true;
            cache.View.IsVisible = true;
            cache.View.OnShow();
            await cache.View.ApplyShowEffect();
            cache.View.AfterShowEffect();
            cache.IsOpening = false;
        }

        /// <summary>
        /// 关闭UI界面
        /// </summary>
        /// <param name="destroy">是否立即销毁UI对象</param>
        /// <typeparam name="T">界面类型</typeparam>
        public async void Close<T>(bool destroy = false) where T : View
        {
            if (!HasLoaded(typeof(T), out var cache)) return;
            if (cache.IsClosing)
            {
                Log.Warning($"{typeof(T).Name} is closing. You're trying to close it repeatedly.", this);
                return;
            }

            if (destroy)
            {
                _caches.Remove(typeof(T));
            }

            cache.IsClosing = true;
            cache.View.OnHide();
            await cache.View.ApplyHideEffect();
            cache.View.AfterHideEffect();
            cache.View.IsVisible = false;
            cache.IsClosing = false;
            if (destroy)
            {
                Object.Destroy(cache.View.gameObject);
                cache.AssetHandle.Release();
            }
        }

        /// <summary>
        /// 获取UI界面
        /// 若界面不在缓存中，则返回null
        /// </summary>
        /// <typeparam name="T">界面类型</typeparam>
        /// <returns>界面脚本实例</returns>
        public T Get<T>() where T : View => HasLoaded(typeof(T), out var cache) ? cache.View as T : null;

        /// <summary>
        /// 判断界面是否正在显示
        /// </summary>
        /// <typeparam name="T">界面类型</typeparam>
        /// <returns></returns>
        public bool IsShowing<T>() where T : View => HasLoaded(typeof(T), out var cache) && cache.View.IsVisible;

        /// <summary>
        /// 直接销毁界面
        /// </summary>
        /// <typeparam name="T">界面类型</typeparam>
        public void Destroy<T>() where T : View
        {
            if (!HasLoaded(typeof(T), out var cache)) return;

            Object.Destroy(cache.View.gameObject);
            cache.AssetHandle.Release();
            _caches.Remove(typeof(T));
        }

        /// <summary>
        /// 将缓存中不在显示的界面对象销毁并清理掉它们的缓存
        /// </summary>
        public void ClearCache(bool clearAll = false)
        {
            var clearing = new List<ViewCache>();
            foreach (var cache in _caches.Values)
            {
                if (!cache.View.IsVisible || clearAll)
                {
                    clearing.Add(cache);
                }
            }

            foreach (var cache in clearing)
            {
                Object.Destroy(cache.View.gameObject);
                cache.AssetHandle.Release();
                _caches.Remove(cache.View.GetType());
            }
        }

        private bool HasLoaded(Type viewType, out ViewCache cache) =>
            _caches.TryGetValue(viewType, out cache) && cache.AssetHandle.IsDone;

        private Transform GetLayerTransform(UILayer layer) =>
            _layerTransforms.TryGetValue((int)layer, out var transform) ? transform : _canvas.transform;

        private void InitializeLayer()
        {
            foreach (int layer in Enum.GetValues(typeof(UILayer)))
            {
                var transform = new GameObject(Enum.GetName(typeof(UILayer), layer)).AddComponent<RectTransform>();
                transform.SetParent(_canvas.transform, false);
                transform.anchorMin = Vector2.zero;
                transform.anchorMax = Vector2.one;
                transform.offsetMin = Vector2.zero;
                transform.offsetMax = Vector2.zero;

                _layerTransforms[layer] = transform;
            }
        }

        private static GameObject LoadAndInstantiate(string name)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(name);
            var obj = Object.Instantiate(handle.WaitForCompletion());
            handle.Release();
            return obj;
        }
    }
}