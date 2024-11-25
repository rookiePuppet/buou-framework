using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BuouFramework.UI
{
    public class UIManagerInitializerFromAddressables : IUIManagerInitializer
    {
        public GameObject ProvideCanvasObject() => LoadAndInstantiate("Canvas");

        public GameObject ProvideCameraObject() => LoadAndInstantiate("UICamera");

        public GameObject ProvideEventSystemObject() => LoadAndInstantiate("EventSystem");

        private static GameObject LoadAndInstantiate(string name)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(name);
            var obj = Object.Instantiate(handle.WaitForCompletion());
            handle.Release();
            return obj;
        }
    }
}